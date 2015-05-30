using System;
using System.Collections.Generic;

namespace Grasshopper.Graphics.Materials
{
	public class MaterialSpec
	{
		public MaterialSpec()
			: this(Guid.NewGuid().ToString())
		{
		}

		public MaterialSpec(string id)
		{
			Id = id;
			Textures = new List<string>();
			Samplers = new List<string>();
		}

		public string Id { get; private set; }
		public bool IsTranslucent { get; set; }
		public List<string> Textures { get; set; }
		public List<string> Samplers { get; set; }
		public VertexShaderSpec VertexShader { get; set; }
		public PixelShaderSpec PixelShader { get; set; }
		
		public MaterialSpec WithTexture(params string[] textureId)
		{
			Textures.AddRange(textureId);
			return this;
		}
	}
}