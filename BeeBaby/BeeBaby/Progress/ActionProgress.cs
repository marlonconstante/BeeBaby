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
		/// Execute this instance.
		/// </summary>
		public void Execute()
		{
			// Shows the spinner
			BTProgressHUD.Show();

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