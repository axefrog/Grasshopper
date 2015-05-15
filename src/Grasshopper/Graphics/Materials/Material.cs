using System.Collections.Generic;

namespace Grasshopper.Graphics.Materials
{
	public class Material
	{
		public Material()
		{
			Textures = new List<ITexture>();
		}

		public List<ITexture> Textures { get; private set; }
	}
}