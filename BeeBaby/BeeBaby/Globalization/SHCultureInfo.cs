using System;
using System.Globalization;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace BeeBaby.Globalization
{
	/// <summary>
	/// CultureInfo stuffs.
	/// </summary>
	public static class SHCultureInfo
	{
		#region Constants
		/// <summary>
		/// PtBr CultureInfo.
		/// </summary>
		public static readonly CultureInfo PtBR = new CultureInfo("pt-BR");

		/// <summary>
		/// EnUS CultureInfo.
		/// </summary>
		public static readonly CultureInfo EnUS = new CultureInfo("en-US");
		#endregion

		#region Fields
		private static Dictionary<string, CultureInfo> s_cultureInfosCache;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes the <see cref="BeeBaby.Globalization.SHCultureInfo"/> class.
		/// </summary>
		static SHCultureInfo()
		{
			FallbackCultureInfo = EnUS;
			s_cultureInfosCache = new Dictionary<string, CultureInfo>();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the fallback culture info.
		/// </summary>
		/// <value>
		/// The fallback culture info.
		/// </value>
		public static CultureInfo FallbackCultureInfo { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// Creates a CultureInfo instance from the specified NSLocale.
		/// </summary>
		/// <param name='locale'>
		/// Locale.
		/// </param>
		public static CultureInfo From(NSLocale locale)
		{
			var cultureCode = String.Format("{0}-{1}", locale.LanguageCode, locale.CountryCode);

			try
			{	
				Console.WriteLine("SHCulureInfo.From: culture code = {0}", cultureCode);

				var culture = new CultureInfo(cultureCode);

				var nf = culture.NumberFormat;

				nf.NumberDecimalSeparator = locale.DecimalSeparator;
				nf.NumberGroupSeparator = locale.GroupingSeparator;

				nf.CurrencyDecimalSeparator = locale.DecimalSeparator;
				nf.CurrencyGroupSeparator = locale.GroupingSeparator;
				nf.CurrencySymbol = locale.CurrencySymbol;

				return culture;
			}
			catch(Exception ex)
			{
				Console.WriteLine(
					"SHCultureInfo.From: error while converting from culture code '{0}', using the fallback culture info: '{1}'. Error: {2}.",
					cultureCode,
					FallbackCultureInfo.Name,
					ex.Message);

				return FallbackCultureInfo;
			}
		}

		/// <summary>
		/// From the specified cultureCode and keeps on the cache.
		/// </summary>
		/// <param name='cultureCode'>
		/// Culture code.
		/// </param>
		public static CultureInfo From(string cultureCode)
		{
			if(!s_cultureInfosCache.ContainsKey(cultureCode))
			{
				s_cultureInfosCache.Add(cultureCode, new CultureInfo(cultureCode));
			}

			return s_cultureInfosCache[cultureCode];
		}
		#endregion
	}
}

