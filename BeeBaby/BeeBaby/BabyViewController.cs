using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using Domain.Baby;
using Application;
using Domain.Moment;

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

				CurrentContext.Instance.CurrentBaby = baby;

				babyService.SaveBaby(baby);

				CurrentContext.Instance.Moment.Babies.Add(baby);
				new MomentService().SaveMoment(CurrentContext.Instance.Moment);

				PerformSegue("segueMoment", sender);
			}, false);
		}
	}
}