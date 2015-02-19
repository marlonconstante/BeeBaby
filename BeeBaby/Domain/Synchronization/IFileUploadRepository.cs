using System;
using Skahal.Infrastructure.Framework.Repositories;

namespace Domain.Synchronization
{
	/// <summary>
	/// I file upload repository.
	/// </summary>
	public interface IFileUploadRepository : IRepository<FileUpload>
	{
	}
}