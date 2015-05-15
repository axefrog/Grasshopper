using Grasshopper.Assets;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics
{
	public class Texture : ITexture
	{
		public IAssetResource Asset { get; private set; }
		public ShaderResourceView ShaderResourceView { get; private set; }

		public Texture(IAssetResource asset, ShaderResourceView shaderResourceView)
		{
			Asset = asset;
			ShaderResourceView = shaderResourceView;
		}

		public void Dispose()
		{
			ShaderResourceView.Dispose();
		}
	}
}