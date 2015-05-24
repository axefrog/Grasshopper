using Grasshopper.Assets;

namespace Grasshopper.Graphics.Materials
{
	public class MaterialLibrary : AssetLibrary<MaterialSpec>
	{
		private readonly TextureLibrary _textures;

		public MaterialLibrary(TextureLibrary textures)
		{
			_textures = textures;
		}
	}
}
