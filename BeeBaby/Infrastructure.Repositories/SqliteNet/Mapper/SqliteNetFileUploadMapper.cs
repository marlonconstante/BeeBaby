using System;
using Infrastructure.Repositories.Commons;
using Domain.Synchronization;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet.Mapper
{
	public class SqliteNetFileUploadMapper : IMapper<FileUpload, FileUploadData>
	{
		#region IMapper implementation

		/// <summary>
		/// Tos the domain entity.
		/// </summary>
		/// <returns>The domain entity.</returns>
		/// <param name="source">Repository entity.</param>
		public FileUpload ToDomainEntity(FileUploadData source)
		{
			FileUpload result = null;

			if (source != null)
			{
				result = new FileUpload();
				result.Id = source.Id;
				result.FilePath = source.FilePath;
			}

			return result;
		}

		/// <summary>
		/// Tos the repository entity.
		/// </summary>
		/// <returns>The repository entity.</returns>
		/// <param name="source">Domain entity.</param>
		public FileUploadData ToRepositoryEntity(FileUpload source)
		{
			FileUploadData result = null;

			if (source != null)
			{
				result = new FileUploadData();
				result.Id = source.Id;
				result.FilePath = source.FilePath;
			}

			return result;
		}

		#endregion
	}
}

