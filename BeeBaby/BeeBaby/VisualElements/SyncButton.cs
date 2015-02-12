using System;
using System.Drawing;
using PixateFreestyleLib;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using BeeBaby.Synchronization;

namespace BeeBaby.VisualElements
{
	public class SyncButton : Button, ISyncEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.VisualElements.SyncButton"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public SyncButton(IntPtr handle) : base(handle)
		{
			SetUp();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.VisualElements.SyncButton"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public SyncButton(RectangleF frame) : base(frame)
		{
			SetUp();
		}

		/// <summary>
		/// Sets up.
		/// </summary>
		void SetUp()
		{
			this.SetStyleClass("button-sync");
		}

		/// <summary>
		/// Starteds the synchronizing.
		/// </summary>
		public void StartedSynchronizing()
		{
			BeginInvokeOnMainThread(() => {
				UIView.Animate(0.3d, () => {
					Alpha = 1f;
				}, () => {
					UIView.Animate(1d, 0d, UIViewAnimationOptions.Repeat | UIViewAnimationOptions.CurveLinear, () => {
						Transform = CGAffineTransform.MakeRotation((float) -Math.PI);
					}, null);
				});
			});
		}

		/// <summary>
		/// Endeds the synchronizing.
		/// </summary>
		public void EndedSynchronizing()
		{
			BeginInvokeOnMainThread(() => {
				UIView.Animate(0.3d, () => {
					Alpha = 0f;
				});
			});
		}
	}
}