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
LongDateMask = d 'de' MMMMM 'de' yyyy
Year = ano
Years = anos
Month = mês
Months = meses
Day = dia
Days = dias
Hour = hora
Hours = horas
Minute = minuto
Minutes = minutos
Second = segundo
Seconds = segundos
And = e
AnyPlace = Um lugar qualquer
Name = Nome
Share = Compartilhar
Baby = Bebê
Photo = Foto
First = Primeiro
Save = Salvar
Albums = Álbuns
WantThese = Quero essas >
FlashAuto = Auto
FlashOn = Com
FlashOff = Sem
Male = Masculino
Female = Feminino
Unknown = Não sei
ChoosePhotos = Escolher Fotos
Event = Evento
Moment = Momento
MomentAbout = Este momento é sobre:
SelectEvent = Escolha um acontecimento
WhichWas = Onde foi?
MomentRemember = O que você gostaria de lembrar sobre este momento?
WhatsBabyName = Qual é o nome do bebê?
WhenWasHeBorn = Quando ele nasceu?
Timeline = Linha do tempo
ProductsForYourChild = Produtos para o seu filho
MyProfile = Meu perfil
InviteFriends = Convidar amigos
ManageFamily = Gerenciar família
Configurations = Configurações
About = Sobre
Exit = Sair
";
		}
	}
}

