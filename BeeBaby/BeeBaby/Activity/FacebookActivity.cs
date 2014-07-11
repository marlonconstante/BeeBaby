using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BeeBaby.Activity
{
	public class FacebookActivity : UIActivity
	{
		UIImage m_image;

		public FacebookActivity(UIImage image)
		{
			m_image = image;
		}

		private NSUrl _url;

		public override string Title
		{
			get
			{
				return "Facebook";
			}
		}

		public override NSString Type
		{
			get
			{
				return (NSString)"com.beebaby.facebook";
			}
		}

		public override UIImage Image
		{
			get
			{
				return UIImage.FromBundle("Icon.png");
			}
		}

		public override bool CanPerform(NSObject[] activityItems)
		{
			return true;
		}

		public override void Prepare(NSObject[] activityItems)
		{
			foreach (var item in activityItems)
			{
				if (item is NSUrl)
				{
					_url = item as NSUrl;
				}
			}
		}

		public override void Perform()
		{
			this.Finished(true);
		}
	}
}

