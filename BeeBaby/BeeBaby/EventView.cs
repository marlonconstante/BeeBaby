using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using Application;
using Domain.Media;
using PixateFreestyleLib;
using Skahal.Infrastructure.Framework.Globalization;

namespace BeeBaby
{
	public partial class EventView : AvatarView
	{
		public EventView(IntPtr handle) : base(handle, AvatarTemplate.PhotoAndDescription)
		{
		}

		public EventView(RectangleF frame) : base(frame, AvatarTemplate.PhotoAndDescription)
		{
		}

		/// <summary>
		/// Gets the name of the background image.
		/// </summary>
		/// <returns>The background image name.</returns>
		protected override string GetBackgroundImageName()
		{
			return "background-event";
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <returns>The description.</returns>
		protected override string GetDescription()
		{
			return CurrentContext.Instance.SelectedEvent.Description;
		}

		/// <summary>
		/// Action the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		protected override void Action(UIView sender)
		{
			var viewController = Windows.GetTopViewController(Window);
			if (viewController is MomentDetailViewController)
			{
				((MomentDetailViewController) viewController).GoBackToEvents();
			}
		}

		/// <summary>
		/// Builds the photo.
		/// </summary>
		/// <returns>The photo.</returns>
		protected override UIView BuildPhoto()
		{
			var x = (InitialFrame.Width / 2f) - (MediaBase.BadgeEventSize / 2f);
			var y = 35f;

			var view = new UIView(new RectangleF(x, y, MediaBase.BadgeEventSize, MediaBase.BadgeEventSize));
			view.SetStyleClass("badge-event");
			view.AddSubview(BuildImageViewEvent());

			return view;
		}

		/// <summary>
		/// Redraw this instance.
		/// </summary>
		public override void Redraw()
		{
			base.Redraw();

			var label = new Label(new RectangleF(115f, 0f, 200f, 22f));
			label.Text = "SwapEvent".Translate();
			label.SetStyleClass("change-event-label");
			label.TextAlignment = UITextAlignment.Right;
			AddSubview("change-event", label);
		}

		/// <summary>
		/// Builds the image view event.
		/// </summary>
		/// <returns>The image view event.</returns>
		UIImageViewClickable BuildImageViewEvent()
		{
			var position = (MediaBase.BadgeEventSize - MediaBase.BadgeEventInnerSize) / 2;
			var frame = new RectangleF(position, position, MediaBase.BadgeEventInnerSize, MediaBase.BadgeEventInnerSize);

			var image = UIImage.FromFile(CurrentContext.Instance.SelectedEvent.BadgeFileName);
			var imageView = new UIImageViewClickable(frame);
			imageView.Layer.CornerRadius = MediaBase.BadgeEventInnerSize / 2;

			UpdateImageView(imageView, image);

			return imageView;
		}
	}
}