using Grasshopper.Assets;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics
{
	public class TextureLoader : ITextureLoader
	{
		private readonly DeviceManager _deviceManager;
		private readonly IAssetResourceFactory _assets;

		public TextureLoader(DeviceManager deviceManager, IAssetResourceFactory assets)
		{
			_deviceManager = deviceManager;
			_assets = assets;
		}

		public ITexture Load(string path)
		{
			var asset = _assets.Create(path);
			var texture = Load(asset);
			return texture;
		}

		public ITexture Load(IAssetResource asset)
		{
			using(var stream = asset.OpenRead())
			{
				var texture = new Texture(asset, ShaderResourceView.FromStream(_deviceManager.Device, stream, asset.Size));
				return texture;
			}
		}
	}
}
