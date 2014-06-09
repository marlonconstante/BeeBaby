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
				if (value is IDisposable)
				{
					var disposable = (IDisposable) value;
					disposable.Dispose();
				}
			}
		}

		/// <summary>
		/// Gets the subviews.
		/// </summary>
		/// <returns>The subviews.</returns>
		/// <param name="view">View.</param>
		static List<UIView> GetSubviews(UIView view)
		{
			List<UIView> subviews = new List<UIView>();
			foreach (var subview in view.Subviews)
			{
				subviews.AddRange(GetSubviews(subview));
				subviews.Add(subview);
			}
			return subviews;
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
		/// Releases the subviews.
		/// </summary>
		/// <param name="view">View.</param>
		public static void ReleaseSubviews(UIView view)
		{
			var subviews = GetSubviews(view);
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
			var method = type.GetMethod("ReleaseDesignerOutlets", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(instance, null);
			}
		}

		/// <summary>
		/// Releases the fields.
		/// </summary>
		/// <param name="instance">Instance.</param>
		public static void ReleaseFields(Object instance)
		{
			var type = instance.GetType();
			foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
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