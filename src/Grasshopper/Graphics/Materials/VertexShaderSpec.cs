using System.Collections.Generic;

namespace Grasshopper.Graphics.Materials
{
	public class VertexShaderSpec : ShaderSpec
	{
		public VertexShaderSpec(string source,
			IEnumerable<ShaderInputElementSpec> perInstanceElements = null,
			IEnumerable<ShaderInputElementSpec> perVertexElements = null) : base(source)
		{
			PerInstanceElements = perInstanceElements ?? new ShaderInputElementSpec[0];
			PerVertexElements = perVertexElements ?? new[]
			{
				ShaderInputElementPurpose.Position.Spec(),
				ShaderInputElementPurpose.Color.Spec(),
				ShaderInputElementPurpose.TextureCoordinate.Spec(),
			};
		}

		public IEnumerable<ShaderInputElementSpec> PerInstanceElements { get; private set; }
		public IEnumerable<ShaderInputElementSpec> PerVertexElements { get; private set; }
	}
}