using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using Application;
using System.Collections.Generic;

namespace BeeBaby
{
	public partial class BabyViewController : NavigationViewController
	{
		public BabyViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			vwBirthDay.MoveScroll = true;
			vwBirthTime.MoveScroll = true;

			vwBirthDay.NextViews = new List<UIView> { vwBirthTime };

			Load(CurrentContext.Instance.CurrentBaby);
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			vwBirthDay.UpdateInfo();
			vwBirthTime.UpdateInfo();
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

			vwBirthDay.Hide();
			vwBirthTime.Hide();
		}

		/// <summary>
		/// Ends the editing.
		/// </summary>
		public override void EndEditing()
		{
			base.EndEditing();

			vwBirthDay.Hide();
			vwBirthTime.Hide();
		}

		/// <summary>
		/// Load the specified baby.
		/// </summary>
		/// <param name="baby">Baby.</param>
		void Load(Baby baby)
		{
			txtName.Text = baby.Name;
			segGender.SelectedSegment = (int) baby.Gender;
			vwBirthDay.DateTime = baby.BirthDateTime;
			vwBirthTime.DateTime = baby.BirthDateTime;
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			var containsMenu = IsContainsMenu();
			ShowProgressWhilePerforming(() => {
				var babyService = new BabyService();
				var baby = CurrentContext.Instance.CurrentBaby;
				var birthDateTime = vwBirthDay.GetText("dd/MM/yyyy") + " " + vwBirthTime.GetText("HH:mm");

				baby.Name = txtName.Text;
				baby.Gender = (Gender) segGender.SelectedSegment;
				baby.BirthDateTime = DateTime.ParseExact(birthDateTime, "dd/MM/yyyy HH:mm", null);

				babyService.SaveBaby(baby);

				if (!containsMenu)
				{
					PerformSegue("segueMoment", sender);
				}
			}, containsMenu);
		}
	}
}