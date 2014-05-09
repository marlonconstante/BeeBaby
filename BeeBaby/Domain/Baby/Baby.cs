using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Globalization;

namespace Domain.Baby
{
	/// <summary>
	/// Baby.
	/// </summary>
	public class Baby : EntityWithIdBase<string>, IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		/// <value>The gender.</value>
		public Gender Gender
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the birth date time.
		/// </summary>
		/// <value>The birth date time.</value>
		public DateTime BirthDateTime
		{
			get;
			set;
		}

		public int AgeInDays
		{
			get
			{
				return Convert.ToInt32((DateTime.Now - BirthDateTime).TotalDays);
			}
		}

		public string AgeInWords
		{
			get
			{
				return FormatAge(BirthDateTime, DateTime.Now);
			}
		}

		/// <summary>
		/// Formats the age.
		/// </summary>
		/// <returns>The age.</returns>
		/// <param name="birthDateTime">Birth date time.</param>
		public static string FormatAge(DateTime birthDateTime, DateTime baseDate)
		{
			var minutes = (baseDate - birthDateTime).TotalMinutes;

			if (minutes < 120)
			{
				return string.Format("{0} {1}", Math.Floor(minutes), "Minutes".Translate());
			}

			var hours = minutes / 60;
			if (minutes >= 120 && hours < 24)
			{
				return string.Format("{0} {1} {2} {3} {4}", Math.Floor(hours), "Hours".Translate(), "and".Translate(), Math.Floor((minutes % 60)), "Minutes".Translate());
			}
				
			var days = hours / 24;
			if (days >= 2 && days < 30)
			{
				return string.Format("{0} {1} {2} {3} {4}", Math.Floor(days), "Days".Translate(), "and".Translate(), Math.Floor(hours % 24), "Hours".Translate());
			}

			var months = days / 30;
			if (months >= 2 && months < 12)
			{
				return string.Format("{0} {1} {2} {3} {4}", Math.Floor(months), "Months".Translate(), "and".Translate(), Math.Floor(days % 30), "Days".Translate());
			}

			var years = months / 12;
			return string.Format("{0} {1} {2} {3} {4}", Math.Floor(years), "Years".Translate(), "and".Translate(), Math.Floor(months % 12), "Months".Translate());
		}
	}
}