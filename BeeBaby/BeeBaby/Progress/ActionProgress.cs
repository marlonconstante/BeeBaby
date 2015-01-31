using System;
using System.Threading;
using MonoTouch.Foundation;
using BigTed;

namespace BeeBaby.Progress
{
	public class ActionProgress : NSObject
	{
		NSAction m_action;
		bool m_closeProgressWhenFinished;

		public ActionProgress(NSAction action, bool closeProgressWhenFinished = true)
		{
			m_action = action;
			m_closeProgressWhenFinished = closeProgressWhenFinished;
		}

		/// <summary>
		/// Execute the specified status.
		/// </summary>
		/// <param name="status">Status.</param>
		public void Execute(string status = null)
		{
			// Shows the spinner
			BTProgressHUD.Show(status);

			InvokeInBackground(() => {
				InvokeOnMainThread(() => {
					m_action();

					if (m_closeProgressWhenFinished)
					{
						// Dismiss the spinner
						BTProgressHUD.Dismiss();
					}
				});
			});
		}
	}
}