using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using BeeBaby.ResourcesProviders;
using Application;
using System.Collections.Generic;
using System.Threading;
using Domain.Moment;

namespace BeeBaby
{
	public class MomentImagePickerDelegate : ImagePickerDelegate
	{
		IList<Action> m_tasks;
		bool m_pendingTasks;
		Thread m_performTasks;

		public MomentImagePickerDelegate(Moment moment)
		{
			ImageProvider = new ImageProvider(moment.Id);
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
				lock (ImageProvider)
				{
					m_tasks.Add(() => SaveTemporaryImage(info, true));
				}
				if (m_performTasks == null)
				{
					PerformTasks();
				}
			}
			else
			{
				ActionProgress actionProgress = new ActionProgress(() =>
				{
					SaveTemporaryImage(info);
					picker.DismissViewController(true, null);
				}, false);
				actionProgress.Execute();
			}
		}

		/// <summary>
		/// Saves the temporary image.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="saveToAlbum">If set to <c>true</c> save to album.</param>
		/// <param name="selected">If set to <c>true</c> selected.</param>
		void SaveTemporaryImage(NSDictionary info, bool saveToAlbum = false, bool selected = true)
		{
			using (var photo = (UIImage)info.ObjectForKey(UIImagePickerController.OriginalImage))
			{
				var fileName = ImageProvider.SaveTemporaryImageOnApp(photo);

				if (saveToAlbum)
				{
					InvokeOnMainThread(() => photo.SaveToPhotosAlbum(null));
				}

				if (selected)
				{
					var thumbnailImageName = ImageProvider.GetThumbnailImageName(fileName);
					CurrentContext.Instance.Moment.SelectedMediaNames.Add(thumbnailImageName);
				}
			}
		}

		/// <summary>
		/// Performs the tasks.
		/// </summary>
		void PerformTasks()
		{
			m_pendingTasks = true;
			m_performTasks = new Thread(() =>
			{
				while (true)
				{
					Action action = null;
					lock (ImageProvider)
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
						Thread.Sleep(300);
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
				Thread.Sleep(300);
			}
		}
	}
}