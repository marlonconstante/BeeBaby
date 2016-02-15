using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BeeBaby.Util;
using Parse;
using Infrastructure.Systems.Utils;
using System.Threading.Tasks;
using BeeBaby.Backup;
using Domain.Synchronization;

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

					await Upload();
					return await Download();
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
		/// Upload this instance.
		/// </summary>
		async Task<bool> Upload()
		{
			var service = new FileUploadService();
			var fileUploads = service.FindAllFileUploads();
			foreach (var fileUpload in fileUploads)
			{
				await new UserFile(DeviceId, fileUpload.FilePath).Upload();
				service.RemoveFileUpload(fileUpload);
			}
			return fileUploads.Count() > 0;
		}

		/// <summary>
		/// Download this instance.
		/// </summary>
		async Task<bool> Download()
		{
			var files = await ParseCloud.CallFunctionAsync<IEnumerable<object>>("FindNewFiles", GetParameters());
			foreach (var file in files)
			{
				var fileDomain = (file as ParseObject).ToDomain<UserFile>();
				await fileDomain.Download((data) => {
					return ConfirmReceiptFile(fileDomain, data);
				});
			}
			return files.Count() > 0;
		}

		/// <summary>
		/// Confirms the receipt file.
		/// </summary>
		/// <returns>The receipt file.</returns>
		/// <param name="file">File.</param>
		/// <param name="data">Data.</param>
		async Task<bool> ConfirmReceiptFile(UserFile file, byte[] data)
		{
			if (file.IsMomentBackup())
			{
				var momentBackup = new MomentBackup(file.DirectoryName);
				momentBackup.ReadAndUpdate(new MemoryStream(data));
				if (!momentBackup.Restore())
				{
					return false;
				}
			}

			return await ParseCloud.CallFunctionAsync<bool>("ConfirmReceiptFile", GetParameters(file.ObjectId));
		}

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <returns>The parameters.</returns>
		/// <param name="objectId">Object identifier.</param>
		IDictionary<string, object> GetParameters(string objectId = null)
		{
			var parameters = new Dictionary<string, object>();
			parameters.Add("DeviceId", DeviceId);

			if (!string.IsNullOrEmpty(objectId))
			{
				parameters.Add("ObjectId", objectId);
			}

			return parameters;
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