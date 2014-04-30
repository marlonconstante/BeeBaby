using System;
using SQLiteNetExtensions.Attributes;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class MomentsBabies
	{
		[ForeignKey(typeof(MomentData))]
		public string MomentId { get; set; }

		[ForeignKey(typeof(BabyData))]
		public string BabyId { get; set; }
	}
}

