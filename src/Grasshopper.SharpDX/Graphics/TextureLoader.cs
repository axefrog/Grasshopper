using Grasshopper.Graphics;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics
{
	public class TextureLoader : ITextureLoader
	{
		private readonly DeviceManager _deviceManager;

		public TextureLoader(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public ITexture Load(string path)
		{
			var texture = new Texture(path, ShaderResourceView.FromFile(_deviceManager.Device, path));
			return texture;
		}
	}
}
