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

		// todo: allow alternate vertex configurations, rather than just the built-in vertex type i'm already using
		// todo: allow input elements to be declared via custom attributes on the associated instance struct, and then have that automatically generate the perInstanceElements collection below
		public VertexShaderSpec(string id, string source, IEnumerable<ShaderInputElementSpec> perInstanceElements)
			: base(id, source)
		{
			PerInstanceElements = perInstanceElements;
		}

		public VertexShaderSpec(string source, IEnumerable<ShaderInputElementSpec> perInstanceElements = null)
			: base(source)
		{
			PerInstanceElements = perInstanceElements ?? new ShaderInputElementSpec[0];
		}

		public IEnumerable<ShaderInputElementSpec> PerInstanceElements { get; private set; }
		//public IEnumerable<ShaderInputElementSpec> PerVertexElements { get; private set; }
	}
}