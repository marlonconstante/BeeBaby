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
		/// <param name="saveCoordinates">If set to <c>true</c> save coordinates.</param>
		public Location SaveLocation(Location location, bool saveCoordinates)
		{

//			var oldLocation = MainRepository.FindBy(location.Id);
//			if (!saveCoordinates)
//			{
//				if (oldLocation != null)
//				{
//					location.Position = oldLocation.Position;
//				}
//				else
//				{
//					location.Position = null;
//				}
//			}
//
//			MainRepository[location.Id] = location;
//			UnitOfWork.Commit();
//
//			return location;


			var oldLocation = MainRepository.FindAll().FirstOrDefault(f => f.Name.Equals(location.Name, StringComparison.OrdinalIgnoreCase));

			if (oldLocation != null)
			{
				if (!saveCoordinates)
				{
					location.Position = oldLocation.Position;
				}
				location.Id = oldLocation.Id;
				MainRepository[oldLocation.Id] = location;
				UnitOfWork.Commit();
				return location;
			}

			if (!saveCoordinates)
			{
				location.Position = null;
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
	}
}

