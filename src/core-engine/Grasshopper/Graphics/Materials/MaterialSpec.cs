using System.Collections.Generic;
using Grasshopper.Assets;

namespace Grasshopper.Graphics.Materials
{
	public class MaterialSpec : Asset
	{
		public MaterialSpec()
		{
		}

		public MaterialSpec(string id)
		{
			((IAsset)this).SetId(id);
			Textures = new List<string>();
		}

		public bool IsTranslucent { get; set; }
		public List<string> Textures { get; private set; }
		public VertexShaderSpec VertexShader { get; set; }
		public PixelShaderSpec PixelShader { get; set; }
		
		public MaterialSpec WithTexture(params string[] textureId)
		{
			Textures.AddRange(textureId);
			return this;
		}
	}
}