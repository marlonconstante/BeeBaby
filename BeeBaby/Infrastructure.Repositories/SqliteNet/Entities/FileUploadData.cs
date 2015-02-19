using System;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	/// <summary>
	/// File upload data.
	/// </summary>
	public class FileUploadData : DataBase
	{
		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>The file path.</value>
		public string FilePath {
			get;
			set;
		}
	}
}