using Grasshopper.Assets;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics
{
	public class TextureLoader : ITextureLoader
	{
		private readonly DeviceManager _deviceManager;
		private readonly IAssetStore _assets;

		public TextureLoader(DeviceManager deviceManager, IAssetStore assets)
		{
			_deviceManager = deviceManager;
			_assets = assets;
		}

		public ITexture Load(string path)
		{
			var asset = _assets.GetFile(path);
			var texture = Load(asset);
			return texture;
		}

		public ITexture Load(IAssetSource asset)
		{
			// todo: make sure the texture has actually loaded successfully - note that it may not be loading from a regular file, so we can't rely on FileNotFoundException bubbling up; wrap it in a local exception
			using(var stream = asset.OpenRead())
			{
				var texture = new Texture(asset, ShaderResourceView.FromStream(_deviceManager.Device, stream, asset.Size));
				return texture;
			}
		}
	}
}
