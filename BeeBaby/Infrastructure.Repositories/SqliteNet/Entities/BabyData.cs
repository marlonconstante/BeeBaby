using System;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

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
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email {
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

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		/// <value>The gender.</value>
		public int Gender {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the moments.
		/// </summary>
		/// <value>The moments.</value>
		///[ManyToMany(typeof(MomentsBabies))]
		///public List<MomentData> Moments { set; get; }
	}
}