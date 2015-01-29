using System;
using System.Collections.Generic;
using Domain.Media;
using Skahal.Infrastructure.Framework.Domain;
using System.Linq;

namespace Domain.Moment
{
	/// <summary>
	/// Represents a Moment, usually composed of photos and videos.
	/// </summary>
	public class Moment : EntityWithIdBase<string>, IAggregateRoot, IMoment
	{
		/// <summary>
		/// The identifier template.
		/// </summary>
		public const string IdTemplate = "MomentTemplate";

		public Moment() : base()
		{
			SelectedMediaNames = new List<string>();
			Babies = new List<Baby.Baby>();
		}

		/// <summary>
		/// Gets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		public string ObjectId { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public Event Event { set; get; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { set; get; }

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Coordinates Position { set; get; }

		/// <summary>
		/// Gets or sets the local.
		/// </summary>
		/// <value>The local.</value>
		public Location Location { set; get; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date { set; get; }

		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		/// <value>The language.</value>
		public string Language {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the media count.
		/// </summary>
		/// <value>The media count.</value>
		public int MediaCount {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the selected media names.
		/// </summary>
		/// <value>The selected media names.</value>
		public IList<string> SelectedMediaNames { set; get; }

		/// <summary>
		/// Gets or sets the baby.
		/// </summary>
		/// <value>The baby.</value>
		public IList<Baby.Baby> Babies { set; get; }

		/// <summary>
		/// Gets the moment identifier.
		/// </summary>
		/// <value>The moment identifier.</value>
		public string MomentId {
			get {
				return Id;
			}
		}

		/// <summary>
		/// Gets the moment description.
		/// </summary>
		/// <value>The moment description.</value>
		public string MomentDescription {
			get {
				return Description;
			}
		}

		/// <summary>
		/// Gets the moment media count.
		/// </summary>
		/// <value>The moment media count.</value>
		public int MomentMediaCount {
			get {
				return MediaCount;
			}
		}

		/// <summary>
		/// Gets the moment date.
		/// </summary>
		/// <value>The moment date.</value>
		public DateTime MomentDate {
			get {
				return Date;
			}
		}

		/// <summary>
		/// Gets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		public string EventId {
			get {
				return Event.Id;
			}
		}

		/// <summary>
		/// Gets the name of the event tag.
		/// </summary>
		/// <value>The name of the event tag.</value>
		public string EventTagName {
			get {
				return Event.TagName;
			}
		}

		/// <summary>
		/// Gets the event description.
		/// </summary>
		/// <value>The event description.</value>
		public string EventDescription {
			get {
				return Event.Description;
			}
		}

		/// <summary>
		/// Gets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		public string LocationId {
			get {
				return Location.Id;
			}
		}

		/// <summary>
		/// Gets the name of the location.
		/// </summary>
		/// <value>The name of the location.</value>
		public string LocationName {
			get {
				return Location.Name;
			}
		}

		/// <summary>
		/// Gets the location latitude.
		/// </summary>
		/// <value>The location latitude.</value>
		public double LocationLatitude {
			get {
				return Location.Position.Latitude;
			}
		}

		/// <summary>
		/// Gets the location longitude.
		/// </summary>
		/// <value>The location longitude.</value>
		public double LocationLongitude {
			get {
				return Location.Position.Longitude;
			}
		}

		/// <summary>
		/// Gets the baby identifier.
		/// </summary>
		/// <value>The baby identifier.</value>
		public string BabyId {
			get {
				return Babies.FirstOrDefault().Id;
			}
		}

		/// <summary>
		/// Gets the name of the baby.
		/// </summary>
		/// <value>The name of the baby.</value>
		public string BabyName {
			get {
				return Babies.FirstOrDefault().Name;
			}
		}

		/// <summary>
		/// Gets the baby gender.
		/// </summary>
		/// <value>The baby gender.</value>
		public Baby.Gender BabyGender {
			get {
				return Babies.FirstOrDefault().Gender;
			}
		}

		/// <summary>
		/// Gets the baby birth date time.
		/// </summary>
		/// <value>The baby birth date time.</value>
		public DateTime BabyBirthDateTime {
			get {
				return Babies.FirstOrDefault().BirthDateTime;
			}
		}

		/// <summary>
		/// Gets the user email.
		/// </summary>
		/// <value>The user email.</value>
		public string UserEmail {
			get {
				return Babies.FirstOrDefault().Email;
			}
		}

		/// <summary>
		/// Determines whether this instance is template.
		/// </summary>
		/// <returns><c>true</c> if this instance is template; otherwise, <c>false</c>.</returns>
		public bool IsTemplate() {
			return Id == IdTemplate;
		}
	}
}