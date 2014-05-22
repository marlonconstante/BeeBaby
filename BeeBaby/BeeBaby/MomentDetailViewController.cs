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
			base.ViewDidLoad();

			m_txtDescriptionDelegate = new PlaceholderTextViewDelegate();
			txtDescription.Delegate = m_txtDescriptionDelegate;
			txtDescription.IsKeyboardAnimation = true;

			mapView.Delegate = new ZoomMapViewDelegate(0.001d, this);

			m_autoCompleteTable = new UITableView(new RectangleF(0, 64, 320, 120));
			m_autoCompleteTable.ScrollEnabled = true;
			m_autoCompleteTable.Hidden = true;

			txtLocalName.ShouldReturn += (textField) =>
			{ 
				m_autoCompleteTable.Hidden = true;
				return true; 
			};

			this.View.AddSubview(m_autoCompleteTable);

			txtLocalName.ShouldChangeCharacters += (sender, something, e) =>
			{
				UpdateSuggestions(e);
				return true;
			};

			m_locations = new LocationService().GetAllLocations();
			wordCollection = m_locations.Select(l => l.Name).ToArray<string>();

			if (mapView.UserLocation.Location != null)
			{
				LoadNearLocation();
			}
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
		/// Updates the suggestions.
		/// </summary>
		/// <param name="e">E.</param>
		public void UpdateSuggestions(string e)
		{
			string[] suggestions = null;

			var source = e != string.Empty ? string.Concat(txtLocalName.Text, e).ToLowerInvariant() : txtLocalName.Text.Substring(0, txtLocalName.Text.Length - 1).ToLowerInvariant();

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
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			vwDate.UpdateInfo();
		}

		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			Event selectedEvent = CurrentContext.Instance.SelectedEvent;
			if (selectedEvent != null)
			{
				CurrentContext.Instance.Moment.Event = selectedEvent;
				btnSelectEvent.SetTitle(selectedEvent.Description, UIControlState.Normal);
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
		/// Selects the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectEvent(UIButton sender)
		{
			ShowProgressWhilePerforming(() =>
			{
				PerformSegue("segueSelectEvent", sender);
			}, false);
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			ShowProgressWhilePerforming(() =>
			{
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

				var location = new Location()
				{
					Name = txtLocalName.Text,
					Position = new Coordinates()
					{
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

				NavigationController.PerformSegue("segueTimeline", sender);
				DismissViewController(true, null);
			}, false);
		}
	}
}