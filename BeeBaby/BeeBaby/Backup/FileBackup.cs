using System;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Skahal.Infrastructure.Framework.Domain;

namespace BeeBaby.Backup
{
	public abstract class FileBackup<T> : FileHandle where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.FileBackup`1"/> class.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="value">Value.</param>
		public FileBackup(string filePath, T value = null)
		{
			FilePath = filePath;
			Load(value);
		}

		/// <summary>
		/// Load the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public void Load(T value = null)
		{
			if (value == null)
			{
				ReadAndUpdate(base.Load(FilePath));
			}
			else
			{
				TextData = JsonConvert.SerializeObject(value);
			}
		}

		/// <summary>
		/// Save this instance.
		/// </summary>
		public void Save()
		{
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(TextData)))
			{
				base.Save(FilePath, stream);
			}
		}

		/// <summary>
		/// Restore this instance.
		/// </summary>
		public bool Restore()
		{
			try
			{
				PerformAction();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ocorreu um erro ao restaurar o arquivo \"" + Path.GetFileName(FilePath) + "\":\n" + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Reads the and update.
		/// </summary>
		/// <param name="stream">Stream.</param>
		public void ReadAndUpdate(Stream stream)
		{
			using (stream)
			{
				using (var streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					TextData = streamReader.ReadToEnd();
				}
			}
		}

		/// <summary>
		/// Performs the action.
		/// </summary>
		protected abstract void PerformAction();

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		public T GetValue()
		{
			return JsonConvert.DeserializeObject<T>(TextData);
		}

		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>The file path.</value>
		public string FilePath {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the text data.
		/// </summary>
		/// <value>The text data.</value>
		public string TextData {
			get;
			set;
		}
	}
}