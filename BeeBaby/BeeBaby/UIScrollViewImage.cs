using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace BeeBaby
{
	public class UIScrollViewImage : UIScrollView
	{
		const float s_defaultZoom = 1f;
		const float s_minZoom = 0.1f;
		const float s_maxZoom = 3f;
		float m_sizeToFitZoom = 1f;
		UIImageView m_imageView;
		UITapGestureRecognizer m_singleTap;
		UITapGestureRecognizer m_doubleTap;

		public UIScrollViewImage()
		{
			AutoresizingMask = UIViewAutoresizing.All;

			m_imageView = new UIImageView();
			m_imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			AddSubview(m_imageView);

			// Setup zoom
			MaximumZoomScale = s_maxZoom;
			MinimumZoomScale = s_minZoom;
			ShowsVerticalScrollIndicator = false;
			ShowsHorizontalScrollIndicator = false;
			BouncesZoom = true;

			ViewForZoomingInScrollView += (UIScrollView sv) => {
				return m_imageView;
			};

			// Setup gestures
			m_singleTap = new UITapGestureRecognizer(() => {
				if (OnSingleTap != null)
				{
					OnSingleTap();
				}
			});
			m_singleTap.NumberOfTapsRequired = 1;
			AddGestureRecognizer(m_singleTap);

			m_doubleTap = new UITapGestureRecognizer(() => {
				if (ZoomScale >= s_defaultZoom)
				{
					SetZoomScale(m_sizeToFitZoom, true);
				}
				else
				{
					// Zoom to user specified point instead of center
					var point = m_doubleTap.LocationInView(m_doubleTap.View);
					var zoomRect = GetZoomRect(s_defaultZoom, point);
					ZoomToRect(zoomRect, true);
				}
			});
			m_doubleTap.NumberOfTapsRequired = 2;
			AddGestureRecognizer(m_doubleTap);

			// To use single tap and double tap gesture recognizers together. See for reference:
			// http://stackoverflow.com/questions/8876202/uitapgesturerecognizer-single-tap-and-double-tap
			m_singleTap.RequireGestureRecognizerToFail(m_doubleTap);
		}

		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="image">Image.</param>
		public void SetImage(UIImage image)
		{
			ZoomScale = s_defaultZoom;
			m_imageView.Image = image;
			m_imageView.Frame = new RectangleF(new PointF(), image.Size);
			ContentSize = image.Size;

			float widthScale;
			float heightScale;
			if (OrientationNotification.IsLandscape())
			{
				widthScale = Frame.Height / image.Size.Width;
				heightScale = Frame.Width / image.Size.Height;
			}
			else
			{
				widthScale = Frame.Width / image.Size.Width;
				heightScale = Frame.Height / image.Size.Height;
			}

			MinimumZoomScale = Math.Min(widthScale, heightScale);
			m_sizeToFitZoom = MinimumZoomScale;
			ZoomScale = MinimumZoomScale;

			m_imageView.Frame = CenterScrollViewContents();
		}

		/// <Docs>Lays out subviews.</Docs>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			m_imageView.Frame = CenterScrollViewContents();
		}

		/// <summary>
		/// Gets or sets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public override RectangleF Frame {
			get {
				return base.Frame;
			}
			set {
				base.Frame = value;

				if (m_imageView != null)
				{
					m_imageView.Frame = value;
				}
			}
		}

		/// <summary>
		/// Centers the scroll view contents.
		/// </summary>
		/// <returns>The scroll view contents.</returns>
		public RectangleF CenterScrollViewContents()
		{
			var boundsSize = Bounds.Size;
			var contentsFrame = m_imageView.Frame;

			if (contentsFrame.Width < boundsSize.Width)
			{
				contentsFrame.X = (boundsSize.Width - contentsFrame.Width) / 2f;
			}
			else
			{
				contentsFrame.X = 0f;
			}

			if (contentsFrame.Height < boundsSize.Height)
			{
				contentsFrame.Y = (boundsSize.Height - contentsFrame.Height) / 2f;
			}
			else
			{
				contentsFrame.Y = 0f;
			}

			return contentsFrame;
		}

		/// <summary>
		/// Gets the zoom rect.
		/// Reference:
		/// http://stackoverflow.com/a/11003277/548395
		/// </summary>
		/// <returns>The zoom rect.</returns>
		/// <param name="scale">Scale.</param>
		/// <param name="center">Center.</param>
		RectangleF GetZoomRect(float scale, PointF center)
		{
			var size = new SizeF(m_imageView.Frame.Size.Height / scale, m_imageView.Frame.Size.Width / scale);

			var imageCenter = ConvertPointToView(center, m_imageView);
			var location = new PointF(imageCenter.X - (size.Width / 2f),
				               imageCenter.Y - (size.Height / 2f));

			return new RectangleF(location, size);
		}

		/// <summary>
		/// Occurs when on single tap.
		/// </summary>
		public event Action OnSingleTap;
	}
}