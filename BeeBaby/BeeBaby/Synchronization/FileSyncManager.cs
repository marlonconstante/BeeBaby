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
			LocalMapFiles = new Dictionary<string, FileData>();
			RemoteMapFiles = new Dictionary<string, FileData>();
			EmptyFileData = new FileData();
			IsRunning = false;
			DateLastSync = DateTime.MinValue;
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
						var localFile = GetFileData(LocalMapFiles, fileKey);
						var remoteFile = GetFileData(RemoteMapFiles, fileKey);

						localFile.ObjectId = remoteFile.ObjectId;

						if (localFile.DateLastModified > remoteFile.DateLastModified)
						{
							await localFile.Upload();
						}
						else if (localFile.DateLastModified < remoteFile.DateLastModified)
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

						UpdateDateLastSync(localFile.DateLastModified, remoteFile.DateLastModified);
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
		/// Updates the date last sync.
		/// </summary>
		/// <param name="dates">Dates.</param>
		void UpdateDateLastSync(params DateTime[] dates)
		{
			foreach (var date in dates)
			{
				if (date > DateLastSync)
				{
					DateLastSync = date;
				}
			}
		}

		/// <summary>
		/// Loads the local map files.
		/// </summary>
		void LoadLocalMapFiles()
		{
			LocalMapFiles.Clear();

			GetDirectories(FileHandle.RootFolderPath).ForEach(dir => {
				GetFiles(dir).ForEach(file => {
					AddFileData(LocalMapFiles, new FileData {
						DeviceId = DeviceId,
						DirectoryName = Path.GetFileName(dir),
						FileName = Path.GetFileName(file),
						DateLastModified = File.GetLastWriteTimeUtc(file)
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

			var parseObjects = await FindFileData();
			foreach (var parseObject in parseObjects)
			{
				AddFileData(RemoteMapFiles, parseObject.ToDomain<FileData>());
			}
		}

		/// <summary>
		/// Finds the file data.
		/// </summary>
		/// <returns>The file data.</returns>
		async Task<IEnumerable<ParseObject>> FindFileData()
		{
			var query = ParseObject.GetQuery("FileData")
				.WhereEqualTo("DeviceId", DeviceId)
				.WhereGreaterThan("DateLastModified", DateLastSync);
			return await query.FindAsync();
		}

		/// <summary>
		/// Adds the file data.
		/// </summary>
		/// <param name="dictionary">Dictionary.</param>
		/// <param name="fileData">File data.</param>
		void AddFileData(IDictionary<string, FileData> dictionary, FileData fileData)
		{
			var fileKey = fileData.GetFileKey();
			FileKeys.Add(fileKey);

			dictionary.Add(fileKey, fileData);
		}

		/// <summary>
		/// Gets the file data.
		/// </summary>
		/// <returns>The file data.</returns>
		/// <param name="dictionary">Dictionary.</param>
		/// <param name="key">Key.</param>
		FileData GetFileData(IDictionary<string, FileData> dictionary, string key)
		{
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}
			return EmptyFileData;
		}

		/// <summary>
		/// Gets the directories.
		/// </summary>
		/// <returns>The directories.</returns>
		/// <param name="path">Path.</param>
		List<string> GetDirectories(string path)
		{
			return Directory.EnumerateDirectories(path).Where(dir => IsValidDirectory(dir))
				.Where(dir => Directory.GetLastWriteTimeUtc(dir) > DateLastSync)
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
				.Where(file => File.GetLastWriteTimeUtc(file) > DateLastSync)
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
		IDictionary<string, FileData> LocalMapFiles {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the remote map files.
		/// </summary>
		/// <value>The remote map files.</value>
		IDictionary<string, FileData> RemoteMapFiles {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the empty file data.
		/// </summary>
		/// <value>The empty file data.</value>
		FileData EmptyFileData {
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
		/// Gets or sets the date last sync.
		/// </summary>
		/// <value>The date last sync.</value>
		DateTime DateLastSync {
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