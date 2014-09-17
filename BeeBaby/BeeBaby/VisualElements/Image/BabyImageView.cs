using System;
using System.Drawing;
using BeeBaby.ViewModels;

namespace BeeBaby.VisualElements
{
	public class BabyImageView : UIImageViewClickable
	{
		public BabyImageView(RectangleF frame) : base(frame)
		{
		}

		/// <summary>
		/// Gets or sets the baby profile.
		/// </summary>
		/// <value>The baby profile.</value>
		public BabyProfile BabyProfile {
			get;
			set;
		}
	}
}