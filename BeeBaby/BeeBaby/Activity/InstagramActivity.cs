using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BeeBaby.Activity
{
	public class InstagramActivity : UIActivity
	{
		public InstagramActivity()
		{
		}

		public UIImage ShareImage
		{
			get;
			set;
		}

		public string ShareString
		{
			get;
			set;
		}

		public override string Title
		{
			get
			{
				return "Instagram";
			}
		}

		public override NSString Type
		{
			get
			{
				return (NSString)"UIActivityTypePostToInstagram";
			}
		}

		public override UIImage Image
		{
			get
			{
				return UIImage.FromBundle("instagram.png");
			}
		}

		public override bool CanPerform(NSObject[] activityItems)
		{
			var instagramURL = NSUrl.FromString("instagram://app");
			return UIApplication.SharedApplication.CanOpenUrl(instagramURL); // no instagram.
		}

		public override void Prepare(NSObject[] activityItems)
		{
			foreach (var item in activityItems)
			{


				if (item is UIImage)
					ShareImage = item as UIImage;
				else if (item is NSString)
				{
					ShareString = String.Concat((ShareImage != null ? String.Concat(ShareString, " ") : ""), item); // concat, with space if already exists.
				}
			}
		}

		public override void Perform()
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Guid.NewGuid() + ".igo");
			using (NSData imageData = imgPhoto.Image.AsJPEG(MediaBase.ImageCompressionQuality))
			{
				NSError err;
				if (!imageData.Save(path, false, out err))
				{
					Console.WriteLine("Saving of file failed: " + err.Description);
				}
			}


			// send it to instagram.
			var fileURL = NSUrl.FromString(path);
			var documentController = new UIDocumentInteractionController();
			documentController.Url = fileURL;

			documentController.Delegate = self;
			documentController.Uti = "com.instagram.exclusivegram";
//			if (self.shareString) [self.documentController setAnnotation:@{@"InstagramCaption" : self.shareString}];

			documentController.PresentOpenInMenu(

			if (![self.documentController presentOpenInMenuFromBarButtonItem:self.presentFromButton animated:YES]) NSLog(@"couldn't present document interaction controller");


			this.Finished(true);
		}
	}
}

