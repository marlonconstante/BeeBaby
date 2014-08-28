namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// I parseable.
	/// </summary>
	public interface IParseable
	{
		/// <summary>
		/// Gets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		string ObjectId { get; }
	}
}