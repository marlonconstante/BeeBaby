using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using Application;

namespace BeeBaby
{
	public partial class BabyViewController : ViewController
	{
		public BabyViewController(IntPtr handle) : base(handle)
		{
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		public override void TranslateLabels()
		{
			lblName.Text = "WhatsBabyName".Translate();
			lblGender.Text = "Gender".Translate();
			lblBirthDay.Text = "WhatDayHeWasBorn".Translate();
			lblBirthTime.Text = "Schedule".Translate();
			segGender.SetTitle("Male".Translate(), 0);
			segGender.SetTitle("Female".Translate(), 1);
		}

		/// <summary>
		/// Selects the birth day.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectBirthDay(UIButton sender)
		{
		}

		/// <summary>
		/// Selects the birth time.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void SelectBirthTime(UIButton sender)
		{
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

				baby.Name = txtName.Text;
				baby.Gender = (Gender) segGender.SelectedSegment + 1;
				//baby.BirthDateTime = pckBirthDate.Date;

				CurrentContext.Instance.Baby = baby;

				babyService.SaveBaby(baby);

				PerformSegue("segueMoment", sender);
			}, false);
		}
	}
}