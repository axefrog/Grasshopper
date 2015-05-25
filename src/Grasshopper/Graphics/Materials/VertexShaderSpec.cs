using System.Collections.Generic;

namespace Grasshopper.Graphics.Materials
{
	public class VertexShaderSpec : ShaderSpec
	{
		public static readonly ShaderInputElementSpec[] DefaultPerVertexElements =
		{
			ShaderInputElementPurpose.Position.Spec(),
			ShaderInputElementPurpose.Color.Spec(),
			ShaderInputElementPurpose.TextureCoordinate.Spec(),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float2, ShaderInputElementPurpose.Padding),
		};

		public VertexShaderSpec(string source,
			IEnumerable<ShaderInputElementSpec> perInstanceElements = null) : base(source)
		{
			PerInstanceElements = perInstanceElements ?? new ShaderInputElementSpec[0];
		}

		public IEnumerable<ShaderInputElementSpec> PerInstanceElements { get; private set; }
		//public IEnumerable<ShaderInputElementSpec> PerVertexElements { get; private set; }
	}
}