using System;
using CoreGraphics;
using PixateFreestyleLib;
using UIKit;
using BeeBaby.Synchronization;
using BeeBaby.Animation;
using System.Drawing;

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
		public SyncButton(CGRect frame) : base(frame)
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
					this.Rotate(1d, () => {
						return IsSynchronizing;
					}, () => {
						UIView.Animate(0.3d, () => {
							Alpha = 0f;
						});
					});
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
	}
}