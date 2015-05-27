namespace Grasshopper.Assets
{
	public interface IAssetStore
	{
		IAssetSource GetFile(string path);
	}
}
