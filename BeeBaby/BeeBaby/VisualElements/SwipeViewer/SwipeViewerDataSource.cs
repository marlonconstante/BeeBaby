using System;
using System.Collections.Generic;
using BeeBaby.ViewModels;
using UIKit;
using SwipeViewer;

namespace BeeBaby.VisualElements
{
	public class SwipeViewerDataSource : SwipeViewDataSource
	{
		public SwipeViewerDataSource()
		{
			Images = new List<ImageModel>();
		}

		/// <summary>
		/// Numbers the of items in swipe view.
		/// </summary>
		/// <returns>The of items in swipe view.</returns>
		/// <param name="swipeView">Swipe view.</param>
		public override int NumberOfItemsInSwipeView(SwipeView swipeView)
		{
			return Images.Count;
		}

		public override UIView ViewForItemAtIndex (SwipeView swipeView, int index, UIView view)
		{
			if (view == null)
			{
				view = new UIImageView() {
					ContentMode = UIViewContentMode.ScaleAspectFit,
					AutoresizingMask = UIViewAutoresizing.All,
					Frame = swipeView.Bounds
				};
			}

			var imageView = view as UIImageView;
			imageView.Image = Images[index].Image;

			return view;
		} 

		/// <summary>
		/// Gets or sets the images.
		/// </summary>
		/// <value>The images.</value>
		public List<ImageModel> Images {
			get;
			protected set;
		}
	}
}