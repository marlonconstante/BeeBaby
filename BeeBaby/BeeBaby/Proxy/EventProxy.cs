using System;

namespace BeeBaby.Proxy
{
	public class EventProxy<TDelegate, TEventArgs> : IDisposable
		where TDelegate : class where TEventArgs : EventArgs
	{
		WeakDelegate<TDelegate> m_weakDelegate;

		public EventProxy(TDelegate delegateInstance)
		{
			m_weakDelegate = new WeakDelegate<TDelegate>(delegateInstance);
		}

		/// <summary>
		/// Handles the event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void HandleEvent(object sender, TEventArgs args)
		{
			var target = m_weakDelegate.Target;

			if (Action != null && target != null)
			{
				Action(target, sender, args);
			}
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		public Action<TDelegate, object, TEventArgs> Action {
			get;
			set; 
		}

		/// <summary>
		/// Releases all resource used by the <see cref="BeeBaby.EventProxy`2"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="BeeBaby.EventProxy`2"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="BeeBaby.EventProxy`2"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="BeeBaby.EventProxy`2"/> so the garbage
		/// collector can reclaim the memory that the <see cref="BeeBaby.EventProxy`2"/> was occupying.</remarks>
		public void Dispose()
		{
			Action = null;
		}
	}
}