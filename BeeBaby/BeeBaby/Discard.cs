using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Reflection;
using System.Collections;
using MonoTouch.Foundation;

namespace BeeBaby
{
	public abstract class Discard
	{
		private Discard()
		{
		}

		/// <summary>
		/// Dispose the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		static void Dispose(Object value)
		{
			if (value != null)
			{
				if (value is IEnumerable)
				{
					ReleaseValues((IEnumerable) value);
				}
				if (value is UIImageView)
				{
					ReleaseImage((UIImageView) value);
				}
				if (value is IDisposable && !"BeeBaby".Equals(value.GetType().Namespace))
				{
					var disposable = (IDisposable) value;
					disposable.Dispose();
				}
			}
		}

		/// <summary>
		/// Releases the values.
		/// </summary>
		/// <param name="values">Values.</param>
		static void ReleaseValues(IEnumerable values)
		{
			foreach (var value in values)
			{
				Dispose(value);
			}
		}

		/// <summary>
		/// Releases the image.
		/// </summary>
		/// <param name="imageView">Image view.</param>
		static void ReleaseImage(UIImageView imageView)
		{
			Dispose(imageView.Image);
			imageView.Image = null;
		}

		/// <summary>
		/// Releases the navigation.
		/// </summary>
		/// <param name="navigationController">Navigation controller.</param>
		public static void ReleaseNavigation(UINavigationController navigationController)
		{
			navigationController.PopToRootViewController(false);
			navigationController.RemoveFromParentViewController();
			navigationController.View.RemoveFromSuperview();
			navigationController.TopViewController.Dispose();
			navigationController.Dispose();
		}

		/// <summary>
		/// Releases the subviews.
		/// </summary>
		/// <param name="view">View.</param>
		public static void ReleaseSubviews(UIView view)
		{
			var subviews = Views.GetSubviews(view);
			subviews.Add(view);
			foreach (var subview in subviews)
			{
				subview.RemoveFromSuperview();
				Dispose(subview);
			}
		}

		/// <summary>
		/// Releases the outlets.
		/// </summary>
		/// <param name="instance">Instance.</param>
		public static void ReleaseOutlets(Object instance)
		{
			var type = instance.GetType();
			var method = type.GetMethod("ReleaseDesignerOutlets", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(instance, null);
			}
		}

		/// <summary>
		/// Releases the properties.
		/// </summary>
		/// <param name="instance">Instance.</param>
		public static void ReleaseProperties(Object instance)
		{
			var type = instance.GetType();
			foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (property.CanRead && property.CanWrite)
				{
					var value = property.GetValue(instance);
					if (value != null)
					{
						Dispose(value);
						property.SetValue(instance, null);
					}
				}
			}
		}

		/// <summary>
		/// Releases the fields.
		/// </summary>
		/// <param name="instance">Instance.</param>
		public static void ReleaseFields(Object instance)
		{
			var type = instance.GetType();
			foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic))
			{
				var value = field.GetValue(instance);
				if (value != null)
				{
					Dispose(value);
					field.SetValue(instance, null);
				}
			}
		}
	}
}