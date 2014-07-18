using System;
using Skahal.Infrastructure.Framework.Globalization;
using Skahal.Infrastructure.Framework.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Globalization
{
	/// <summary>
	/// Globalization label repository.
	/// </summary>
	public class GlobalizationLabelRepository : IGlobalizationLabelRepository
	{

		List<GlobalizationLabel> m_entities = new List<GlobalizationLabel>();

		/// <summary>
		/// Loads the culture labels.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="cultureName">Culture name.</param>
		public bool LoadCultureLabels(string cultureName)
		{
			if (m_entities.Count(f => f.CultureName.Equals(cultureName, StringComparison.OrdinalIgnoreCase)) == 0)
			{
				LogService.Debug("TextGlobalizationLabelRepositoryBase :: Loading texts for language '{0}'...", cultureName);

				var lines = GetCultureText(cultureName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				LogService.Debug("TextGlobalizationLabelRepositoryBase :: {0} texts founds...", lines.Length);

				foreach (var line in lines)
				{
					var lineParts = line.Split('=');
					m_entities.Add(new GlobalizationLabel()
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

		/// <summary>
		/// Finds the first.
		/// </summary>
		/// <returns>The first.</returns>
		/// <param name="englishText">English text.</param>
		/// <param name="currentCulture">Current culture.</param>
		public GlobalizationLabel FindFirst(string englishText, string currentCulture)
		{
			return m_entities.FindAll(
				f =>   f.EnglishText.Equals(englishText, StringComparison.OrdinalIgnoreCase)
				&& f.CultureName == currentCulture)
					.FirstOrDefault ();

		}

		/// <summary>
		/// Gets the culture text.
		/// </summary>
		/// <returns>The culture text.</returns>
		/// <param name="cultureName">Culture name.</param>
		protected string GetCultureText(string cultureName)
		{
			if (cultureName == "pt-BR")
			{
				#region pt-BR
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
Yes = Sim
No = Não
ConfirmDeletion = Confirmação de exclusão
RemoveMoment = Remover este momento
QuestionRemoveMoment = Tem certeza que quer remover este momento?
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
				#endregion
			}
			else
			{
				#region En
				return @"
LongDateMask = MMMMM d, yyyy
Year = year
Years = years
Month = month
Months = months
Day = day
Days = days
Hour = hour
Hours = hours
Minute = minute
Minutes = minutes
Second = second
Seconds = seconds
And = and
AnyPlace = somewhere
Share = share
Warning = Warning
Information = Information
SharedMomentFacebook = Moment shared successfully on Facebook.
IllustrateMoment = Illustrate this moment of your child with beautiful pictures.
TakePictureOrImportAlbum = Take a picture or import it from your album.
GotIt = Gotcha
Ready = Ready!
Ops = Ops...
WeNeedValidEmail = We need a valid e-mail.
Yes = Yes
No = No
ConfirmDeletion = Confirm deletion
RemoveMoment = Delete this moment
QuestionRemoveMoment = Are you sure you want to delete this moment?
Name = Name
Baby = Baby
Photo = Photo
First = First
Save = Save
ImportPhotos = Import photos
Albums = Albums
WantThese = I want these >
FlashAuto = Auto
FlashOn = On
FlashOff = Off
Male = Male
Female = Female
Unknown = Unknown
ChoosePhotos = Choose Photos
Event = Event
Moment = Moment
MomentAbout = This moment is about:
SelectEvent = Choose an event
WhichWas = Where was it?
MomentRemember = What would you like to remember about this moment?
WhatsPlaceName = What is the name of this place?
WhatsBabyName = What is the baby name?
WhatsUserName = What is the username of the baby?
EnterBabyName = Type a name
EnterUserName = Type an e-mail
WhenWasHeBorn = When was he born?
Timeline = Timeline
ProductsForYourChild = Products for your child
MyProfile = My Profile
InviteFriends = Invite Friends
ManageFamily = Manage Family
Configurations = Configurations
About = About
Exit = Exit
Search = Search
LittleBody = Little Body
Family = Family
Ride = Ride
Sleepy = Sleepy
Bath = Bath
Smile = Smile
Lapy = Lapy
Celebrations = Celebrations
Birth = Birth
Pregnancy = Pregnancy
School = School
Recomendations = Recomendations
Everyday = Everyday
Firsts = Firsts
WhatsNew = What's New:
Version-1.1-ChangeLog = - Translated to english - Photos now saved on the iOS Album \n - Uploading photos to Facebook, Instagram, email and other \n
";
				#endregion
			}
		}
	}
}

