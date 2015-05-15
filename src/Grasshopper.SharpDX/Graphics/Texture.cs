using Grasshopper.Graphics;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics
{
	public class Texture : ITexture
	{
		public ShaderResourceView ShaderResourceView { get; private set; }

		public Texture(string path, ShaderResourceView shaderResourceView)
		{
			Path = path;
			ShaderResourceView = shaderResourceView;
		}

		public string Path { get; private set; }

		public void Dispose()
		{
			ShaderResourceView.Dispose();
		}
	}
}