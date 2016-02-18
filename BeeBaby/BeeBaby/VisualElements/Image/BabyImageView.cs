using System;
using CoreGraphics;
using BeeBaby.ViewModels;
using System.Drawing;

namespace BeeBaby.VisualElements
{
	public class BabyImageView : UIImageViewClickable
	{
		public BabyImageView(CGRect frame) : base(frame)
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