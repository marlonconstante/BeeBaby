using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using BeeBaby.ResourcesProviders;
using Application;
using System.Collections.Generic;
using System.Threading;
using BigTed;

namespace BeeBaby
{
	public class ImagePickerDelegate : UIImagePickerControllerDelegate
	{
		ImageProvider m_imageProvider;
		IList<Action> m_tasks;
		bool m_pendingTasks;
		Thread m_performTasks;

		public ImagePickerDelegate()
		{
			m_imageProvider = new ImageProvider(CurrentContext.Instance.Moment);
			m_tasks = new List<Action>();
		}

		/// <summary>
		/// Finisheds the picking media.
		/// </summary>
		/// <param name="picker">Picker.</param>
		/// <param name="info">Info.</param>
		public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
		{
			if (picker.SourceType == UIImagePickerControllerSourceType.Camera)
			{
				lock (m_imageProvider)
				{
					m_tasks.Add(() => {
						SaveTemporaryImageOnApp(info, false);
					});
				}
				if (m_performTasks == null)
				{
					PerformTasks();
				}
			}
			else
			{
				ActionProgress actionProgress = new ActionProgress(() => {
					SaveTemporaryImageOnApp(info, true);
					picker.DismissViewController(true, null);
				}, false);
				actionProgress.Execute();
			}
		}

		/// <summary>
		/// Finisheds the picking image.
		/// </summary>
		/// <param name="picker">Picker.</param>
		/// <param name="image">Image.</param>
		public override void FinishedPickingImage(UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
		{
			m_imageProvider.SaveTemporaryImageOnApp(image);
		}

		/// <summary>
		/// Wills the show view controller.
		/// </summary>
		/// <param name="navigationController">Navigation controller.</param>
		/// <param name="viewController">View controller.</param>
		public override void WillShowViewController(UINavigationController navigationController, UIViewController viewController, bool animated)
		{
			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

			// Dismiss the spinner
			BTProgressHUD.Dismiss();
		}

		/// <summary>
		/// Saves the temporary image on app.
		/// </summary>
		/// <param name="info">Info.</param>
		void SaveTemporaryImageOnApp(NSDictionary info, bool selected)
		{
			UIImage photo = (UIImage) info.ObjectForKey(UIImagePickerController.OriginalImage);
			var fileName = m_imageProvider.SaveTemporaryImageOnApp(photo);
			if (selected)
			{
				var thumbnailImageName = m_imageProvider.GetThumbnailImageName(fileName);
				CurrentContext.Instance.Moment.SelectedMediaNames.Add(thumbnailImageName);
			}
		}

		/// <summary>
		/// Performs the tasks.
		/// </summary>
		void PerformTasks()
		{
			m_pendingTasks = true;
			m_performTasks = new Thread(() => {
				while (true)
				{
					Action action = null;
					lock (m_imageProvider)
					{
						if (m_tasks.Count > 0)
						{
							action = m_tasks[0];
							m_tasks.RemoveAt(0);
						}
					}
					if (action != null)
					{
						action.Invoke();
					}
					else if (!m_pendingTasks)
					{
						break;
					}
					else
					{
						Thread.Sleep(100);
					}
				}
			});
			m_performTasks.IsBackground = true;
			m_performTasks.Start();
		}

		/// <summary>
		/// Waits for pending tasks.
		/// </summary>
		public void WaitForPendingTasks()
		{
			m_pendingTasks = false;
			while (m_performTasks != null && m_performTasks.IsAlive)
			{
				Thread.Sleep(100);
			}
		}
	}
}