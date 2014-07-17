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
	public class GlobalizationLabelRepository : MemoryGlobalizationLabelRepository
	{
		/// <summary>
		/// Loads the culture labels.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="cultureName">Culture name.</param>
		public override bool LoadCultureLabels(string cultureName)
		{
			var abc = this.CountAll(cw => true);

			if (CountAll(f => f.CultureName.Equals(cultureName, StringComparison.OrdinalIgnoreCase)) == 0)
			{
				LogService.Debug("TextGlobalizationLabelRepositoryBase :: Loading texts for language '{0}'...", cultureName);

				var lines = GetCultureText(cultureName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				LogService.Debug("TextGlobalizationLabelRepositoryBase :: {0} texts founds...", lines.Length);

				foreach (var line in lines)
				{
					var lineParts = line.Split('=');
					Entities.Add(new GlobalizationLabel()
					{
						EnglishText = lineParts[0].Trim(),
						CultureText = lineParts[1].Trim().Replace(@"\n", Environment.NewLine),
						CultureName = cultureName
					});
				}
				return true;
			}

			return false;
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
Share = Compartilhar
Warning = Atenção
Information = Informação
SharedMomentFacebook = Momento compartilhado com sucesso no Facebook.
IllustrateMoment = Ilustre esse momento do seu filho com belas imagens.
TakePictureOrImportAlbum = Faça uma foto dele ou importe do seu álbum.
GotIt = Entendi
Ready = Pronto!
Ops = Ops...
WeNeedValidEmail = Precisamos de um e-mail válido.
Name = Nome
Baby = Bebê
Photo = Foto
First = Primeiro
Save = Salvar
ImportPhotos = Importar fotos
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
WhatsPlaceName = Qual o nome deste local?
WhatsBabyName = Qual é o nome do bebê?
WhatsUserName = Qual é o usuário do bebê?
EnterBabyName = Digite um nome
EnterUserName = Digite um e-mail
WhenWasHeBorn = Quando ele nasceu?
Timeline = Linha do tempo
ProductsForYourChild = Produtos para o seu filho
MyProfile = Meu perfil
InviteFriends = Convidar amigos
ManageFamily = Gerenciar família
Configurations = Configurações
About = Sobre
Exit = Sair
Search = Buscar
LittleBody = Corpinho
Family = Família
Ride = Passeio
Sleepy = Soninho
Bath = Banho
Smile = Sorriso
Lapy = Colinho
Celebrations = Comemorações
Birth = Nascimento
Pregnancy = Gravidez
School = Escolinha
Recomendations = Recomendações
Everyday = Cotidiano
Firsts = Primeiros
WhatsNew = O que há de novo:
Version-1.1-ChangeLog = - Fotos salvas no álbum do iOS \n - Envio de fotos para o Facebook, Instagram, e-mail e outros \n
";
		}
	}
}

