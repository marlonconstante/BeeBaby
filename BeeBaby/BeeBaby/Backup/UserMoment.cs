using System;
using Domain.Moment;
using Domain.Baby;

namespace BeeBaby.Backup
{
	public class UserMoment : IMoment
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.UserMoment"/> class.
		/// </summary>
		public UserMoment()
		{
		}

		/// <summary>
		/// Gets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		public string ObjectId { get; set; }

		/// <summary>
		/// Gets the moment identifier.
		/// </summary>
		/// <value>The moment identifier.</value>
		public string MomentId { get; set; }

		/// <summary>
		/// Gets the moment description.
		/// </summary>
		/// <value>The moment description.</value>
		public string MomentDescription { get; set; }

		/// <summary>
		/// Gets the moment media count.
		/// </summary>
		/// <value>The moment media count.</value>
		public int MomentMediaCount { get; set; }

		/// <summary>
		/// Gets the moment date.
		/// </summary>
		/// <value>The moment date.</value>
		public DateTime MomentDate { get; set; }

		/// <summary>
		/// Gets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		public string EventId { get; set; }

		/// <summary>
		/// Gets the name of the event tag.
		/// </summary>
		/// <value>The name of the event tag.</value>
		public string EventTagName { get; set; }

		/// <summary>
		/// Gets the event description.
		/// </summary>
		/// <value>The event description.</value>
		public string EventDescription { get; set; }

		/// <summary>
		/// Gets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		public string LocationId { get; set; }

		/// <summary>
		/// Gets the name of the location.
		/// </summary>
		/// <value>The name of the location.</value>
		public string LocationName { get; set; }

		/// <summary>
		/// Gets the location latitude.
		/// </summary>
		/// <value>The location latitude.</value>
		public double LocationLatitude { get; set; }

		/// <summary>
		/// Gets the location longitude.
		/// </summary>
		/// <value>The location longitude.</value>
		public double LocationLongitude { get; set; }

		/// <summary>
		/// Gets the baby identifier.
		/// </summary>
		/// <value>The baby identifier.</value>
		public string BabyId { get; set; }

		/// <summary>
		/// Gets the name of the baby.
		/// </summary>
		/// <value>The name of the baby.</value>
		public string BabyName { get; set; }

		/// <summary>
		/// Gets the baby gender.
		/// </summary>
		/// <value>The baby gender.</value>
		public Gender BabyGender { get; set; }

		/// <summary>
		/// Gets the baby birth date time.
		/// </summary>
		/// <value>The baby birth date time.</value>
		public DateTime BabyBirthDateTime { get; set; }

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <value>The language.</value>
		public string Language { get; set; }
	}
}