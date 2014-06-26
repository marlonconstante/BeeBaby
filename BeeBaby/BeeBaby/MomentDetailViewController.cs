using System;
using System.Linq;
using MonoTouch.UIKit;
using Domain.Moment;
using Application;
using System.Drawing;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;
using System.Threading;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.CoreLocation;
using PixateFreestyleLib;

namespace BeeBaby
{
	public partial class MomentDetailViewController : NavigationViewController
	{
		PlaceholderTextViewDelegate m_txtDescriptionDelegate;
		UITableView m_autoCompleteTable;
		string[] m_wordCollection;
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

			vwDate.Init(UIDatePickerMode.DateAndTime);

			m_txtDescriptionDelegate = new PlaceholderTextViewDelegate();
			txtDescription.Delegate = m_txtDescriptionDelegate;
			txtDescription.IsKeyboardAnimation = true;

			txtLocalName.IsKeyboardAnimation = true;
			txtLocalName.OffsetHeight = 131f;

			mapView.Delegate = new ZoomMapViewDelegate(0.001d, this);

			m_autoCompleteTable = new UITableView(new RectangleF(0f, 0f, 320f, 131f));
			m_autoCompleteTable.ExclusiveTouch = true;
			m_autoCompleteTable.ScrollEnabled = true;

			var autoCompleteView = new UIView(new RectangleF(0f, (txtLocalName.Frame.Y + txtLocalName.Frame.Height) - 10, 320f, 131f));
			autoCompleteView.SetStyleClass("row");
			autoCompleteView.Hidden = true;
			autoCompleteView.AddSubview(m_autoCompleteTable);
			View.AddSubview(autoCompleteView);

			m_locations = new LocationService().GetAllLocations();
			m_wordCollection = m_locations.Select(l => l.Name).ToArray<string>();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			FlurryAnalytics.Flurry.LogEvent("Momento: Cadastro.", true);

			base.ViewWillAppear(animated);

			vwDate.UpdateInfo();

			txtLocalName.ShouldReturn += InputLocalShouldReturn;
			txtLocalName.ShouldChangeCharacters += InputLocalShouldChangeCharacters;

			Event selectedEvent = CurrentContext.Instance.SelectedEvent;
			if (selectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = selectedEvent;
				btnSelectEvent.SetTitle(selectedEvent.Description, UIControlState.Normal);
			}
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Momento: Cadastro.", null);

			base.ViewWillDisappear(animated);

			txtLocalName.ShouldReturn -= InputLocalShouldReturn;
			txtLocalName.ShouldChangeCharacters -= InputLocalShouldChangeCharacters;
		}

		/// <summary>
		/// Loads the near location.
		/// </summary>
		/// <param name="coordinate">Coordinate.</param>
		public void LoadNearLocation(CLLocationCoordinate2D coordinate)
		{
			//Atualizar uma vez...
			var currentPlace = new CLLocation(coordinate.Latitude, coordinate.Longitude);
			foreach (var location in m_locations)
			{
				var place = new CLLocation(location.Position.Latitude, location.Position.Longitude);
				if (place.DistanceFrom(currentPlace) <= 200d)
				{
					FlurryAnalytics.Flurry.LogEvent("Momento: GPS Localizou automatico.");

					SetAutoCompleteText(location.Name);
					break;
				}
			}
		}

		/// <summary>
		/// Inputs the local should return.
		/// </summary>
		/// <returns><c>true</c>, if local should return was input, <c>false</c> otherwise.</returns>
		/// <param name="textField">Text field.</param>
		public bool InputLocalShouldReturn(UITextField textField)
		{
			m_autoCompleteTable.Superview.Hidden = true;

			return true;
		}

		/// <summary>
		/// Inputs the local should change characters.
		/// </summary>
		/// <returns><c>true</c>, if local should change characters was input, <c>false</c> otherwise.</returns>
		/// <param name="textField">Text field.</param>
		/// <param name="range">Range.</param>
		/// <param name="replacementString">Replacement string.</param>
		public bool InputLocalShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
		{
			string[] suggestions = null;

			var source = replacementString != string.Empty ? string.Concat(textField.Text, replacementString).ToLowerInvariant() : textField.Text.Substring(0, textField.Text.Length - 1).ToLowerInvariant();
			try
			{
				suggestions = m_wordCollection.Where(x => x.ToLowerInvariant().Contains(source))
					.OrderByDescending(x => x.StartsWith(source, StringComparison.InvariantCultureIgnoreCase))
					.Select(x => x).ToArray();

				if (suggestions.Length != 0)
				{
					m_autoCompleteTable.Superview.Hidden = false;
					m_autoCompleteTable.Source = new AutoCompleteTableSource(suggestions, this);
					m_autoCompleteTable.ReloadData();
				}
				else
				{
					m_autoCompleteTable.Superview.Hidden = true;
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Erro ao buscar as sugestões.");
			}

			return true;
		}

		/// <summary>
		/// Sets the auto complete text.
		/// </summary>
		/// <param name="finalString">Final string.</param>
		public void SetAutoCompleteText(string finalString)
		{
			txtLocalName.Text = finalString;
			var location = new LocationService().GetLocationByName(finalString);
			mapView.SetCenterCoordinate(new MonoTouch.CoreLocation.CLLocationCoordinate2D(location.Position.Latitude, location.Position.Longitude), true);
			m_autoCompleteTable.Superview.Hidden = true;
			txtLocalName.ResignFirstResponder();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			TitleScreen = "Moment".Translate();
			lblMomentAbout.Text = "MomentAbout".Translate();
			btnSelectEvent.SetTitle("SelectEvent".Translate(), UIControlState.Normal);
			btnSave.SetTitle("Save".Translate(), UIControlState.Normal);
			txtDescription.Text = "MomentRemember".Translate();
		}

		/// <summary>
		/// Starts the editing.
		/// </summary>
		public override void StartEditing()
		{
			base.StartEditing();

			vwDate.Hide();
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();
			vwDate.Hide();
			m_autoCompleteTable.Superview.Hidden = true;
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			FlurryAnalytics.Flurry.LogEvent("Momento: Salvou momento.");

			ShowProgressWhilePerforming(() => {
				var imageProvider = new ImageProvider(CurrentContext.Instance.Moment.Id);
				var momentService = new MomentService();
				var moment = CurrentContext.Instance.Moment;

				moment.Description = m_txtDescriptionDelegate.Placeholder.GetInitialText(txtDescription.Text);
				moment.Event = CurrentContext.Instance.SelectedEvent;
				moment.Babies.Add(CurrentContext.Instance.CurrentBaby);

				moment.Date = vwDate.DateTime;

				moment.Position = new Coordinates();
				moment.Position.Latitude = mapView.UserLocation.Coordinate.Latitude;
				moment.Position.Longitude = mapView.UserLocation.Coordinate.Longitude;

				var location = new Location() {
					Name = txtLocalName.Text,
					Position = new Coordinates() {
						Latitude = moment.Position.Latitude,
						Longitude = moment.Position.Longitude
					}
				};

				location = new LocationService().SaveLocation(location);
				moment.Location = location;

				imageProvider.SavePermanentImages(moment.SelectedMediaNames);
				momentService.SaveMoment(moment);

				CurrentContext.Instance.Moment = null;
				CurrentContext.Instance.SelectedEvent = null;

				PresentingViewController.DismissViewController(false, null);
				Discard.ReleaseNavigation(NavigationController);
			}, false);
				
			if (RootViewController.GetType() == typeof(SlideoutNavigationController))
			{
				var slideoutNavigation = (SlideoutNavigationController) RootViewController;
				var menu = (MenuViewController) slideoutNavigation.MenuViewLeft;
				menu.SyncMoment();
			}

		}
	}
}