using System;
using System.IO;
using Foundation;

namespace BeeBaby.Backup
{
	public abstract class FileHandle
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.FileHandle"/> class.
		/// </summary>
		protected FileHandle()
		{
		}

		/// <summary>
		/// Load the specified path.
		/// </summary>
		/// <param name="path">Path.</param>
		protected Stream Load(string path)
		{
			if (FileHandle.IsValid(path))
			{
				return new MemoryStream(NSData.FromFile(path).ToArray());
			}
			return Stream.Null;
		}

		/// <summary>
		/// Save the specified path and stream.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="stream">Stream.</param>
		protected void Save(string path, Stream stream)
		{
			using (var data = NSData.FromStream(stream))
			{
				NSError error;
				if (!data.Save(path, false, out error))
				{
					Console.WriteLine("Ocorreu um erro ao salvar o arquivo \"" + Path.GetFileName(path) + "\":\n" + error.LocalizedDescription);
				}
			}
		}

		/// <summary>
		/// Delete the specified path.
		/// </summary>
		/// <param name="path">Path.</param>
		protected void Delete(string path)
		{
			File.Delete(path);
		}

		/// <summary>
		/// Determines if is valid the specified path.
		/// </summary>
		/// <returns><c>true</c> if is valid the specified path; otherwise, <c>false</c>.</returns>
		/// <param name="path">Path.</param>
		public static bool IsValid(string path)
		{
			return File.Exists(path) && new FileInfo(path).Length > 0L;
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="directoryName">Directory name.</param>
		/// <param name="fileName">File name.</param>
		public static string GetPath(string directoryName, string fileName)
		{
			var directory = Path.Combine(FileHandle.RootFolderPath, directoryName);
			Directory.CreateDirectory(directory);

			return Path.Combine(directory, fileName);
		}

		/// <summary>
		/// Gets the root folder path.
		/// </summary>
		/// <value>The root folder path.</value>
		public static string RootFolderPath {
			get {
				return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
		}
	}
}