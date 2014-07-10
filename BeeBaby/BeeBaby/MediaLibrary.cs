using System;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using MonoTouch.ImageIO;

namespace BeeBaby
{
	public sealed class MediaLibrary : ALAssetsLibrary
	{
		static MediaLibrary s_instance;
		const string s_albumName = "BeeBaby";
		ALAssetsGroup m_album;

		private MediaLibrary()
		{
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize()
		{
			AddAssetsGroupAlbum(s_albumName, (group) => {
				if (group == null)
				{
					FindAlbum();
				}
				else
				{
					m_album = group;
				}
				Console.WriteLine("Álbum \"" + s_albumName + "\" criado com sucesso.");
			}, (error) => {
				Console.WriteLine("Ocorreu um erro na criação do álbum \"" + s_albumName + "\":\n" + error.LocalizedDescription);
			});
		}

		/// <summary>
		/// Save the specified image.
		/// </summary>
		/// <param name="image">Image.</param>
		public void Save(UIImage image)
		{
			WriteImageToSavedPhotosAlbum(image, (ALAssetOrientation) image.Orientation, (url, error) => {
				if (error == null)
				{
					CopyToAlbum(url);
				}
				else
				{
					Console.WriteLine("Ocorreu um erro ao salvar a foto no \"Rolo da Câmera\":\n" + error.LocalizedDescription);
				}
			});
		}

		/// <summary>
		/// Copies to album.
		/// </summary>
		/// <param name="url">URL.</param>
		void CopyToAlbum(NSUrl url)
		{
			AssetForUrl(url, (asset) => {
				m_album.AddAsset(asset);
			}, (error) => {
				Console.WriteLine("Ocorreu um erro ao copiar a foto para o álbum \"" + s_albumName + "\":\n" + error.LocalizedDescription);
			});
		}

		/// <summary>
		/// Finds the album.
		/// </summary>
		void FindAlbum()
		{
			Enumerate(ALAssetsGroupType.Album, EnumerateGroups, (error) => {
				Console.WriteLine("Ocorreu um erro ao procurar o álbum \"" + s_albumName + "\":\n" + error.LocalizedDescription);
			});
		}

		/// <summary>
		/// Enumerates the groups.
		/// </summary>
		/// <param name="group">Group.</param>
		/// <param name="stop">Stop.</param>
		void EnumerateGroups(ALAssetsGroup group, ref bool stop)
		{
			if (group != null && s_albumName.Equals(group.Name))
			{
				m_album = group;
				stop = true;
			}
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static MediaLibrary Instance {
			get {
				if (s_instance == null)
				{
					s_instance = new MediaLibrary();
				}
				return s_instance; 
			}
		}
	}
}