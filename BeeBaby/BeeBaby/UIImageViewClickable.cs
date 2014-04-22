using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public class UIImageViewClickable : UIImageView
	{
		UITapGestureRecognizer grTap;

		event Action onCl;

		public UIImageViewClickable(RectangleF frame) : base(frame)
		{
		}

		/// <summary>
		/// Occurs when on click.
		/// </summary>
		public event Action OnClick {
			add {
				onCl += value;
				UpdateUserInteractionFlag();
			}
			remove {
				onCl -= value;
				UpdateUserInteractionFlag();
			}
		}

		/// <summary>
		/// Updates the user interaction flag.
		/// </summary>
		void UpdateUserInteractionFlag()
		{
			UserInteractionEnabled = ((onCl != null) && (onCl.GetInvocationList().Length > 0));
			if (UserInteractionEnabled)
			{
				if (grTap == null)
				{
					grTap = new UITapGestureRecognizer(() => {
						if (onCl != null)
						{
							onCl();
						}
					});
					grTap.CancelsTouchesInView = true;
					AddGestureRecognizer(grTap);
				}
			}
			else
			{
				if (grTap != null)
				{
					RemoveGestureRecognizer(grTap);
					grTap = null;
				}
			}
		}
	}
}