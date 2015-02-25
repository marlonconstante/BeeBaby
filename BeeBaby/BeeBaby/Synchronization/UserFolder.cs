using System;
using Skahal.Infrastructure.Framework.PCL.Domain;
using Infrastructure.Parse;

namespace BeeBaby.Synchronization
{
	public class UserFolder : IParseDomain, IParseUser, IFileRelease
	{
		public UserFolder()
		{
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
		/// Gets or sets the length of the files.
		/// </summary>
		/// <value>The length of the files.</value>
		public long FilesLength {
			get;
			set;
		}
	}
}