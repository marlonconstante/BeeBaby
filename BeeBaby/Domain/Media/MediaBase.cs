using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Media
{
	/// <summary>
	/// Class that represents the kind of moment.
	/// </summary>
	public class MediaBase : EntityWithIdBase<string>, IAggregateRoot
	{
		#region Constants
		public const int ImageThumbnailSize = 150;
		public const int ImageShareSize = 320;
		public const float ImageCompressionQuality = 0.7f;
		public const float PhotoProfileSize = 98f;
		public const float PhotoProfileInnerSize = 90f;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Media.MediaBase"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public MediaBase(string id) : base(id)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Media.MediaBase"/> class.
		/// </summary>
		public MediaBase() : this(null)
		{
		}

		/// <summary>
		/// Gets or sets the temporary key.
		/// </summary>
		/// <value>The temporary key.</value>
		public string TemporaryKey { get; set; }
	}
}