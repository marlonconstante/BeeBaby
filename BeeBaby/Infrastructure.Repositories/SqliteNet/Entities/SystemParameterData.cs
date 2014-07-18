using System;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class SystemParameterData : DataBase
	{
		/// <summary>
		/// Gets or sets the entity.
		/// </summary>
		/// <value>The entity.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public string Value
		{
			get;
			set;
		}
	}
}

