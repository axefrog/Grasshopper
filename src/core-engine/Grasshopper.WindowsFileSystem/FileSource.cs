using System;
using System.IO;
using Grasshopper.Platform;

namespace Grasshopper.WindowsFileSystem
{
	public class FileSource : IFileSource
	{
		private readonly FileInfo _location;

		public FileSource(string path)
		{
			_location = new FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));
			Path = path;
			Size = (int)new FileInfo(_location.FullName).Length;
		}

		public Stream OpenRead()
		{
			return File.OpenRead(_location.FullName);
		}

		public string Path { get; private set; }
		public int Size { get; private set; }
	}
}