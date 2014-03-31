using SQLite.Net.Attributes;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class DataBase
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[PrimaryKey]
		public string Id { get; set; }
	}
}

