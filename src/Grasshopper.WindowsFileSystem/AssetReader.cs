using System;
using System.IO;
using Grasshopper.Assets;

namespace Grasshopper.WindowsFileSystem
{
	public class AssetReader : IAssetReader
	{
		public Stream OpenRead(string path)
		{
			path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
			return File.OpenRead(path);
		}
	}
}
