using System;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class EventData : DataBase
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }

		/// <summary>
		/// Gets or sets the start age in days.
		/// </summary>
		/// <value>The start age.</value>
		public int StartAge	{ get; set; }

		/// <summary>
		/// Gets or sets the end age in days.
		/// </summary>
		/// <value>The end age.</value>
		public int EndAge { get; set; }

		/// <summary>
		/// Gets or sets the kind.
		/// </summary>
		/// <value>The kind.</value>
		public int Kind { get; set; }

		[OneToMany]
		public List<MomentData> Moments { get; set; }

		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>The tag.</value>
		public int Tag { get; set; }
	}
}

