using System;
using Skahal.Infrastructure.Framework.PCL.Domain;
using Skahal.Infrastructure.Framework.PCL.Commons;
using Itenso.TimePeriod;
using Infrastructure.Commons;

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
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email
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

		/// <summary>
		/// Gets the age in days.
		/// </summary>
		/// <value>The age in days.</value>
		public int AgeInDays
		{
			get
			{
				return CalculateAgeInDay(DateTime.Now);
			}
		}

		/// <summary>
		/// Gets the age in words.
		/// </summary>
		/// <value>The age in words.</value>
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
			var diff = new DateDiff(birthDateTime, baseDate);
			return  diff.ToReadableString();
		}

		/// <summary>
		/// Calculates the age in day.
		/// </summary>
		/// <returns>The age in day.</returns>
		/// <param name="date">Date.</param>
		public int CalculateAgeInDay(DateTime date)
		{
			return Convert.ToInt32((date - BirthDateTime).TotalDays);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Domain.Baby.Baby"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			return string.Concat(Name, Email, Gender, BirthDateTime).GetHashCode();
		}
	}
}