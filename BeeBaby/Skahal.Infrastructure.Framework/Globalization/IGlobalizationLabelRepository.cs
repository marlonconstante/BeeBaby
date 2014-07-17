using System;
using Skahal.Infrastructure.Framework.Repositories;

namespace Skahal.Infrastructure.Framework.Globalization
{
	/// <summary>
	/// Defines a interface for globalization label repository.
	/// </summary>
	public interface IGlobalizationLabelRepository
	{
		/// <summary>
		/// Loads the culture labels.
		/// </summary>
		/// <returns><c>true</c>, if culture labels was loaded, <c>false</c> otherwise, already loaded.</returns>
		/// <param name="cultureName">Culture name.</param>
		bool LoadCultureLabels(string cultureName);

		/// <summary>
		/// Finds the first.
		/// </summary>
		/// <returns>The first.</returns>
		/// <param name="englishText">English text.</param>
		/// <param name="currentCulture">Current culture.</param>
		GlobalizationLabel FindFirst(string englishText, string currentCulture);
	}
}