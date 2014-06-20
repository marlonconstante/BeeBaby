using System;

namespace Infrastructure.Repositories.Dropbox.Entities
{
	public class MomentData
	{
		public MomentData()
		{
		}

		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		public string EventId { set; get; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string EventDescription { set; get; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public double Longitude { set; get; }

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public double Latitude { set; get; }

		/// <summary>
		/// Gets or sets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		public string LocationId { set; get; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public string LocationDescription { set; get; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date { set; get; }

		/// <summary>
		/// Gets or sets the baby identifier.
		/// </summary>
		/// <value>The baby identifier.</value>
		public string BabyId { set; get; }

		/// <summary>
		/// Gets or sets the name of the baby.
		/// </summary>
		/// <value>The name of the baby.</value>
		public string BabyName { set; get; }

		/// <summary>
		/// Gets or sets the baby birh date.
		/// </summary>
		/// <value>The baby birh date.</value>
		public DateTime BabyBirhDate { set; get; }
	}
}