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
			InitialTransform = Transform;
		}

		/// <summary>
		/// Rotate this instance.
		/// </summary>
		void Rotate()
		{
			Transform = InitialTransform;

			UIView.Animate(1d, 0d, UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveLinear, () => {
				Transform = CGAffineTransform.MakeRotation((float) -Math.PI);
			}, () => {
				if (IsSynchronizing)
				{
					Rotate();
				} else {
					UIView.Animate(0.3d, () => {
						Alpha = 0f;
					});
				}
			});
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update()
		{
			Alpha = IsSynchronizing ? 1f : 0f;
		}

		/// <summary>
		/// Starteds the synchronizing.
		/// </summary>
		public void StartedSynchronizing()
		{
			IsSynchronizing = true;

			BeginInvokeOnMainThread(() => {
				UIView.Animate(0.3d, () => {
					Alpha = 1f;
				}, () => {
					Rotate();
				});
			});
		}

		/// <summary>
		/// Endeds the synchronizing.
		/// </summary>
		public void EndedSynchronizing()
		{
			IsSynchronizing = false;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is synchronizing.
		/// </summary>
		/// <value><c>true</c> if this instance is synchronizing; otherwise, <c>false</c>.</value>
		bool IsSynchronizing {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the initial transform.
		/// </summary>
		/// <value>The initial transform.</value>
		CGAffineTransform InitialTransform {
			get;
			set;
		}
	}
}