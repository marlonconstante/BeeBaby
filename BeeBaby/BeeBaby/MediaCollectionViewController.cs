using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BeeBaby.ResourcesProviders;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Globalization;
using Application;
using BeeBaby.ViewModels;
using BigTed;

namespace BeeBaby
{
	public partial class MediaCollectionViewController : UIViewController
	{
		private UIImagePickerController m_picker;
		private ImageCollectionViewSource m_collectionViewSource;

		public MediaCollectionViewController(IntPtr handle) : base(handle)
		{
			m_collectionViewSource = new ImageCollectionViewSource(this);
		}

		/// <summary>
		/// Buttons the add media from library.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void AddMediaFromLibrary(UIButton sender)
		{
			var imagePickerProvider = new MediaPickerProvider(UIImagePickerControllerSourceType.SavedPhotosAlbum);
			m_picker = imagePickerProvider.GetUIImagePickerController();

			PresentViewController(m_picker, false, null);
		}

		/// <summary>
		/// Buttons the next step.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void NextStep(UIButton sender)
		{
			// Shows the spinner
			BTProgressHUD.Show();

			PerformSegue("segueMoment", sender);
		}

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			clnView.Source = m_collectionViewSource;

			btnNextStep.SetTitle("Next".Translate(), UIControlState.Normal);
			btnAddMediaFromLibrary.SetTitle("AddFromLib".Translate(), UIControlState.Normal);

			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			m_collectionViewSource.ReloadData(clnView);
			base.ViewWillAppear(animated);
		}
	}
}
