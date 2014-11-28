using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using Application;
using System.Collections.Generic;
using System.Drawing;
using BigTed;
using BeeBaby.Navigations;
using BeeBaby.Util;

namespace BeeBaby.Controllers
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

			Load(CurrentContext.Instance.CurrentBaby);

			vwBirthDay.Init(UIDatePickerMode.Date);
			vwBirthTime.Init(UIDatePickerMode.Time);
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
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			FlurryAnalytics.Flurry.LogEvent("Baby: Cadastro do Bebe", true);
			base.ViewDidAppear(animated);
		}

		/// <summary>
		/// Views the will disappear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			FlurryAnalytics.Flurry.EndTimedEvent("Baby: Cadastro do Bebe", null);
			base.ViewWillDisappear(animated);

			Save();
		}

		/// <summary>
		/// Views the did layout subviews.
		/// </summary>
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (scrView.ContentSize == SizeF.Empty)
			{
				scrView.ContentSize = new SizeF(320f, 455f);
			}
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblName.Text = "WhatsBabyName".Translate();
			lblUser.Text = "WhatsUserName".Translate();
			lblBirthDate.Text = "WhenWasHeBorn".Translate();
			txtName.Placeholder = "EnterBabyName".Translate();
			txtUser.Placeholder = "EnterUserName".Translate();
			segGender.SetTitle("Male".Translate(), 0);
			segGender.SetTitle("Female".Translate(), 1);
			segGender.SetTitle("Unknown".Translate(), 2);
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
			txtUser.Text = baby.Email;
			segGender.SelectedSegment = (int) baby.Gender;
			vwBirthDay.DateTime = baby.BirthDateTime;
			vwBirthTime.DateTime = baby.BirthDateTime;
		}

		/// <summary>
		/// Save the baby.
		/// </summary>
		void Save()
		{
			var babyService = new BabyService();
			var baby = CurrentContext.Instance.CurrentBaby;
			var birthDateTime = vwBirthDay.GetText("dd/MM/yyyy") + " " + vwBirthTime.GetText("HH:mm");
			var previousHashCode = baby.GetHashCode();

			baby.Name = txtName.Text;
			baby.Email = txtUser.Text;
			baby.Gender = (Gender) segGender.SelectedSegment;
			baby.BirthDateTime = DateTime.ParseExact(birthDateTime, "dd/MM/yyyy HH:mm", null);

			if (previousHashCode != baby.GetHashCode())
			{
				babyService.SaveBaby(baby);
				CurrentContext.Instance.ReloadMoments = true;
			}
		}
	}
}