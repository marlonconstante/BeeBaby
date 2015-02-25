using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using Application;
using Domain.Media;
using PixateFreestyleLib;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using BeeBaby.Util;
using BeeBaby.Progress;
using BeeBaby.Controllers;

namespace BeeBaby.VisualElements
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
			var actionProgress = new ActionProgress(() => {
				var viewController = Windows.GetTopViewController(Window) as MomentDetailViewController;
				if (viewController != null)
				{
					if (viewController.IsEditFlow())
					{
						viewController.PerformSegue("eventSegue", this);
					}
					else
					{
						viewController.GoBackToEvents();
					}
				}
			}, false);
			actionProgress.Execute();
		}

		/// <summary>
		/// Builds the photo.
		/// </summary>
		/// <returns>The photo.</returns>
		protected override UIView BuildPhoto()
		{
			var x = (InitialFrame.Width / 2f) - (BadgeEventSize / 2f);

			var view = new UIView(new RectangleF(x, 0f, BadgeEventSize, BadgeEventSize));
			view.SetStyleClass("badge-event");
			view.AddSubview(BuildViewTagEvent());

			return view;
		}

		/// <summary>
		/// Redraw this instance.
		/// </summary>
		/// <param name="updateFrame">If set to <c>true</c> update frame.</param>
		public override void Redraw(bool updateFrame = false)
		{
			base.Redraw(updateFrame);

			var label = new Label(new RectangleF(115f, 0f, 200f, 22f));
			label.Text = "SwapEvent".Translate();
			label.SetStyleClass("change-event-label");
			label.TextAlignment = UITextAlignment.Right;
			AddSubview("change-event", label);
		}

		/// <summary>
		/// Builds the view tag event.
		/// </summary>
		/// <returns>The view tag event.</returns>
		UIView BuildViewTagEvent()
		{
			var position = (BadgeEventSize - BadgeEventInnerSize) / 2;
			var frame = new RectangleF(position, position, BadgeEventInnerSize, BadgeEventInnerSize);

			var view = new UIView(frame);
			view.ContentMode = UIViewContentMode.Center;
			view.SetStyleClass(CurrentContext.Instance.SelectedEvent.TagName);

			return view;
		}

		/// <summary>
		/// Gets the padding.
		/// </summary>
		/// <value>The padding.</value>
		protected override float Padding {
			get {
				return base.Padding * Multiplier;
			}
		}

		/// <summary>
		/// Gets the size of the badge event.
		/// </summary>
		/// <value>The size of the badge event.</value>
		float BadgeEventSize {
			get {
				return MediaBase.BadgeEventSize * Multiplier;
			}
		}

		/// <summary>
		/// Gets the size of the badge event inner.
		/// </summary>
		/// <value>The size of the badge event inner.</value>
		float BadgeEventInnerSize {
			get {
				return MediaBase.BadgeEventInnerSize * Multiplier;
			}
		}

		/// <summary>
		/// Gets the multiplier.
		/// </summary>
		/// <value>The multiplier.</value>
		float Multiplier {
			get {
				if (UIScreen.MainScreen.Bounds.Height <= 480f)
				{
					return 0.85f;
				}
				return 1f;
			}
		}
	}
}