using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Domain.Media
{
	/// <summary>
	/// Class that represents the kind of moment.
	/// </summary>
	public class MediaBase: EntityWithIdBase<string>, IAggregateRoot
	{
		#region Constants
		public const int ImageThumbnailSize = 150;
		public const float ImageCompressionQuality = 0.7f;
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