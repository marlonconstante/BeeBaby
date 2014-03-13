using System;
using Skahal.Infrastructure.Framework.Domain;
using Infrastructure.Framework.Resources;

namespace Domain.Media
{
	/// <summary>
	/// Class that represents the kind of moment.
	/// </summary>
	public class MediaBase: EntityWithIdBase<string>, IResourceEntity, IAggregateRoot
	{
		#region Constants
		/// <summary>
		/// A largura da imagem do anúncio.
		/// </summary>
		public const int ImageWidth = 120;

		/// <summary>
		/// A altura da imagem do anúncio.
		/// </summary>
		public const int ImageHeight = 72;
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

		/// <summary>
		/// Gets the width of the resource.
		/// </summary>
		/// <value>The width of the resource.</value>
		public int ResourceWidth
		{
			get { return ImageWidth; }
		}

		/// <summary>
		/// Gets the height of the resource.
		/// </summary>
		/// <value>The height of the resource.</value>
		public int ResourceHeight
		{
			get { return ImageHeight; }
		}

	}
}

