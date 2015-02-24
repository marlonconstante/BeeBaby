using System;
using Domain.Moment;
using Infrastructure.Systems.Domain;
using Domain.Baby;
using System.Globalization;

namespace Infrastructure.Systems.Utils
{
	public static class MomentExtension
	{
		/// <summary>
		/// The date format.
		/// </summary>
		const string DateFormat = "yyyyMMddhhmmssff";

		/// <summary>
		/// Tos the moment domain.
		/// </summary>
		/// <returns>The moment domain.</returns>
		/// <param name="source">Source.</param>
		public static Moment ToMomentDomain(this MomentPlan source)
		{
			var position = new Coordinates {
				Latitude = source.LocationLatitude,
				Longitude = source.LocationLongitude
			};

			var target = new Moment {
				Id = source.MomentId,
				Description = source.MomentDescription,
				MediaCount = source.MomentMediaCount,
				Date = DateTime.ParseExact(source.MomentDate, DateFormat, CultureInfo.InvariantCulture),
				Language = source.Language,
				Event = new Event {
					Id = source.EventId,
					Description = source.EventDescription
				},
				Position = position,
				Location = new Location {
					Id = source.LocationId,
					Name = source.LocationName,
					Position = position
				}
			};

			target.Babies.Add(new Baby {
				Id = source.BabyId,
				Name = source.BabyName,
				Gender = (Gender) Enum.Parse(typeof(Gender), source.BabyGender),
				BirthDateTime = DateTime.ParseExact(source.BabyBirthDateTime, DateFormat, CultureInfo.InvariantCulture)
			});

			return target;
		}

		/// <summary>
		/// Tos the moment plan.
		/// </summary>
		/// <returns>The moment plan.</returns>
		/// <param name="source">Source.</param>
		public static MomentPlan ToMomentPlan(this Moment source)
		{
			var target = new MomentPlan {
				MomentId = source.MomentId,
				MomentDescription = source.MomentDescription,
				MomentMediaCount = source.MomentMediaCount,
				MomentDate = source.MomentDate.ToString(DateFormat),
				EventId = source.EventId,
				EventTagName = source.EventTagName,
				EventDescription = source.EventDescription,
				LocationId = source.LocationId,
				LocationName = source.LocationName,
				LocationLatitude = source.LocationLatitude,
				LocationLongitude = source.LocationLongitude,
				BabyId = source.BabyId,
				BabyName = source.BabyName,
				BabyGender = source.BabyGender.ToString(),
				BabyBirthDateTime = source.BabyBirthDateTime.ToString(DateFormat),
				Language = source.Language
			};

			return target;
		}
	}
}