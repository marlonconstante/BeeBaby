﻿using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.CoreGraphics;
using System.IO;

namespace BeeBaby.Activity
{
	public class WhatsAppActivity : UIActivity
	{

		readonly string WhatsAppURL ="whatsapp://app";

		public WhatsAppActivity ()
		{
		}

		public override bool CanPerform(NSObject[] activyItems)
		{
			base.CanPerform(activyItems);

			var Url = new NSUrl(WhatsAppURL);
			if (!UIApplication.SharedApplication.CanOpenUrl(Url))
				return false;

			foreach (var item in activyItems)
			{
				var image = item as UIImage; 
				if (image != null && IsImageLargeEnough(image))
				{
					return true;
				}
				else
				{
					//TODO: Log message
				}
			}

			return false;
		}

		public override void Prepare(NSObject[] activyItems)
		{
			base.Prepare(activyItems);

			foreach (var item in activyItems)
			{
				if (item.GetType() == typeof(UIImage))
				{
					ShareImage = (UIImage) item;
				}
				else if (item.GetType() == typeof(NSString))
				{
					ShareString = 
						!string.IsNullOrEmpty(ShareString) 
						? string.Format("{0} {1}", ShareString, item.ToString()) 
						: item.ToString();
				}
				else if (item.GetType() == typeof(NSUrl))
				{
					if (IncludeURL)
						ShareString += !string.IsNullOrEmpty(ShareString) ? " " + ((NSUrl) item).AbsoluteString : ((NSUrl) item).AbsoluteString;
				}
				else
				{
					//TODO: Log message: "Unknown item type %@", item"
				}
			}
		}

		/// <Docs>Performs the service when no custom UIViewController is provided.</Docs>
		/// <summary>
		/// Perform this instance.
		/// </summary>
		public override void Perform()
		{
			var cropVal = ShareImage.Size.Height > ShareImage.Size.Width ? ShareImage.Size.Width : ShareImage.Size.Height;
			cropVal *= ShareImage.CurrentScale;

			var cropRect = new RectangleF {
				Height = cropVal,
				Width = cropVal
			};

			var imageRef = ShareImage.CGImage.WithImageInRect(cropRect);
			var image = UIImage.FromImage(imageRef);
			var imageData = image.AsJPEG(1.0f);

			//TODO: Find way to release image
			imageRef.Dispose();

			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var fullUrl = Path.Combine(path, "whatsapp.wai"); 

			Console.WriteLine("Full Url: " + fullUrl);

			NSError error;
			if (!imageData.Save(fullUrl, NSDataWritingOptions.Atomic, out error))
			{
				Console.WriteLine("Error saving the image: " + fullUrl);
				return;
			}

			var fileUrl = NSUrl.FromFilename(fullUrl);
			DocumentController = UIDocumentInteractionController.FromUrl(fileUrl);
			DocumentController.Delegate = new DocumentInteractionControllerDelegate(this);
			DocumentController.Uti = "com.whatsapp.image";

//			if (!string.IsNullOrEmpty(ShareString))
//			{
//				var dict = new NSMutableDictionary();
//				dict.Add((NSString) "WhastAppCaption", (NSString) ShareString);
//				DocumentController.Annotation = dict;
//			}

			base.Perform();
		}

		/// <summary>
		/// Determines whether this instance is image large enough the specified image.
		/// </summary>
		/// <returns><c>true</c> if this instance is image large enough the specified image; otherwise, <c>false</c>.</returns>
		/// <param name="image">Image.</param>
		bool IsImageLargeEnough(UIImage image)
		{
			var imageSize = image.Size;
			return ((imageSize.Height * image.CurrentScale) >= 612 && (imageSize.Width * image.CurrentScale) >= 612);
		}

		/// <summary>
		/// Determines whether this instance is image square the specified image.
		/// </summary>
		/// <returns><c>true</c> if this instance is image square the specified image; otherwise, <c>false</c>.</returns>
		/// <param name="image">Image.</param>
		bool IsImageSquare(UIImage image)
		{
			var imageSize = image.Size;
			return (imageSize.Height == imageSize.Width);
		}

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>
		public override string Title {
			get { return "WhatsApp"; }
		}

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public override NSString Type {
			get { return (NSString) "UIActivityTypePostToWhatsApp"; }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public override UIImage Image {
			get { return new UIImage("whatsapp.png"); }
		}

		/// <summary>
		/// Gets or sets the share image.
		/// </summary>
		/// <value>The share image.</value>
		public UIImage ShareImage { get; set; }

		/// <summary>
		/// Gets or sets the share string.
		/// </summary>
		/// <value>The share string.</value>
		public string ShareString { get; set; }

		/// <summary>
		/// Gets or sets the background colors.
		/// </summary>
		/// <value>The background colors.</value>
		public string BackgroundColors { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BeeBaby.Activity.InstagramActivity"/> include UR.
		/// </summary>
		/// <value><c>true</c> if include UR; otherwise, <c>false</c>.</value>
		public bool IncludeURL { get; set; }

		/// <summary>
		/// Gets or sets the document controller.
		/// </summary>
		/// <value>The document controller.</value>
		public UIDocumentInteractionController DocumentController { get; set; }

		/// <summary>
		/// Gets or sets the present from view.
		/// </summary>
		/// <value>The present from view.</value>
		public UIView PresentFromView { get; set; }
	}
}


