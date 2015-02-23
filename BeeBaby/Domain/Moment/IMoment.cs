using System;
using Domain.Baby;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Moment
{
	public interface IMoment : IParseable
	{
		/// <summary>
		/// Gets the moment identifier.
		/// </summary>
		/// <value>The moment identifier.</value>
		string MomentId { get; }

		/// <summary>
		/// Gets the moment description.
		/// </summary>
		/// <value>The moment description.</value>
		string MomentDescription { get; }

		/// <summary>
		/// Gets the moment media count.
		/// </summary>
		/// <value>The moment media count.</value>
		int MomentMediaCount { get; }

		/// <summary>
		/// Gets the moment date.
		/// </summary>
		/// <value>The moment date.</value>
		DateTime MomentDate { get; }

		/// <summary>
		/// Gets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		string EventId { get; }

		/// <summary>
		/// Gets the name of the event tag.
		/// </summary>
		/// <value>The name of the event tag.</value>
		string EventTagName { get; }

		/// <summary>
		/// Gets the event description.
		/// </summary>
		/// <value>The event description.</value>
		string EventDescription { get; }

		/// <summary>
		/// Gets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		string LocationId { get; }

		/// <summary>
		/// Gets the name of the location.
		/// </summary>
		/// <value>The name of the location.</value>
		string LocationName { get; }

		/// <summary>
		/// Gets the location latitude.
		/// </summary>
		/// <value>The location latitude.</value>
		double LocationLatitude { get; }

		/// <summary>
		/// Gets the location longitude.
		/// </summary>
		/// <value>The location longitude.</value>
		double LocationLongitude { get; }

		/// <summary>
		/// Gets the baby identifier.
		/// </summary>
		/// <value>The baby identifier.</value>
		string BabyId { get; }

		/// <summary>
		/// Gets the name of the baby.
		/// </summary>
		/// <value>The name of the baby.</value>
		string BabyName { get; }

		/// <summary>
		/// Gets the baby gender.
		/// </summary>
		/// <value>The baby gender.</value>
		Gender BabyGender { get; }

		/// <summary>
		/// Gets the baby birth date time.
		/// </summary>
		/// <value>The baby birth date time.</value>
		DateTime BabyBirthDateTime { get; }

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <value>The language.</value>
		string Language { get; }
	}
}