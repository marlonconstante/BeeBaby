using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Globalization;
using Itenso.TimePeriod;

namespace Skahal.Infrastructure.Framework.Commons
{
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Tos the readable string.
		/// </summary>
		/// <returns>The readable string.</returns>
		/// <param name="dateDiff">Date diff.</param>
		public static string ToReadableString(this DateDiff dateDiff)
		{
			var separator = string.Format(" {0} ", "And".Translate());
			var values = dateDiff.GetReadableStringElements();
			return string.Join(separator,
				values
				.Where(txt => !string.IsNullOrWhiteSpace(txt))
				.Take(2));
		}

		/// <summary>
		/// Gets the readable string elements.
		/// </summary>
		/// <returns>The readable string elements.</returns>
		/// <param name="dateDiff">Date diff.</param>
		private static IEnumerable<string> GetReadableStringElements(this DateDiff dateDiff)
		{
			IList<string> results = new List<string>();
			results.Add(GetReadableString(dateDiff.Years, "Year"));
			results.Add(GetReadableString(dateDiff.Months % 12, "Month"));
			results.Add(GetReadableString(dateDiff.Days % 30, "Day"));
			results.Add(GetReadableString(dateDiff.Hours % 24, "Hour"));
			results.Add(GetReadableString(dateDiff.Minutes % 60, "Minute"));
			results.Add(GetReadableString(dateDiff.Seconds % 60, "Second"));

			return results;
		}

		/// <summary>
		/// Gets the readable string.
		/// </summary>
		/// <returns>The readable string.</returns>
		/// <param name="value">Value.</param>
		/// <param name="labelKey">Label key.</param>
		private static string GetReadableString(int value, string labelKey)
		{
			if (value == 0)
			{
				return string.Empty;
			}
			else
			{
				if (value > 1)
				{
					labelKey += "s";
				}
				return string.Format("{0} {1}", value, labelKey.Translate());
			}
		}

		/// <summary>
		/// Gets the years.
		/// </summary>
		/// <returns>The years.</returns>
		/// <param name="span">Span.</param>
		private static int GetYears(this TimeSpan span)
		{
			return span.Days / 365;
		}

		/// <summary>
		/// Gets the months.
		/// </summary>
		/// <returns>The months.</returns>
		/// <param name="span">Span.</param>
		private static int GetMonths(this TimeSpan span)
		{
			return (span.Days / 30) - (GetYears(span) * 12);
		}
	}
}