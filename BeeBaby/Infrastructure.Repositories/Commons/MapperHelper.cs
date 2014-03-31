using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace Infrastructure.Repositories.Commons
{
	internal static class MapperHelper
	{
		/// <summary>
		/// Converte uma lista de entidades do repositório para uma lista de entidades de domínio.
		/// </summary>
		/// <typeparam name="TDomainEntity">O tipo de entidade de domínio.</typeparam>
		/// <typeparam name="TRepositoryEntity">O tipo de entidade do repositório.</typeparam>
		/// <param name="sources">A lista de entidades origem do repositório.</param>
		/// <param name="mapper">O mapeador das entidades.</param>
		/// <returns>A lista de entidades convertidas.</returns>
		public static IList<TDomainEntity> ToDomainEntities<TDomainEntity, TRepositoryEntity>(IEnumerable<TRepositoryEntity> sources, IMapper<TDomainEntity, TRepositoryEntity> mapper)
		{
			var result = new List<TDomainEntity>();

			if (sources != null)
			{
				foreach (var source in sources)
				{
					result.Add(mapper.ToDomainEntity(source));
				}
			}

			return result;
		}

		/// <summary>
		/// Converte uma lista de entidades de domínio para uma lista de entidades do repositório.
		/// </summary>
		/// <typeparam name="TDomainEntity">O tipo de entidade de domínio.</typeparam>
		/// <typeparam name="TRepositoryEntity">O tipo de entidade do repositório.</typeparam>
		/// <param name="sources">A lista de entidades origem do domínio.</param>
		/// <param name="mapper">O mapeador das entidades.</param>
		/// <returns>A lista de entidades convertidas.</returns>
		public static IList<TRepositoryEntity> ToRepositoryEntities<TDomainEntity, TRepositoryEntity>(IEnumerable<TDomainEntity> sources, IMapper<TDomainEntity, TRepositoryEntity> mapper)
		{
			var result = new List<TRepositoryEntity>();

			if (sources != null)
			{
				foreach (var source in sources)
				{
					result.Add(mapper.ToRepositoryEntity(source));
				}
			}

			return result;
		}

		/// <summary>
		/// Realiza o parse de um valor de enumeração oriundo do repositório para uma enumeração do domínio.
		/// </summary>
		/// <typeparam name="TDomainEnum">O tipo da enumeração do domínio.</typeparam>
		/// <param name="repositoryEnumValue">O valor da enumeração oriunda do repositório.</param>
		/// <param name="charsToRemove">Os caracters que devem ser removidos.</param>
		/// <returns>O valor equivalente da enumeração no domínio.</returns>
		public static TDomainEnum ParseToDomainEnum<TDomainEnum>(string repositoryEnumValue, char[] charsToRemove) where TDomainEnum : struct
		{
			if (repositoryEnumValue == null)
			{
				throw new ArgumentNullException("repositoryEnumValue");
			}

			var domainEnumValue = repositoryEnumValue;

			foreach (var c in charsToRemove)
			{
				domainEnumValue = domainEnumValue.Replace(c.ToString(), "");
			}

			TDomainEnum domainEnum;

			if (Enum.TryParse<TDomainEnum>(domainEnumValue, true, out domainEnum))
			{
				return domainEnum;
			}
			else
			{
				var msg = String.Format(CultureInfo.InvariantCulture, "Não foi possível converter a enumeração com valor '{0}' para a enumeração de domínio do tipo '{1}'.",
					          repositoryEnumValue, typeof(TDomainEnum));

				throw new ArgumentException(msg);
			}
		}

		/// <summary>
		/// Realiza o parse de valors de enumeração oriundos do repositório para enumerações do domínio.
		/// </summary>
		/// <typeparam name="TDomainEnum">O tipo da enumeração do domínio.</typeparam>
		/// <param name="repositoryEnumValues">O valor da enumeração oriunda do repositório.</param>
		/// <param name="charsToRemove">Os caracters que devem ser removidos.</param>
		/// <returns>Os valors equivalente das enumerações no domínio.</returns>
		public static IList<TDomainEnum> ParseToDomainEnum<TDomainEnum>(IEnumerable<string> repositoryEnumValues, char[] charsToRemove) where TDomainEnum : struct
		{
			var result = new List<TDomainEnum>();

			foreach (var e in repositoryEnumValues)
			{
				result.Add(ParseToDomainEnum<TDomainEnum>(e, charsToRemove));
			}

			return result;
		}

		/// <summary>
		/// Realiza o parse de valors de enumeração oriundos do repositório para enumerações do domínio.
		/// </summary>
		/// <typeparam name="TDomainEnum">O tipo da enumeração do domínio.</typeparam>
		/// <param name="repositoryEnumValues">O valor da enumeração oriunda do repositório.</param>
		/// <param name="charsToRemove">Os caracters que devem ser removidos.</param>
		/// <returns>Os valors equivalente das enumerações no domínio.</returns>
		public static IList<TDomainEnum> ParseToDomainEnum<TDomainEnum>(IEnumerable<Enum> repositoryEnumValues, char[] charsToRemove) where TDomainEnum : struct
		{
			return ParseToDomainEnum<TDomainEnum>(repositoryEnumValues.Select(r => r.ToString()), charsToRemove);
		}
	}
}

