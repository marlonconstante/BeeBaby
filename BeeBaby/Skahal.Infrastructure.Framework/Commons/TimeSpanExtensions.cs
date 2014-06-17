using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Globalization;

namespace Skahal.Infrastructure.Framework.Commons
{
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Tos the readable string.
		/// </summary>
		/// <returns>The readable string.</returns>
		/// <param name="span">Span.</param>
		public static string ToReadableString(this TimeSpan span)
		{
			var separator = string.Format(" {0} ", "And".Translate());
			return string.Join(separator,
				span.GetReadableStringElements()
				.Where(txt => !string.IsNullOrWhiteSpace(txt))
				.Take(2));
		}

		/// <summary>
		/// Gets the readable string elements.
		/// </summary>
		/// <returns>The readable string elements.</returns>
		/// <param name="span">Span.</param>
		private static IEnumerable<string> GetReadableStringElements(this TimeSpan span)
		{
			yield return GetReadableString(span.GetYears(), "Year");
			yield return GetReadableString(span.GetMonths(), "Month");
			yield return GetReadableString(span.Days, "Day");
			yield return GetReadableString(span.Hours, "Hour");
			yield return GetReadableString(span.Minutes, "Minute");
			yield return GetReadableString(span.Seconds, "Second");
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