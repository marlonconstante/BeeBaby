namespace Infrastructure.Repositories.Commons
{
	/// <summary>
	/// Defines an interface of a mapper between domain and repository.
	/// </summary>
	public interface IMapper<TDomainEntity, TRepositoryEntity>
	{
		/// <summary>
		/// Tos the domain entity.
		/// </summary>
		/// <returns>The domain entity.</returns>
		/// <param name="source">Repository entity.</param>
		TDomainEntity ToDomainEntity(TRepositoryEntity source);

		/// <summary>
		/// Tos the repository entity.
		/// </summary>
		/// <returns>The repository entity.</returns>
		/// <param name="source">Domain entity.</param>
		TRepositoryEntity ToRepositoryEntity(TDomainEntity source);
	}}

