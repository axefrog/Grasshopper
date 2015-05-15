namespace Grasshopper.Assets
{
	public interface IAssetResourceFactory
	{
		IAssetResource Create(string path);
	}
}
