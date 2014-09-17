using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby.VisualElements
{
	public class UIImageViewClickable : UIImageView
	{
		UITapGestureRecognizer m_tap;

		EventHandler m_clicked;

		public UIImageViewClickable(RectangleF frame) : base(frame)
		{
		}

		/// <summary>
		/// Occurs when clicked.
		/// </summary>
		public event EventHandler Clicked {
			add {
				m_clicked += value;
				UpdateUserInteractionFlag();
			}
			remove {
				m_clicked -= value;
				UpdateUserInteractionFlag();
			}
		}

		/// <summary>
		/// Updates the user interaction flag.
		/// </summary>
		void UpdateUserInteractionFlag()
		{
			UserInteractionEnabled = ((m_clicked != null) && (m_clicked.GetInvocationList().Length > 0));
			if (UserInteractionEnabled)
			{
				if (m_tap == null)
				{
					m_tap = new UITapGestureRecognizer(() => {
						if (m_clicked != null)
						{
							m_clicked(this, EventArgs.Empty);
						}
					});
					m_tap.CancelsTouchesInView = true;
					AddGestureRecognizer(m_tap);
				}
			}
			else
			{
				if (m_tap != null)
				{
					RemoveGestureRecognizer(m_tap);
					m_tap = null;
				}
			}
		}
	}
}