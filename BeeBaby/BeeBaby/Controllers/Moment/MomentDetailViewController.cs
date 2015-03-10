using System;
using System.Linq;
using MonoTouch.UIKit;
using Domain.Moment;
using Application;
using System.Drawing;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.CoreLocation;
using PixateFreestyleLib;
using System.IO;
using BeeBaby.Globalization;
using Domain.Baby;
using BeeBaby.Util;
using BeeBaby.Proxy;
using BeeBaby.Navigations;
using BeeBaby.Localization;

namespace BeeBaby.Controllers
{
	public partial class MomentDetailViewController : NavigationViewController
	{
		UserLocation m_userLocation;
		IEnumerable<Location> m_locations;

		public MomentDetailViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var proxy = new EventProxy<MomentDetailViewController, EventArgs>(this);
			proxy.Action = (target, sender, args) => {
				target.InputLocalReturn(target.txtLocalName);
			};
			scrView.Scrolled += proxy.HandleEvent;

			vwDate.Clicked += proxy.HandleEvent;
			vwDate.Init(UIDatePickerMode.DateAndTime);

			tblView.ExclusiveTouch = true;
			txtLocalName.OffsetHeight = tblView.Frame.Height;
			txtLocalName.PlaceholderColor = UIColor.FromRGB(77, 95, 92);

			txtDescription.TextContainerInset = new UIEdgeInsets(13f, 27f, 0f, 0f);
			txtDescription.PlaceholderFrame = new RectangleF(32f, 14f, 275f, 34f);

			m_userLocation = new UserLocation();
			if (IsCameraFlow())
			{
				m_userLocation.UpdatedPosition = LoadNearLocation;
			}

			m_locations = new LocationService().GetAllLocations();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			FlurryAnalytics.Flurry.LogEvent("Momento: Cadastro.", true);

			base.ViewWillAppear(animated);

			txtLocalName.ShouldBeginEditing += InputLocalBeginEditing;
			txtLocalName.ShouldReturn += InputLocalReturn;
			txtLocalName.ShouldChangeCharacters += InputLocalChangeCharacters;

			if (IsCameraFlow())
			{
				m_userLocation.StartUpdatingLocation();
			}

			if (IsEditFlow())
			{
				var moment = CurrentContext.Instance.Moment;
				moment.SelectedMediaNames.Clear();

				IList<string> fileNames = new ImageProvider(moment.Id).GetFileNames(true);
				foreach (var fileName in fileNames)
				{
					moment.SelectedMediaNames.Add(Path.GetFileName(fileName));
				}

				vwDate.DateTime = moment.Date;
				txtDescription.Text = moment.Description;
				SelectLocation(moment.Location);
			}

			vwDate.UpdateInfo();
			evtView.Redraw(true);
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Momento: Cadastro.", null);

			base.ViewWillDisappear(animated);

			m_userLocation.StopUpdatingLocation();

			txtLocalName.ShouldBeginEditing -= InputLocalBeginEditing;
			txtLocalName.ShouldReturn -= InputLocalReturn;
			txtLocalName.ShouldChangeCharacters -= InputLocalChangeCharacters;
		}

		/// <summary>
		/// Views the did layout subviews.
		/// </summary>
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (scrView.ContentSize == SizeF.Empty)
			{
				scrView.ContentSize = new SizeF(320f, UIScreen.MainScreen.Bounds.Height - 64f);
				AdjustConstraints();
			}
		}

		/// <summary>
		/// Adjusts the constraints.
		/// </summary>
		void AdjustConstraints()
		{
			var constant = UIScreen.MainScreen.Bounds.Height - 568f;
			if (constant < 0 && evtView.Frame.Height == 234f)
			{
				Views.ChangeHeightAndDragNextViews(evtView, constant);
				evtView.Redraw(true);
			}
		}

		/// <summary>
		/// Loads the near location.
		/// </summary>
		public void LoadNearLocation()
		{
			var currentPlace = GetLocation(m_userLocation.Position);
			foreach (var location in m_locations)
			{
				var place = GetLocation(location.Position);
				if (place.DistanceFrom(currentPlace) <= 200d)
				{
					FlurryAnalytics.Flurry.LogEvent("Momento: GPS Localizou automatico.");
					SelectLocation(location);
					break;
				}
			}
		}

		/// <summary>
		/// Gets the location.
		/// </summary>
		/// <returns>The location.</returns>
		/// <param name="position">Position.</param>
		CLLocation GetLocation(Coordinates position)
		{
			return new CLLocation(position.Latitude, position.Longitude);
		}

		/// <summary>
		/// Inputs the local begin editing.
		/// </summary>
		/// <returns><c>true</c>, if local begin editing was input, <c>false</c> otherwise.</returns>
		/// <param name="textField">Text field.</param>
		public bool InputLocalBeginEditing(UITextField textField)
		{
			UIView.Animate(0.5d, () => {
				tblView.Superview.Alpha = 1f;
				InputLocalChangeCharacters(textField, new NSRange(0, 0), string.Empty);
			});

			return true;
		}

		/// <summary>
		/// Inputs the local return.
		/// </summary>
		/// <returns><c>true</c>, if local return was input, <c>false</c> otherwise.</returns>
		/// <param name="textField">Text field.</param>
		public bool InputLocalReturn(UITextField textField)
		{
			if (!textField.IsEditing)
			{
				UIView.Animate(0.5d, () => {
					tblView.Superview.Alpha = 0f;
				});
			}

			return true;
		}

		/// <summary>
		/// Inputs the local change characters.
		/// </summary>
		/// <returns><c>true</c>, if local change characters was input, <c>false</c> otherwise.</returns>
		/// <param name="textField">Text field.</param>
		/// <param name="range">Range.</param>
		/// <param name="replacementString">Replacement string.</param>
		public bool InputLocalChangeCharacters(UITextField textField, NSRange range, string replacementString)
		{
			var source = ((range.Location == 0 && range.Length == 0) || replacementString != string.Empty)
				? string.Concat(textField.Text, replacementString).ToLowerInvariant()
				: textField.Text.Substring(0, textField.Text.Length - 1).ToLowerInvariant();
			try
			{
				var locations = m_locations.Where(l => l.Name.ToLowerInvariant().Contains(source))
					.OrderByDescending(l => l.Name.StartsWith(source, StringComparison.InvariantCultureIgnoreCase))
					.ToArray();

				SelectLocation(locations.Where(l => l.Name.ToLowerInvariant().Equals(source)).FirstOrDefault(), true);

				tblView.Source = new AutoCompleteTableSource(locations, this);
				tblView.ReloadData();
			}
			catch (Exception)
			{
				Console.WriteLine("Erro ao buscar as sugestões.");
			}

			return true;
		}

		/// <summary>
		/// Selects the location.
		/// </summary>
		/// <param name="location">Location.</param>
		/// <param name="updateOnlyCoordinates">If set to <c>true</c> update only coordinates.</param>
		public void SelectLocation(Location location, bool updateOnlyCoordinates = false)
		{
			m_userLocation.StopUpdatingLocation();

			if (location == null)
			{
				m_userLocation.StartUpdatingLocation();
			}
			else
			{
				m_userLocation.Position = location.Position;

				if (!updateOnlyCoordinates)
				{
					txtLocalName.Text = location.Name;
					txtLocalName.ResignFirstResponder();
					InputLocalReturn(txtLocalName);
				}
			}
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "AboutThisMoment".Translate();
			btnSave.SetTitle("Save".Translate(), UIControlState.Normal);
			txtLocalName.Placeholder = "WhatsPlaceName".Translate();
			txtDescription.Placeholder = "MomentRemember".Translate();
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();

			vwDate.Hide();
			InputLocalReturn(txtLocalName);
		}

		/// <summary>
		/// Gos the back to events.
		/// </summary>
		public void GoBackToEvents()
		{
			LeftBarButtonAction();
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			FlurryAnalytics.Flurry.LogEvent("Momento: Salvou momento.");

			ShowProgressWhilePerforming(() => {
				var moment = CurrentContext.Instance.Moment;
				moment.Description = txtDescription.Text;
				moment.Event = CurrentContext.Instance.SelectedEvent;

				moment.Babies = new List<Baby> { CurrentContext.Instance.CurrentBaby };

				moment.Date = vwDate.DateTime;
				moment.Language = SHCultureInfo.From(NSLocale.CurrentLocale).Name;
				moment.MediaCount = moment.SelectedMediaNames.Count;

				moment.Position = m_userLocation.Position;

				moment.Location = new Location() {
					Name = txtLocalName.Text,
					Position = moment.Position
				};

				((MomentNavigationController) NavigationController).SaveCurrentMoment();
			}, false);
		}
	}
}