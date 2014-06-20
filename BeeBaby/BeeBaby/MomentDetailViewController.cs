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

namespace BeeBaby
{
	public partial class MomentDetailViewController : NavigationViewController
	{
		PlaceholderTextViewDelegate m_txtDescriptionDelegate;
		UITableView m_autoCompleteTable;
		string[] wordCollection;
		IEnumerable<Location> m_locations;

		public MomentDetailViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			FlurryAnalytics.Flurry.LogEvent("Momento: Cadastro.", true);
			base.ViewDidLoad();

			vwDate.Init(UIDatePickerMode.DateAndTime);

			m_txtDescriptionDelegate = new PlaceholderTextViewDelegate();
			txtDescription.Delegate = m_txtDescriptionDelegate;
			txtDescription.IsKeyboardAnimation = true;

			mapView.Delegate = new ZoomMapViewDelegate(0.001d, this);

			m_autoCompleteTable = new UITableView(new RectangleF(0, 64, 320, 120));
			m_autoCompleteTable.ScrollEnabled = true;
			m_autoCompleteTable.Hidden = true;

			this.View.AddSubview(m_autoCompleteTable);

			m_locations = new LocationService().GetAllLocations();
			wordCollection = m_locations.Select(l => l.Name).ToArray<string>();

			if (mapView.UserLocation.Location != null)
			{
				LoadNearLocation();
			}
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Momento: Cadastro.", null);
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
			base.ViewWillDisappear(animated);

			txtLocalName.ShouldReturn -= InputLocalShouldReturn;
			txtLocalName.ShouldChangeCharacters -= InputLocalShouldChangeCharacters;
		}

		/// <summary>
		/// Loads the near location.
		/// </summary>
		public void LoadNearLocation()
		{
			var currentPlace = new Coordinates(mapView.UserLocation.Location.Coordinate.Latitude, mapView.UserLocation.Location.Coordinate.Longitude);
			var nearest = m_locations.OrderBy(l => l.Position.DistanceFrom(currentPlace)).FirstOrDefault();

			if (nearest != null && currentPlace.DistanceFrom(nearest.Position) <= 100)
			{
				SetAutoCompleteText(nearest.Name);
			}
		}

		/// <summary>
		/// Inputs the local should return.
		/// </summary>
		/// <returns><c>true</c>, if local should return was input, <c>false</c> otherwise.</returns>
		/// <param name="textField">Text field.</param>
		public bool InputLocalShouldReturn(UITextField textField)
		{
			m_autoCompleteTable.Hidden = true;

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
				suggestions = wordCollection.Where(x => x.ToLowerInvariant().Contains(source))
					.OrderByDescending(x => x.StartsWith(source, StringComparison.InvariantCultureIgnoreCase))
					.Select(x => x).ToArray();

				if (suggestions.Length != 0)
				{
					m_autoCompleteTable.Hidden = false;
					m_autoCompleteTable.Source = new AutoCompleteTableSource(suggestions, this);
					m_autoCompleteTable.ReloadData();
				}
				else
				{
					m_autoCompleteTable.Hidden = true;
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
			txtLocalName.ResignFirstResponder();
			m_autoCompleteTable.Hidden = true;
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
			m_autoCompleteTable.Hidden = true;
			vwDate.Hide();
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
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
		}
	}
}