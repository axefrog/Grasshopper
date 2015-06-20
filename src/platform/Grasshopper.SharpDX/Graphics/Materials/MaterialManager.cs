using Grasshopper.Graphics.Materials;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	class MaterialManager : ActivatablePlatformResourceManager<Material>, IMaterialManager
	{
		private readonly DeviceManager _deviceManager;
		private readonly ITextureResourceManager _textureResourceManager;
		private readonly ITextureSamplerManager _textureSamplerManager;

		public MaterialManager(GraphicsContext gfx)
		{
			_deviceManager = gfx.DeviceManager;
			_textureResourceManager = gfx.TextureResourceManager;
			_textureSamplerManager = gfx.TextureSamplerManager;
		}

		protected override Material CreateResource(string id)
		{
			return new MaterialResource(_deviceManager, _textureResourceManager, _textureSamplerManager, id);
		}

		public Material Create(string id)
		{
			return CreateAndAdd(id);
		}
	}
}
