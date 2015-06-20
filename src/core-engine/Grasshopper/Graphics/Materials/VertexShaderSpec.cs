using System;
using System.Collections.Generic;

namespace Grasshopper.Graphics.Materials
{
	public class VertexShaderSpec : ShaderSpec
	{
		public VertexShaderSpec(string id, string source, IEnumerable<ShaderInputElementSpec> perVertexElements, IEnumerable<ShaderInputElementSpec> perInstanceElements = null)
			: base(id, source)
		{
			if(perVertexElements == null)
				throw new ArgumentNullException("perVertexElements");
			PerVertexElements = perVertexElements;
			PerInstanceElements = perInstanceElements ?? new ShaderInputElementSpec[0];
		}

		public VertexShaderSpec(string source, IEnumerable<ShaderInputElementSpec> perVertexElements, IEnumerable<ShaderInputElementSpec> perInstanceElements = null)
			: base(source)
		{
			if(perVertexElements == null)
				throw new ArgumentNullException("perVertexElements");
			PerVertexElements = perVertexElements;
			PerInstanceElements = perInstanceElements ?? new ShaderInputElementSpec[0];
		}

		public IEnumerable<ShaderInputElementSpec> PerInstanceElements { get; private set; }
		public IEnumerable<ShaderInputElementSpec> PerVertexElements { get; private set; }
	}
}