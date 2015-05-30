using System;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	public class CompiledMaterial : IDisposable
	{
		public CompiledMaterial(MaterialSpec spec)
		{
			Spec = spec;
		}

		public InputLayout InputLayout { get; internal set; }
		public VertexShader VertexShader { get; internal set; }
		public PixelShader PixelShader { get; internal set; }
		public MaterialSpec Spec { get; private set; }

		public void Dispose()
		{
			InputLayout.Dispose();
			InputLayout = null;
			VertexShader.Dispose();
			VertexShader = null;
			PixelShader.Dispose();
			PixelShader = null;
		}
	}
}
