using System;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class BabyData : DataBase
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the birth date time.
		/// </summary>
		/// <value>The birth date time.</value>
		public DateTime BirthDateTime
		{
			get;
			set;
		}
	}
}

