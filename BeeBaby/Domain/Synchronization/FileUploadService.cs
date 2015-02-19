using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

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
		/// Saves the file upload.
		/// </summary>
		/// <param name="fileUpload">File upload.</param>
		public void SaveFileUpload(FileUpload fileUpload)
		{
			MainRepository[fileUpload.Id] = fileUpload;
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