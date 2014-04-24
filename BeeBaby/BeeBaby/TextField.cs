using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public partial class TextField : UITextField
	{
		public TextField(IntPtr handle) : base(handle)
		{
			ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
		}
	}
}