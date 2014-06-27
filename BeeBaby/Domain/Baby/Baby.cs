﻿using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Commons;

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
		public string Email {
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
				return Convert.ToInt32((DateTime.Now - BirthDateTime).TotalDays);
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
			return (baseDate - birthDateTime).ToReadableString();
		}
	}
}