using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Moment
{
	/// <summary>
	/// Location service.
	/// </summary>
	public class LocationService: ServiceBase<Location, ILocationRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.LocationService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public LocationService(ILocationRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Moment.LocationService"/> class.
		/// </summary>
		public LocationService()
		{
		}

		/// <summary>
		/// Gets all locations.
		/// </summary>
		/// <returns>The all locations.</returns>
		public IEnumerable<Location> GetAllLocations()
		{
			return MainRepository.FindAllAscending(o => o.Name);
		}

		/// <summary>
		/// Saves the location.
		/// </summary>
		/// <returns>The location.</returns>
		/// <param name="location">Location.</param>
		public Location SaveLocation(Location location)
		{
			var oldLocation = GetLocationByName(location.Name);

			if (oldLocation != null)
			{
				return oldLocation;
			}

			MainRepository[location.Id] = location;
			UnitOfWork.Commit();

			return location;
		}

		/// <summary>
		/// Gets the location.
		/// </summary>
		/// <returns>The location.</returns>
		/// <param name="id">Identifier.</param>
		public Location GetLocation(string id)
		{
			return MainRepository.FindBy(id);
		}

		/// <summary>
		/// Gets the name of the location by.
		/// </summary>
		/// <returns>The location by name.</returns>
		/// <param name="name">Name.</param>
		public Location GetLocationByName(string name)
		{
			return MainRepository.FindAll().FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
		}
	}
}

