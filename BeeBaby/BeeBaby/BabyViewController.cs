// This file has been autogenerated from a class added in the UI designer.

using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skahal.Infrastructure.Framework.Globalization;
using BigTed;
using Domain.Baby;
using Application;

namespace BeeBaby
{
	public partial class BabyViewController : UIViewController
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

			TranslateLabels();

			txtName.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};

			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Translates the labels.
		/// </summary>
		void TranslateLabels()
		{
			lblName.Text = "Name".Translate();
			lblGender.Text = "Gender".Translate();
			lblBirthDate.Text = "BirthDate".Translate();
		}

		/// <summary>
		/// Save the moment.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void Save(UIButton sender)
		{
			// Shows the spinner
			BTProgressHUD.Show(); 

			var baby = new Baby();
			baby.Name = txtName.Text;
			baby.Gender = (Gender) segGender.SelectedSegment + 1;
			baby.BirthDateTime = pckBirthDate.Date;

			//TODO: Colocar chamado do serviço
			CurrentContext.Instance.Baby = baby;

			PerformSegue("segueMoment", sender);
		}
	}
}