using System;
using Skahal.Infrastructure.Framework.PCL.Domain;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using System.Collections.Generic;

namespace Domain.Synchronization
{
	/// <summary>
	/// File upload service.
	/// </summary>
	public class FileUploadService : ServiceBase<FileUpload, IFileUploadRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Synchronization.FileUploadService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public FileUploadService(IFileUploadRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Synchronization.FileUploadService"/> class.
		/// </summary>
		public FileUploadService()
		{
		}

		/// <summary>
		/// Finds all file uploads.
		/// </summary>
		/// <returns>The all file uploads.</returns>
		public IEnumerable<FileUpload> FindAllFileUploads()
		{
			return MainRepository.FindAll();
		}

		/// <summary>
		/// Inserts the file paths.
		/// </summary>
		/// <param name="filePaths">File paths.</param>
		public void InsertFilePaths(IEnumerable<string> filePaths)
		{
			foreach (var filePath in filePaths)
			{
				MainRepository.Add(new FileUpload {
					FilePath = filePath
				});
			}
			UnitOfWork.Commit();
		}

		/// <summary>
		/// Removes the file upload.
		/// </summary>
		/// <param name="fileUpload">File upload.</param>
		public void RemoveFileUpload(FileUpload fileUpload)
		{
			MainRepository.Remove(fileUpload);
			UnitOfWork.Commit();
		}
	}
}