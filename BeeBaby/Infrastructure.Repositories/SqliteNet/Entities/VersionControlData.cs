using System;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class VersionControlData: DataBase
	{
		/// <summary>
		/// Gets or sets the entity.
		/// </summary>
		/// <value>The entity.</value>
		public string Entity
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public int Version
		{
			get;
			set;
		}
	}
}

