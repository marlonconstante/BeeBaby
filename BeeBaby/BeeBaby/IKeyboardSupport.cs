using System;

namespace BeeBaby
{
	public interface IKeyboardSupport
	{
		/// <summary>
		/// Gets or sets a value indicating whether this instance is keyboard animation.
		/// </summary>
		/// <value><c>true</c> if this instance is keyboard animation; otherwise, <c>false</c>.</value>
		bool IsKeyboardAnimation {
			get;
			set;
		}
	}
}