using NUnit.Framework;
using System;
using TestSharp;
using System.IO;
using Infrastructure.App.Resources;
using HelperSharp;
using System.Linq;
using Domain.Media;
using Infrastructure.Framework.Resources;

namespace Infrastructure.App.UnitTests.Resources
{
	[TestFixture()]
	public class FileSystemResourceProviderTest
	{
		#region Fields
		private string m_imageFileName;
		private string m_rootDirectory;
		#endregion

		#region Initialize
		[TestFixtureSetUp]
		public void InitializeTest()
		{
			var projectFolder = VSProjectHelper.GetProjectFolderPath("Infrastructure.App.UnitTests");
			m_imageFileName = Path.Combine(projectFolder, @"Resources/Stubs/image.png");
			m_rootDirectory = Path.Combine(projectFolder, @"Resources/FileSystemEntityResourceProviderTestTempFolder");            
		}

		[TestFixtureTearDown]
		public void ClearTest()
		{
			try
			{
				DirectoryHelper.DeleteAllFiles(m_rootDirectory, "*.*", true);
				DirectoryHelper.DeleteIfNotExists(m_rootDirectory);
			}
			catch { }
		}
		#endregion

		#region Tests
		[Test()]
		public void Constructor_NullOrEmptyRootDirectory_Exception()
		{
			ExceptionAssert.IsThrowing(typeof(ArgumentException), () =>
			{
				new FileSystemResourceProvider("");
			});

			ExceptionAssert.IsThrowing(typeof(ArgumentNullException), () =>
			{
				new FileSystemResourceProvider(null);
			});
		}

		[Test()]
		public void SavePermanentImages_NullEntityOrStream_Exception()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);

			ExceptionAssert.IsThrowing(new ArgumentNullException("resourceEntity"), () =>
			{
				target.SavePermanentImages((MediaBase) null);
			});
		}

		[Test()]
		public void SavePermanentImages_EntityWithoutId_Exception()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);

			ExceptionAssert.IsThrowing(new ArgumentException("Uma entidade precisar ter um id antes de ter suas imagens permanentemente salvas.", "resourceEntity"), () =>
			{
				target.SavePermanentImages(new MediaBase());
			});
		}

		[Test()]
		public void SavePermanentImages_EntityWithoutTemporaryKey_DoNothing()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			target.SavePermanentImages(new MediaBase("1"));
			TestSharp.FileAssert.NonExists(m_rootDirectory + @"/MediaBase/1/1.png");            
		}

		[Test()]
		public void SavePermanentImages_EntityAndNotExistingKey_Exception()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			var resourceEntity = (new MediaBase("2")) as IResourceEntity;
			resourceEntity.TemporaryKey = Guid.NewGuid().ToString();

			ExceptionAssert.IsThrowing(new ArgumentException("A chave '{0}' não é uma chave temporária válida. Antes de chamar o método SavePermanentImages é necessário chamar SaveTemporaryImages.".With(resourceEntity.TemporaryKey), "resourceEntity"), () =>
			{
				target.SavePermanentImages(resourceEntity);
			});

		}

		[Test()]
		public void SavePermanentImages_EntityAndExistingKey_Saved()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			var resourceEntity1 = (new MediaBase("1")) as IResourceEntity;
			var resourceEntity2 = (new MediaBase("2")) as IResourceEntity;
			target.SaveTemporaryImages(resourceEntity1, File.OpenRead(m_imageFileName));
			target.SaveTemporaryImages(resourceEntity2, File.OpenRead(m_imageFileName));

			target.SavePermanentImages(resourceEntity2);
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/MediaBase/2/1.png");
			Assert.IsFalse(Directory.Exists(m_rootDirectory + @"/Temp/" + resourceEntity2.TemporaryKey));

			target.SavePermanentImages(resourceEntity1);
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/MediaBase/1/1.png");
			Assert.IsFalse(Directory.Exists(m_rootDirectory + @"/Temp/" + resourceEntity1.TemporaryKey));

			TestSharp.FileAssert.Exists(m_rootDirectory + @"/MediaBase/2/1.png");
			Assert.IsFalse(Directory.Exists(m_rootDirectory + @"/Temp/" + resourceEntity2.TemporaryKey));
		}

		[Test()]
		public void CopyImages_WithoutTemporaryKeyAndWithoutKey_False()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			var resourceEntity1 = (new MediaBase()) as IResourceEntity;
			var resourceEntity2 = (new MediaBase()) as IResourceEntity;

			Assert.IsFalse(target.CopyImages(resourceEntity1, resourceEntity2));
		}

		[Test()]
		public void CopyImages_WithTemporaryKeyAndWithoutKey_CopyOnlyTemporaryImages()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			var fromEntity = (new MediaBase("1")) as IResourceEntity;
			var toEntity = (new MediaBase("2")) as IResourceEntity;

			target.SaveTemporaryImages(fromEntity, File.OpenRead(m_imageFileName));
			target.CopyImages(fromEntity, toEntity);

			Assert.IsNotNull(toEntity.TemporaryKey);
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/Temp/{0}/1.png".With(fromEntity.TemporaryKey));
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/Temp/{0}/1.png".With(toEntity.TemporaryKey));
		}

		[Test()]
		public void CopyImages_WithoutTemporaryKeyAndWithtKey_CopyOnlyPermanentImages()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			var fromEntity = (new MediaBase("1")) as IResourceEntity;
			var toEntity = (new MediaBase("2")) as IResourceEntity;
			fromEntity.Key = "1";
			toEntity.Key = "2";
			target.SaveTemporaryImages(fromEntity, File.OpenRead(m_imageFileName));
			target.SavePermanentImages(fromEntity);
			fromEntity.TemporaryKey = null;
			target.CopyImages(fromEntity, toEntity);

			TestSharp.FileAssert.Exists(m_rootDirectory + @"/MediaBase/1/1.png");
		}

		[Test()]
		public void CopyImages_WithTemporaryKeyAndWithtKey_CopyAllImages()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);
			var fromEntity = (new MediaBase("1")) as IResourceEntity;
			var toEntity = (new MediaBase("2")) as IResourceEntity;
			fromEntity.Key = "1";
			toEntity.Key = "2";
			target.SaveTemporaryImages(fromEntity, File.OpenRead(m_imageFileName));
			target.SavePermanentImages(fromEntity);
			target.SaveTemporaryImages(fromEntity, File.OpenRead(m_imageFileName));
			target.CopyImages(fromEntity, toEntity);

			Assert.IsNotNull(toEntity.TemporaryKey);
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/Temp/{0}/1.png".With(fromEntity.TemporaryKey));
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/Temp/{0}/1.png".With(toEntity.TemporaryKey));
			TestSharp.FileAssert.Exists(m_rootDirectory + @"/MediaBase/1/1.png");
		}

		[Test()]
		public void GetImagesUrls_NullEntity_Exception()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory);

			ExceptionAssert.IsThrowing(new ArgumentNullException("resourceEntity"), () =>
			{
				target.GetImagesUrls(null);
			});
		}

		[Test()]
		public void GetImagesUrls_Entity_Url()
		{
			var target = new FileSystemResourceProvider(m_rootDirectory, "/Resources/Images");

			SavePermanentImages_EntityAndExistingKey_Saved();
			var urls = target.GetImagesUrls(new MediaBase("2"));
			Assert.AreEqual(1, urls.Count());
			Assert.AreEqual("/Resources/Images/MediaBase/2/1.png", urls.FirstOrDefault());
		}
		#endregion
	}
}

