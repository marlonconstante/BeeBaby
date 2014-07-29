using System;
using System.Linq;
using MonoTouch.UIKit;
using Domain.Moment;
using Application;
using System.Drawing;
using BeeBaby.ResourcesProviders;
using Skahal.Infrastructure.Framework.Globalization;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.CoreLocation;
using PixateFreestyleLib;
using System.IO;

namespace BeeBaby
{
	public partial class MomentDetailViewController : NavigationViewController
	{
		ZoomMapViewDelegate m_mapViewDelegate;
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
			proxy.Action = (target, sender, args) =>
			{
				target.InputLocalReturn(target.txtLocalName);
			};
			vwDate.Clicked += proxy.HandleEvent;
			vwDate.Init(UIDatePickerMode.DateAndTime);

			txtLocalName.OffsetHeight = tblView.Frame.Height;

			m_mapViewDelegate = new ZoomMapViewDelegate(this, 0.001d, IsCameraFlow());
			mapView.Delegate = m_mapViewDelegate;

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

			Event selectedEvent = CurrentContext.Instance.SelectedEvent;
			if (selectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = selectedEvent;
				btnSelectEvent.SetTitle(selectedEvent.Description, UIControlState.Normal);
			}

			if (IsEventFlow())
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
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Momento: Cadastro.", null);

			base.ViewWillDisappear(animated);

			m_mapViewDelegate.UpdateUserLocation = false;

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
			scrView.ContentSize = new SizeF(320f, 504f);
		}

		/// <summary>
		/// Loads the near location.
		/// </summary>
		public void LoadNearLocation()
		{
			var currentPlace = new CLLocation(mapView.CenterCoordinate.Latitude, mapView.CenterCoordinate.Longitude);
			foreach (var location in m_locations)
			{
				var place = new CLLocation(location.Position.Latitude, location.Position.Longitude);
				if (place.DistanceFrom(currentPlace) <= 200d)
				{
					FlurryAnalytics.Flurry.LogEvent("Momento: GPS Localizou automatico.");
					SelectLocation(location);
					break;
				}
			}
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
			m_mapViewDelegate.UpdateUserLocation = false;

			if (location == null)
			{
				m_mapViewDelegate.LoadUserLocation(mapView);
			}
			else
			{
				mapView.CenterCoordinate = new CLLocationCoordinate2D(location.Position.Latitude, location.Position.Longitude);

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
			TitleScreen = "Moment".Translate();
			lblMomentAbout.Text = "MomentAbout".Translate();
			btnSelectEvent.SetTitle("SelectEvent".Translate(), UIControlState.Normal);
			btnSave.SetTitle("Save".Translate(), UIControlState.Normal);
			txtLocalName.Placeholder = "WhatsPlaceName".Translate();
			txtDescription.Placeholder = "MomentRemember".Translate();
		}

		/// <summary>
		/// Starts the editing.
		/// </summary>
		public override void StartEditing()
		{
			base.StartEditing();

			vwDate.Hide();
			InputLocalReturn(txtLocalName);
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
		/// <param name="sender">Sender.</param>
		partial void GoBackToEvents(UIButton sender)
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
				moment.Babies.Add(CurrentContext.Instance.CurrentBaby);

				moment.Date = vwDate.DateTime;
				moment.MediaCount = moment.SelectedMediaNames.Count;

				moment.Position = new Coordinates();
				moment.Position.Latitude = mapView.CenterCoordinate.Latitude;
				moment.Position.Longitude = mapView.CenterCoordinate.Longitude;

				var location = new Location() {
					Name = txtLocalName.Text,
					Position = new Coordinates() {
						Latitude = moment.Position.Latitude,
						Longitude = moment.Position.Longitude
					}
				};

				location = new LocationService().SaveLocation(location);
				moment.Location = location;

				new ImageProvider(moment.Id).SavePermanentImages(moment.SelectedMediaNames);
				new MomentService().SaveMoment(moment);

				if (RootViewController.GetType() == typeof(SlideoutNavigationController))
				{
					var slideoutNavigation = (SlideoutNavigationController) RootViewController;
					var menu = (MenuViewController) slideoutNavigation.MenuViewLeft;
					menu.SyncMoment(CurrentContext.Instance.Moment);
				}

				CurrentContext.Instance.ReloadMoments = true;
				((MomentNavigationController) NavigationController).Close();
			}, false);
		}
	}
}