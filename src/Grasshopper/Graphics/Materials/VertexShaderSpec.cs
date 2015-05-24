using System.Collections.Generic;

namespace Grasshopper.Graphics.Materials
{
	public class VertexShaderSpec : ShaderSpec
	{
		public VertexShaderSpec(string source, IEnumerable<ShaderInputElementSpec> perVertexElements) : base(source)
		{
			PerVertexElements = perVertexElements;
		}

		public IEnumerable<ShaderInputElementSpec> PerVertexElements { get; private set; }
		public IEnumerable<ShaderInputElementSpec> PerInstanceElements { get; private set; }
	}
}