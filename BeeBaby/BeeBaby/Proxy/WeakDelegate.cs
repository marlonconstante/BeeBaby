using System;

namespace BeeBaby.Proxy
{
	public class WeakDelegate<TDelegate> where TDelegate : class
	{
		WeakReference m_weakDelegate;

		public WeakDelegate(TDelegate delegateInstance)
		{
			m_weakDelegate = new WeakReference(delegateInstance);
		}

		/// <summary>
		/// Gets the target.
		/// </summary>
		/// <value>The target.</value>
		public TDelegate Target {
			get {
				return m_weakDelegate.Target as TDelegate;
			}
		}
	}
}