using System;
using System.Drawing;
using MonoTouch.UIKit;
using PixateFreestyleLib;
using System.Collections.Generic;

namespace BeeBaby
{
	/// <summary>
	/// Avatar template.
	/// </summary>
	public enum AvatarTemplate
	{
		Photo = 0,
		PhotoAndDescription = 1
	}

	/// <summary>
	/// Avatar view.
	/// </summary>
	public abstract class AvatarView : View
	{
		protected const float Padding = 10f;

		Dictionary<string, UIView> m_viewMap = new Dictionary<string, UIView>();

		public AvatarView(IntPtr handle, AvatarTemplate template) : base(handle)
		{
			Init(template);
		}

		public AvatarView(RectangleF frame, AvatarTemplate template) : base(frame)
		{
			Init(template);
		}

		/// <summary>
		/// Init the specified template.
		/// </summary>
		/// <param name="template">Template.</param>
		void Init(AvatarTemplate template)
		{
			Template = template;
			InitialFrame = Frame;
			ClipsToBounds = true;
			AddBackgroundImage();
			Redraw();
		}

		/// <summary>
		/// Adds the background image.
		/// </summary>
		void AddBackgroundImage()
		{
			var backgroundImageName = GetBackgroundImageName();
			if (!string.IsNullOrEmpty(backgroundImageName))
			{
				var image = UIImage.FromFile(string.Format("{0}.png", backgroundImageName));
				var imageView = new UIImageViewClickable(new RectangleF(0f, 0f, InitialFrame.Width, InitialFrame.Height));

				UpdateImageView(imageView, image, Template == AvatarTemplate.PhotoAndDescription);

				AddSubview(imageView);
			}
		}

		/// <summary>
		/// Discards the view.
		/// </summary>
		/// <param name="key">Key.</param>
		void DiscardView(string key)
		{
			UIView view;
			if (m_viewMap.TryGetValue(key, out view))
			{
				view.RemoveFromSuperview();
				Discard.ReleaseSubviews(view);

				m_viewMap.Remove(key);
			}
		}

		/// <summary>
		/// Adds the subview.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="view">View.</param>
		public void AddSubview(string key, UIView view)
		{
			DiscardView(key);
			m_viewMap.Add(key, view);
			AddSubview(view);
		}

		/// <summary>
		/// Redraw this instance.
		/// </summary>
		public virtual void Redraw()
		{
			AddSubview("photo", BuildPhoto());
			if (Template == AvatarTemplate.PhotoAndDescription)
			{
				AddSubview("description", BuildDescription());
			}
		}

		/// <summary>
		/// Determines whether this instance is increase touch area.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public override bool IsIncreaseTouchArea()
		{
			return false;
		}

		/// <summary>
		/// Gets the name of the background image.
		/// </summary>
		/// <returns>The background image name.</returns>
		protected abstract string GetBackgroundImageName();

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <returns>The description.</returns>
		protected abstract string GetDescription();

		/// <summary>
		/// Action the specified sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		protected abstract void Action(UIView sender);

		/// <summary>
		/// Builds the photo.
		/// </summary>
		/// <returns>The photo.</returns>
		protected abstract UIView BuildPhoto();

		/// <summary>
		/// Builds the description.
		/// </summary>
		/// <returns>The description.</returns>
		protected virtual Label BuildDescription()
		{
			var width = InitialFrame.Width - (Padding * 2f);
			var height = 64f;
			var y = InitialFrame.Height - height;

			var label = new Label(new RectangleF(Padding, y, width, height));
			label.SetStyleClass("avatar-label");
			label.TextAlignment = UITextAlignment.Center;
			label.Lines = 2;
			label.Text = GetDescription();

			return label;
		}

		/// <summary>
		/// Updates the image view.
		/// </summary>
		/// <param name="imageView">Image view.</param>
		/// <param name="image">Image.</param>
		/// <param name="addClickedEvent">If set to <c>true</c> add clicked event.</param>
		protected void UpdateImageView(UIImageViewClickable imageView, UIImage image, bool addClickedEvent = true) {
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.ClipsToBounds = true;
			imageView.Image = image;
			if (addClickedEvent)
			{
				imageView.Clicked += ProxyAction.HandleEvent;
			}
		}

		/// <summary>
		/// Gets the proxy action.
		/// </summary>
		/// <value>The proxy action.</value>
		EventProxy<AvatarView, EventArgs> ProxyAction {
			get {
				var proxy = new EventProxy<AvatarView, EventArgs>(this);
				proxy.Action = (target, sender, args) => {
					target.Action((UIView) sender);
				};
				return proxy;
			}
		}

		/// <summary>
		/// Gets the initial frame.
		/// </summary>
		/// <value>The initial frame.</value>
		public RectangleF InitialFrame {
			get;
			private set;
		}

		/// <summary>
		/// Gets the template.
		/// </summary>
		/// <value>The template.</value>
		public AvatarTemplate Template {
			get;
			private set;
		}
	}
}