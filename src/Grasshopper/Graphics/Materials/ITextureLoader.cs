using Grasshopper.Assets;

namespace Grasshopper.Graphics.Materials
{
	public interface ITextureLoader
	{
		ITexture Load(string path);
		ITexture Load(IAssetResource asset);
	}
}