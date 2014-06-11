using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public class ControlEvent : IDisposable
	{
		UIView m_control;
		EventHandler m_eventHandler;
		ControlEventType m_type;

		public ControlEvent(UIView control, EventHandler eventHandler, ControlEventType type)
		{
			m_control = control;
			m_eventHandler = eventHandler;
			m_type = type;
		}

		/// <summary>
		/// Enable this instance.
		/// </summary>
		public void Enable()
		{
			switch (m_type)
			{
				case ControlEventType.TouchUpInside:
					((UIControl)m_control).TouchUpInside += m_eventHandler;
					break;
				case ControlEventType.ValueChanged:
					((UIControl)m_control).ValueChanged += m_eventHandler;
					break;
				case ControlEventType.Scrolled:
					((UIScrollView)m_control).Scrolled += m_eventHandler;
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Disable this instance.
		/// </summary>
		public void Disable()
		{
			switch (m_type)
			{
				case ControlEventType.TouchUpInside:
					((UIControl)m_control).TouchUpInside -= m_eventHandler;
					break;
				case ControlEventType.ValueChanged:
					((UIControl)m_control).ValueChanged -= m_eventHandler;
					break;
				case ControlEventType.Scrolled:
					((UIScrollView)m_control).Scrolled -= m_eventHandler;
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="BeeBaby.ControlEvent"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="BeeBaby.ControlEvent"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="BeeBaby.ControlEvent"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="BeeBaby.ControlEvent"/> so the garbage
		/// collector can reclaim the memory that the <see cref="BeeBaby.ControlEvent"/> was occupying.</remarks>
		public void Dispose()
		{
			Discard.ReleaseFields(this);
		}
	}
}