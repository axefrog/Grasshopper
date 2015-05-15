using Grasshopper.Assets;

namespace Grasshopper.WindowsFileSystem
{
	public class AssetResourceFactory : IAssetResourceFactory
	{
		public IAssetResource Create(string path)
		{
			return new AssetResource(path);
		}
	}
}
