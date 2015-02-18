using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BeeBaby.Util;
using Parse;
using Infrastructure.Systems.Utils;
using System.Threading.Tasks;
using System.Net;
using MonoTouch.Foundation;
using BeeBaby.Backup;
using Domain.Moment;
using System.Threading;

namespace BeeBaby.Synchronization
{
	public sealed class FileSyncManager
	{
		/// <summary>
		/// Instance of singleton.
		/// </summary>
		static FileSyncManager s_instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Synchronization.FileSyncManager"/> class.
		/// </summary>
		private FileSyncManager()
		{
			FileKeys = new SortedSet<string>();
			LocalMapFiles = new Dictionary<string, UserFile>();
			RemoteMapFiles = new Dictionary<string, UserFile>();
			EmptyUserFile = new UserFile();
			IsRunning = false;
		}

		/// <summary>
		/// Synchronize files.
		/// </summary>
		/// <param name="syncEvent">Sync event.</param>
		public async Task<bool> Synchronize(ISyncEvent syncEvent)
		{
			if (!IsRunning)
			{
				try
				{
					IsRunning = true;
					syncEvent.StartedSynchronizing();

					FileKeys.Clear();

					LoadLocalMapFiles();
					await LoadRemoteMapFiles();

					foreach (var fileKey in FileKeys.Reverse())
					{
						var localFile = GetUserFile(LocalMapFiles, fileKey);
						var remoteFile = GetUserFile(RemoteMapFiles, fileKey);

						localFile.ObjectId = remoteFile.ObjectId;

						if (localFile.Version > remoteFile.Version)
						{
							await localFile.Upload();
						}
						else if (localFile.Version < remoteFile.Version)
						{
							await remoteFile.Download((data) => {
								if (remoteFile.IsMomentBackup())
								{
									var momentBackup = new MomentBackup(remoteFile.DirectoryName);
									momentBackup.ReadAndUpdate(new MemoryStream(data));
									return momentBackup.Restore();
								}
								return true;
							});
						}
					}

					return FileKeys.Count > 0;
				}
				catch (Exception ex)
				{
					// Ignora..
				}
				finally
				{
					syncEvent.EndedSynchronizing();
					IsRunning = false;
				}
			}
			return false;
		}

		/// <summary>
		/// Loads the local map files.
		/// </summary>
		void LoadLocalMapFiles()
		{
			LocalMapFiles.Clear();

			GetDirectories(FileHandle.RootFolderPath).ForEach(dir => {
				GetFiles(dir).ForEach(file => {
					AddUserFile(LocalMapFiles, new UserFile {
						DeviceId = DeviceId,
						DirectoryName = Path.GetFileName(dir),
						FileName = Path.GetFileName(file),
						Size = new FileInfo(file).Length,
						Version = 0L
					});
				});
			});
		}

		/// <summary>
		/// Loads the remote map files.
		/// </summary>
		/// <returns>The remote map files.</returns>
		async Task LoadRemoteMapFiles()
		{
			RemoteMapFiles.Clear();

			var parseObjects = await FindUserFile();
			foreach (var parseObject in parseObjects)
			{
				AddUserFile(RemoteMapFiles, parseObject.ToDomain<UserFile>());
			}
		}

		/// <summary>
		/// Finds the user file.
		/// </summary>
		/// <returns>The user file.</returns>
		async Task<IEnumerable<ParseObject>> FindUserFile()
		{
			var query = ParseObject.GetQuery("UserFile")
				.WhereEqualTo("DeviceId", DeviceId);
			return await query.FindAsync();
		}

		/// <summary>
		/// Adds the user file.
		/// </summary>
		/// <param name="dictionary">Dictionary.</param>
		/// <param name="userFile">User file.</param>
		void AddUserFile(IDictionary<string, UserFile> dictionary, UserFile userFile)
		{
			var fileKey = userFile.GetFileKey();
			FileKeys.Add(fileKey);

			dictionary.Add(fileKey, userFile);
		}

		/// <summary>
		/// Gets the user file.
		/// </summary>
		/// <returns>The user file.</returns>
		/// <param name="dictionary">Dictionary.</param>
		/// <param name="key">Key.</param>
		UserFile GetUserFile(IDictionary<string, UserFile> dictionary, string key)
		{
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}
			return EmptyUserFile;
		}

		/// <summary>
		/// Gets the directories.
		/// </summary>
		/// <returns>The directories.</returns>
		/// <param name="path">Path.</param>
		List<string> GetDirectories(string path)
		{
			return Directory.EnumerateDirectories(path).Where(dir => IsValidDirectory(dir))
				.ToList();
		}

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="path">Path.</param>
		List<string> GetFiles(string path)
		{
			return Directory.EnumerateFiles(path)
				.ToList();
		}

		/// <summary>
		/// Determines whether this instance is valid directory the specified path.
		/// </summary>
		/// <returns><c>true</c> if this instance is valid directory the specified path; otherwise, <c>false</c>.</returns>
		/// <param name="path">Path.</param>
		bool IsValidDirectory(string path)
		{
			string[] invalidFileNames = new string[] { Moment.IdTemplate, ".config", "temp" };
			return !invalidFileNames.Contains(Path.GetFileName(path));
		}

		/// <summary>
		/// Gets or sets the file keys.
		/// </summary>
		/// <value>The file keys.</value>
		SortedSet<string> FileKeys {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the local map files.
		/// </summary>
		/// <value>The local map files.</value>
		IDictionary<string, UserFile> LocalMapFiles {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the remote map files.
		/// </summary>
		/// <value>The remote map files.</value>
		IDictionary<string, UserFile> RemoteMapFiles {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the empty user file.
		/// </summary>
		/// <value>The empty user file.</value>
		UserFile EmptyUserFile {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is running.
		/// </summary>
		/// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
		bool IsRunning {
			get;
			set;
		}

		/// <summary>
		/// Gets the device identifier.
		/// </summary>
		/// <value>The device identifier.</value>
		string DeviceId {
			get {
				return PreferencesEditor.DeviceId;
			}
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static FileSyncManager Instance {
			get {
				if (s_instance == null)
				{
					s_instance = new FileSyncManager();
				}
				return s_instance; 
			}
		}
	}
}