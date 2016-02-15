using System;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace BeeBaby.Activity
{
	public class DocumentInteractionControllerDelegate : UIDocumentInteractionControllerDelegate
	{
		private UIViewController _viewController;
		private UIActivity _uiActivity;

		public DocumentInteractionControllerDelegate(UIActivity uiActivity)
		{
			_uiActivity = uiActivity;
			_viewController = _uiActivity.ViewController;
		}

		/// <summary>
		/// Views the controller for preview.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
		{
			return _viewController;
		}

		/// <summary>
		/// Views for preview.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public override UIView ViewForPreview(UIDocumentInteractionController controller)
		{
			return _viewController.View;
		}

		/// <summary>
		/// Rectangles for preview.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public override CGRect RectangleForPreview(UIDocumentInteractionController controller)
		{
			return _viewController.View.Frame;
		}
	}
}