using System;
using Parse;
using System.Net;
using MonoTouch.Foundation;
using Infrastructure.Systems.Utils;
using System.IO;
using System.Threading.Tasks;
using BeeBaby.Backup;
using Skahal.Infrastructure.Framework.Domain;

namespace BeeBaby.Synchronization
{
	public class UserFile : FileHandle, IParseDomain, IParseUser, IFileRelease
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Synchronization.UserFile"/> class.
		/// </summary>
		public UserFile()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Synchronization.UserFile"/> class.
		/// </summary>
		/// <param name="deviceId">Device identifier.</param>
		/// <param name="filePath">File path.</param>
		public UserFile(string deviceId, string filePath)
		{
			DeviceId = deviceId;
			DirectoryName = Path.GetDirectoryName(filePath);
			FileName = Path.GetFileName(filePath);
			Version = 0L;

			UpdateSize();
		}

		/// <summary>
		/// Upload this instance.
		/// </summary>
		public async Task Upload()
		{
			using (var stream = base.Load(GetFilePath()))
			{
				ParseFile = new ParseFile(FileName, stream);

				var parseObject = this.ToParseObject<ParseObject>();
				await parseObject.SaveAsync();
			}
		}

		/// <summary>
		/// Download the specified beforeSaveAction.
		/// </summary>
		/// <param name="beforeSaveAction">Before save action.</param>
		public async Task Download(Func<byte[], bool> beforeSaveAction)
		{
			using (var webClient = new WebClient())
			{
				var data = await webClient.DownloadDataTaskAsync(ParseFile.Url);
				if (beforeSaveAction(data))
				{
					using (var stream = new MemoryStream(data))
					{
						base.Save(GetFilePath(), stream);
					}
				}
			}
		}

		/// <summary>
		/// Determines whether this instance is moment backup.
		/// </summary>
		/// <returns><c>true</c> if this instance is moment backup; otherwise, <c>false</c>.</returns>
		public bool IsMomentBackup()
		{
			return FileName.Equals(MomentBackup.FileName);
		}

		/// <summary>
		/// Gets the file key.
		/// </summary>
		/// <returns>The file key.</returns>
		public string GetFileKey()
		{
			return string.Concat(DirectoryName, FileName);
		}

		/// <summary>
		/// Updates the size.
		/// </summary>
		void UpdateSize()
		{
			var filePath = GetFilePath();
			Size = File.Exists(filePath) ? new FileInfo(filePath).Length : 0L;
		}

		/// <summary>
		/// Gets the file path.
		/// </summary>
		/// <returns>The file path.</returns>
		string GetFilePath()
		{
			return FileHandle.GetPath(DirectoryName, FileName);
		}

		/// <summary>
		/// Gets or sets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		public string ObjectId {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created at.
		/// </summary>
		/// <value>The created at.</value>
		public DateTime? CreatedAt {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the updated at.
		/// </summary>
		/// <value>The updated at.</value>
		public DateTime? UpdatedAt {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the device identifier.
		/// </summary>
		/// <value>The device identifier.</value>
		public string DeviceId {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public long Size {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public long Version {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the directory.
		/// </summary>
		/// <value>The name of the directory.</value>
		public string DirectoryName {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the parse file.
		/// </summary>
		/// <value>The parse file.</value>
		public ParseFile ParseFile {
			get;
			set;
		}
	}
}