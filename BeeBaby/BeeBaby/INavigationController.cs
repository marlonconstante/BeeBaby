﻿using System;
using MonoTouch.UIKit;

namespace BeeBaby
{
	public interface INavigationController
	{
		/// <summary>
		/// Gets the current view controller.
		/// </summary>
		/// <returns>The current view controller.</returns>
		UIViewController GetCurrentViewController();
	}
}