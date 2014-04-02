using SQLite.Net.Attributes;
using Skahal.Infrastructure.Framework.Domain;

namespace Infrastructure.Repositories.SqliteNet.Entities
{
	public class DataBase : EntityWithIdBase<string>
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[PrimaryKey]
		override public string Id { get; set; }
	}
}

