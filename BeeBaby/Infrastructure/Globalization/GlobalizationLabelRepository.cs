using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Globalization;
using Skahal.Infrastructure.Framework.Logging;

namespace Infrastructure.Globalization
{
	/// <summary>
	/// Globalization label repository.
	/// </summary>
	public class GlobalizationLabelRepository : TextGlobalizationLabelRepositoryBase
	{
		public override bool LoadCultureLabels(string cultureName)
		{
			//TODO: Quando for corrigido o Bug na Xamarin, podemos remover esse override.
			if (Entities.Count(f => f.CultureName.Equals(cultureName, StringComparison.OrdinalIgnoreCase)) == 0)
			{
				LogService.Debug("TextGlobalizationLabelRepositoryBase :: Loading texts for language '{0}'...", cultureName);

				var lines = GetCultureText(cultureName).Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);

				LogService.Debug("TextGlobalizationLabelRepositoryBase :: {0} texts founds...", lines.Length);

				foreach (var line in lines)
				{
					var lineParts = line.Split('=');
					Entities.Add(new GlobalizationLabel()
					{
						EnglishText = lineParts[0].Trim(),
						CultureText = lineParts[1].Trim().Replace(@"\n", System.Environment.NewLine),
						CultureName = cultureName
					});
				}

				return true;
			}

			return false;
		}


		protected override string GetCultureText(string cultureName)
		{
			return @"
Baby = Bebe
Photo = Foto
TimeLine = Linha de Tempo
First = Primeiro
Save = Salvar
";
		}
	}
}

