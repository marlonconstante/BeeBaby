using System;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using Skahal.Infrastructure.Framework.PCL.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Globalization
{
	/// <summary>
	/// Globalization label repository.
	/// </summary>
	public class GlobalizationLabelRepository : IGlobalizationLabelRepository
	{
		public void SetUnitOfWork(Skahal.Infrastructure.Framework.PCL.Repositories.IUnitOfWork unitOfWork)
		{
			throw new NotImplementedException();
		}

		public GlobalizationLabel FindBy(object key)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<GlobalizationLabel> FindAll(int offset, int limit, System.Linq.Expressions.Expression<Func<GlobalizationLabel, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<GlobalizationLabel> FindAllAscending<TOrderByKey>(int offset, int limit, System.Linq.Expressions.Expression<Func<GlobalizationLabel, bool>> filter, System.Linq.Expressions.Expression<Func<GlobalizationLabel, TOrderByKey>> orderBy)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<GlobalizationLabel> FindAllDescending<TOrderByKey>(int offset, int limit, System.Linq.Expressions.Expression<Func<GlobalizationLabel, bool>> filter, System.Linq.Expressions.Expression<Func<GlobalizationLabel, TOrderByKey>> orderBy)
		{
			throw new NotImplementedException();
		}

		public long CountAll(System.Linq.Expressions.Expression<Func<GlobalizationLabel, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public void Add(GlobalizationLabel item)
		{
			throw new NotImplementedException();
		}

		public void Remove(GlobalizationLabel item)
		{
			throw new NotImplementedException();
		}

		public GlobalizationLabel this [object index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		static SortedDictionary<string, GlobalizationLabel> m_entities = new SortedDictionary<string, GlobalizationLabel>();

		/// <summary>
		/// Loads the culture labels.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="cultureName">Culture name.</param>
		public bool LoadCultureLabels(string cultureName)
		{
			LogService.Debug("TextGlobalizationLabelRepositoryBase :: Loading texts for language '{0}'...", cultureName);

			var lines = GetCultureText(cultureName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			LogService.Debug("TextGlobalizationLabelRepositoryBase :: {0} texts founds...", lines.Length);

			foreach (var line in lines)
			{
				var lineParts = line.Split('=');

				m_entities[lineParts[0].Trim()] = new GlobalizationLabel()
				{
					EnglishText = lineParts[0].Trim(),
					CultureText = lineParts[1].Trim().Replace(@"\n", Environment.NewLine),
					CultureName = cultureName
				};
			}

			return true;
		}

		/// <summary>
		/// Finds the first.
		/// </summary>
		/// <returns>The first.</returns>
		/// <param name="englishText">English text.</param>
		/// <param name="currentCulture">Current culture.</param>
		public GlobalizationLabel FindFirst(string englishText, string currentCulture)
		{
			return m_entities[englishText];
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
Wait = Aguarde...
Warning = Atenção
Information = Informação
SharedMomentFacebook = Momento compartilhado com sucesso no Facebook.
IllustrateMoment = Ilustre esse momento do seu filho com belas imagens.
TakePictureOrImportAlbum = Faça uma foto dele ou importe do seu álbum.
ImportAlbum = Importe uma foto dele do seu álbum.
GotIt = Entendi
TryAgain = Tentar novamente
Ready = Pronto!
Ops = Ops...
WeNeedValidEmail = Precisamos de um e-mail válido.
PasswordRequired = Você precisa digitar uma senha.
SignUpError = Ocorreu um erro durante a inscrição.
EmailAndPasswordNotMatch = E-mail e senha não conferem.
CannotImportPhotos = Não é possível importar mais de {0} fotos por vez.
Yes = Sim
No = Não
Delete = Remover
AddPhotos = Adicionar fotos
ChangeMoment = Alterar este momento
SyncMoment = Sincronizar este momento
RemoveMoment = Remover este momento
QuestionRemoveMoment = Tem certeza que quer remover este momento?
Name = Nome
Baby = Bebê
MyBaby = Meu bebê
Photo = Foto
First = Primeiro
Save = Salvar
TakePhotos = Tirar fotos
ImportPhotos = Importar fotos
Albums = Álbuns
WantThese = Quero essas >
SwapEvent = Trocar evento >
FlashAuto = Auto
FlashOn = Com
FlashOff = Sem
Male = Menino
Female = Menina
Unknown = Não sei
ChoosePhotos = Escolher Fotos
Event = Evento
MomentTemplateDescription = Este é um momento modelo.
MomentTemplateEvent = Comece a construir sua história a cada conquista!
MomentTemplateLocation = Um lugar especial
Moment = Momento
MomentAbout = Este momento é sobre:
SelectEvent = Escolha um acontecimento
WhichWas = Onde foi?
MomentRemember = Você quer adicionar uma descrição sobre este momento?
WhatsPlaceName = Onde vocês estão?
WhatsBabyName = Qual é o nome do bebê?
WhatsUserName = E-mail para login
EnterBabyName = Digite um nome
EnterUserName = Digite um e-mail
WhenWasHeBorn = Data de nascimento (ou data prevista)
NewUserOrExisting = Novo usuário ou já existente
EnterPassword = Digite uma senha
WithoutSpamAndConfidentialData = Não se preocupe, não enviamos spam e \n seus dados serão mantidos em sigilo.
SignUp = Cadastrar
LogIn = Conectar
ForgotPassword = Esqueceu sua senha?
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
Version-1.1-ChangeLog = - Fotos salvas no álbum do iOS \n - Envio de fotos para o Facebook, Instagram, e-mail e outros \n - Agora é possível deletar o momento na Linha do Tempo \n
BabyData = Dados do Bebê
YourBabyDataWereSaved = Os dados do seu bebê foram salvos.
WhatEventIsThis = Qual é este evento?
BrowseCategory = Procure em outras categorias:
At = Em
InDate = No dia
AtAge = Com
Have = Tem
AboutThisMoment = Sobre esse momento
old = 
EditMomentTitle = Edite o momento
EditMomentText = Acrescente fotos \n e altere os detalhes \n deste momento
ViewAndShareTitle = Visualize e compartilhe
ViewAndShareText = Toque na foto para \n vê-la ampliada e compartilhar \n nas redes sociais
LetsStartTitle = Vamos começar?
LetsStartText = Toque no botão da câmera \n para tirar ou importar \n fotos
Version-1.2-ChangeLog = - Altere os momentos \n - Aproveite a facilidade da nova tela de momento \n - Novas cores, detalhes e lindezas no layout da linha do tempo \n - Veja os comentários do momento na linha do tempo \n - Compartilhe o novo card nas redes sociais
Version-1.3-ChangeLog = - Agora você pode selecionar múltiplas fotos do seu bebê para serem importadas. \n - E na Timeline, ao visualizar as fotos em tela cheia, é possível navegar entre elas.
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
AnyPlace = Somewhere
Share = Share
Wait = Wait...
Warning = Warning
Information = Information
SharedMomentFacebook = Moment shared successfully on Facebook.
IllustrateMoment = Illustrate this moment of your child with beautiful pictures.
TakePictureOrImportAlbum = Take a picture or import it from your album.
ImportAlbum = Import a picture from your album.
GotIt = Gotcha
TryAgain = Try again
Ready = Ready!
Ops = Ops...
WeNeedValidEmail = We need a valid e-mail.
PasswordRequired = You need to enter a password.
SignUpError = An error occurred during sign up.
EmailAndPasswordNotMatch = E-mail and password did not match.
CannotImportPhotos = Cannot import more than {0} photos at a time.
Yes = Yes
No = No
Delete = Delete
AddPhotos = Add photos
ChangeMoment = Change this moment
SyncMoment = Synchronize this moment
RemoveMoment = Delete this moment
QuestionRemoveMoment = Are you sure you want to delete this moment?
Name = Name
Baby = Baby
MyBaby = My baby
Photo = Photo
First = First
Save = Save
TakePhotos = Take photos
ImportPhotos = Import photos
Albums = Albums
WantThese = I want these >
SwapEvent = Change event >
FlashAuto = Auto
FlashOn = On
FlashOff = Off
Male = Boy
Female = Girl
Unknown = Unknown
ChoosePhotos = Choose Photos
Event = Event
MomentTemplateDescription = This is a moment template.
MomentTemplateEvent = Start building your story every achievement!
MomentTemplateLocation = A special place
Moment = Moment
MomentAbout = This moment is about:
SelectEvent = Choose an event
WhichWas = Where was it?
MomentRemember = Would you like to add a description to this moment?
WhatsPlaceName = Where are you?
WhatsBabyName = What is the baby name?
WhatsUserName = E-mail for login
EnterBabyName = Type a name
EnterUserName = Type an e-mail
WhenWasHeBorn = Birth date (or expected date of birth)
NewUserOrExisting = New user or existing
EnterPassword = Type a password
WithoutSpamAndConfidentialData = Do not worry, do not send spam and \n your data will be kept confidential.
SignUp = Sign up
LogIn = Log in
ForgotPassword = Forgot your password?
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
Lapy = Lap
Celebrations = Celebrations
Birth = Birth
Pregnancy = Pregnancy
School = Pre-School
Recomendations = Recomendations
Everyday = Everyday
Firsts = Firsts
WhatsNew = What's New:
Version-1.1-ChangeLog = - Translated to english - Photos now saved on the iOS Album \n - Uploading photos to Facebook, Instagram, email and other \n
BabyData = Baby Information
YourBabyDataWereSaved = Your baby's information were successfully saved.
WhatEventIsThis = What event is this?
BrowseCategory = Browse other categories:
At = At
InDate = On
AtAge = At
Have = Is
AboutThisMoment = About this moment
old = old
EditMomentTitle = Edit the moment
EditMomentText = Add photos \n and change the details \n this moment
ViewAndShareTitle = View and share
ViewAndShareText = Tap the photo to \n see it enlarged and share \n on social networks
LetsStartTitle = Let's start?
LetsStartText = Touch the camera button \n to take or import \n photos
Version-1.2-ChangeLog = Say hi to the new version of BeeBaby. \n After being featured in Best New Apps in Brazil, BeeBaby App goes worldwide in english version. 
Version-1.3-ChangeLog = - Now you can select multiple photos of your baby to be imported. \n - And in the Timeline when viewing photos in full screen, you can navigate between them.
";
				#endregion
			}
		}
	}
}

