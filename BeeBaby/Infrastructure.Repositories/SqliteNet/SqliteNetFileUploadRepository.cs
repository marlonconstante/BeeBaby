using System;
using SQLite.Net;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using Domain.Synchronization;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetFileUploadRepository : SqliteNetRepositoryBase<FileUpload, FileUploadData>, IFileUploadRepository
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Infrastructure.Repositories.SqliteNet.SqliteNetFileUploadRepository"/> class.
		/// </summary>
		/// <param name="connection">Connection.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public SqliteNetFileUploadRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetFileUploadMapper(), unitOfWork)
		{
			connection.CreateTable<FileUploadData>();
		}
	}
}