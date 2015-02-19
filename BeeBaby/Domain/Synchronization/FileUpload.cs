using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Synchronization
{
	/// <summary>
	/// File upload.
	/// </summary>
	public class FileUpload : EntityWithIdBase<string>, IAggregateRoot
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