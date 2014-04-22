using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Globalization;
using Skahal.Infrastructure.Framework.Logging;
using System.Collections.Generic;

namespace Infrastructure.Globalization
{
	/// <summary>
	/// Globalization label repository.
	/// </summary>
	public class GlobalizationLabelRepository : IGlobalizationLabelRepository
	{
		List<GlobalizationLabel> m_entities;

		public bool LoadCultureLabels(string cultureName)
		{
			m_entities = new List<GlobalizationLabel>();

			//TODO: Quando for corrigido o Bug na Xamarin, podemos remover esse override.
			if (m_entities.Count(f => f.CultureName.Equals(cultureName, StringComparison.OrdinalIgnoreCase)) == 0)
			{
				LogService.Debug("TextGlobalizationLabelRepositoryBase :: Loading texts for language '{0}'...", cultureName);

				var lines = GetCultureText(cultureName).Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);

				LogService.Debug("TextGlobalizationLabelRepositoryBase :: {0} texts founds...", lines.Length);

				foreach (var line in lines)
				{
					var lineParts = line.Split('=');
					m_entities.Add(new GlobalizationLabel()
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

		public GlobalizationLabel FindFirst(string englishText)
		{
			return m_entities.FindAll(
				f => f.EnglishText.Equals(englishText, StringComparison.OrdinalIgnoreCase))
				.FirstOrDefault();
		}

		protected string GetCultureText(string cultureName)
		{
			return @"
Baby = Bebê
Photo = Foto
TimeLine = Linha do Tempo
First = Primeiro
Save = Salvar
Albums = Álbuns
WantThese = Quero essas >
FlashAuto = Auto
FlashOn = Com
FlashOff = Sem
Name=Nome
Gender=Sexo
BirthDate=Data de Nascimento
";
		}
	}
}

