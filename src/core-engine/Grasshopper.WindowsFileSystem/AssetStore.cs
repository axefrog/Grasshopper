using Grasshopper.Assets;

namespace Grasshopper.WindowsFileSystem
{
	public class AssetStore : IAssetStore
	{
		public IAssetSource GetFile(string path)
		{
			return new AssetSource(path);
		}
	}
}
