using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using PixateFreestyleLib;

namespace BeeBaby.VisualElements
{
	public partial class TabBar : UITabBar
	{
		public TabBar(IntPtr handle) : base(handle)
		{
			TimelineItem.SetStyleClass("tree");
			CameraItem.SetStyleClass("camera");
			DealsItem.SetStyleClass("market");

			Items = new UITabBarItem[] { TimelineItem, CameraItem, DealsItem };

			SelectedItem = TimelineItem;
			ItemSelected += SelectItem;
		}

		/// <summary>
		/// Selects the item.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void SelectItem(object sender, UITabBarItemEventArgs args)
		{
			
		}

		/// <summary>
		/// Gets the timeline item.
		/// </summary>
		/// <value>The timeline item.</value>
		UITabBarItem TimelineItem {
			get;
		} = new UITabBarItem();

		/// <summary>
		/// Gets the camera item.
		/// </summary>
		/// <value>The camera item.</value>
		UITabBarItem CameraItem {
			get;
		} = new UITabBarItem();

		/// <summary>
		/// Gets the deals item.
		/// </summary>
		/// <value>The deals item.</value>
		UITabBarItem DealsItem {
			get;
		} = new UITabBarItem();

		/// <summary>
		/// Dispose the specified disposing.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ItemSelected -= SelectItem;
			}
			base.Dispose(disposing);
		}
	}
}