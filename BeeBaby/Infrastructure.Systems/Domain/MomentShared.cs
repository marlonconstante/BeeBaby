using System;
using System.Reflection;
using Parse;
using Domain.Moment;
using Domain.Baby;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Systems.Domain
{
	public class MomentShared : ParseShared, IMoment
	{
		public MomentShared(Moment moment) : base(moment)
		{
		}

		public MomentShared(ParseObject parseObject) : base(parseObject)
		{
		}

		/// <summary>
		/// Sets the thumbnails.
		/// </summary>
		/// <value>The thumbnails.</value>
		public IDictionary<string, byte[]> Thumbnails {
			set {
				Add("Thumbnails", value.Select((image) => new ParseFile(image.Key, image.Value)).ToList());
			}
		}

		/// <summary>
		/// Sets the full images.
		/// </summary>
		/// <value>The full images.</value>
		public IDictionary<string, byte[]> FullImages {
			set {
				Add("FullImages", value.Select((image) => new ParseFile(image.Key, image.Value)).ToList());
			}
		}

		/// <summary>
		/// Gets the thumbnails URL.
		/// </summary>
		/// <value>The thumbnails URL.</value>
		public IList<Uri> ThumbnailsUrl {
			get {
				return Get<IList<ParseFile>>("Thumbnails").Select((file) => file.Url).ToList();
			}
		}

		/// <summary>
		/// Gets the full images URL.
		/// </summary>
		/// <value>The full images URL.</value>
		public IList<Uri> FullImagesUrl {
			get {
				return Get<IList<ParseFile>>("FullImages").Select((file) => file.Url).ToList();
			}
		}

		/// <summary>
		/// Gets the moment identifier.
		/// </summary>
		/// <value>The moment identifier.</value>
		public string MomentId {
			get {
				return Get<string>("MomentId");
			}
		}

		/// <summary>
		/// Gets the moment description.
		/// </summary>
		/// <value>The moment description.</value>
		public string MomentDescription {
			get {
				return Get<string>("MomentDescription");
			}
		}

		/// <summary>
		/// Gets the moment media count.
		/// </summary>
		/// <value>The moment media count.</value>
		public int MomentMediaCount {
			get {
				return Get<int>("MomentMediaCount");
			}
		}

		/// <summary>
		/// Gets the moment date.
		/// </summary>
		/// <value>The moment date.</value>
		public DateTime MomentDate {
			get {
				return Get<DateTime>("MomentDate");
			}
		}

		/// <summary>
		/// Gets the event identifier.
		/// </summary>
		/// <value>The event identifier.</value>
		public string EventId {
			get {
				return Get<string>("EventId");
			}
		}

		/// <summary>
		/// Gets the name of the event tag.
		/// </summary>
		/// <value>The name of the event tag.</value>
		public string EventTagName {
			get {
				return Get<string>("EventTagName");
			}
		}

		/// <summary>
		/// Gets the event description.
		/// </summary>
		/// <value>The event description.</value>
		public string EventDescription {
			get {
				return Get<string>("EventDescription");
			}
		}

		/// <summary>
		/// Gets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
		public string LocationId {
			get {
				return Get<string>("LocationId");
			}
		}

		/// <summary>
		/// Gets the name of the location.
		/// </summary>
		/// <value>The name of the location.</value>
		public string LocationName {
			get {
				return Get<string>("LocationName");
			}
		}

		/// <summary>
		/// Gets the location latitude.
		/// </summary>
		/// <value>The location latitude.</value>
		public double LocationLatitude {
			get {
				return Get<double>("LocationLatitude");
			}
		}

		/// <summary>
		/// Gets the location longitude.
		/// </summary>
		/// <value>The location longitude.</value>
		public double LocationLongitude {
			get {
				return Get<double>("LocationLongitude");
			}
		}

		/// <summary>
		/// Gets the baby identifier.
		/// </summary>
		/// <value>The baby identifier.</value>
		public string BabyId {
			get {
				return Get<string>("BabyId");
			}
		}

		/// <summary>
		/// Gets the name of the baby.
		/// </summary>
		/// <value>The name of the baby.</value>
		public string BabyName {
			get {
				return Get<string>("BabyName");
			}
		}

		/// <summary>
		/// Gets the baby gender.
		/// </summary>
		/// <value>The baby gender.</value>
		public Gender BabyGender {
			get {
				return (Gender) Enum.Parse(typeof(Gender), Get<string>("BabyGender"));
			}
		}

		/// <summary>
		/// Gets the baby birth date time.
		/// </summary>
		/// <value>The baby birth date time.</value>
		public DateTime BabyBirthDateTime {
			get {
				return Get<DateTime>("BabyBirthDateTime");
			}
		}

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <value>The language.</value>
		public string Language {
			get {
				return Get<string>("Language");
			}
		}
	}
}