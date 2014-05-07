using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using Application;
using Domain.Moment;
using BeeBaby.Util;

namespace BeeBaby
{
	public partial class BabyViewController : NavigationViewController
	{
		public BabyViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			ViewBirthDay.UpdateInfo();
			ViewBirthTime.UpdateInfo();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblName.Text = "WhatsBabyName".Translate();
			lblBirthDate.Text = "WhenWasHeBorn".Translate();
			segGender.SetTitle("Male".Translate(), 0);
			segGender.SetTitle("Female".Translate(), 1);
			segGender.SetTitle("Unknown".Translate(), 2);
		}

		/// <summary>
		/// Starts the editing.
		/// </summary>
		public override void StartEditing()
		{
			base.StartEditing();

			ViewBirthDay.Hide();
			ViewBirthTime.Hide();
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();

			ViewBirthDay.Hide();
			ViewBirthTime.Hide();
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			ShowProgressWhilePerforming(() => {
				var babyService = new BabyService();
				var baby = new Baby();
				var birthDateTime = ViewBirthDay.GetText("dd/MM/yyyy") + " " + ViewBirthTime.GetText("HH:mm");

				baby.Name = txtName.Text;
				baby.Gender = (Gender) segGender.SelectedSegment;
				baby.BirthDateTime = DateTime.ParseExact(birthDateTime, "dd/MM/yyyy HH:mm", null).ToUniversalTime();

				babyService.SaveBaby(baby);

				CurrentContext.Instance.CurrentBaby = baby;
				CurrentContext.Instance.Moment.Babies.Add(baby);

				PreferencesEditor.SaveLastUsedBaby(baby.Id);

				new MomentService().SaveMoment(CurrentContext.Instance.Moment);

				PerformSegue("segueMoment", sender);
			}, false);
		}

		/// <summary>
		/// Gets the view birth day.
		/// </summary>
		/// <value>The view birth day.</value>
		public ViewDatePicker ViewBirthDay {
			get {

				return (ViewDatePicker) vwBirthDay;
			}
		}

		/// <summary>
		/// Gets the view birth time.
		/// </summary>
		/// <value>The view birth time.</value>
		public ViewDatePicker ViewBirthTime {
			get {

				return (ViewDatePicker) vwBirthTime;
			}
		}
	}
}