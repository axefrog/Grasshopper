using System;
using Grasshopper.Assets;
using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	class GraphicsContextFactory : IGraphicsContextFactory
	{
		private readonly Lazy<IAssetStore> _assets;

		public GraphicsContextFactory(Lazy<IAssetStore> assets)
		{
			_assets = assets;
		}

		public IGraphicsContext CreateContext()
		{
			var gfx = new GraphicsContext(_assets.Value);
			gfx.Initialize();
			return gfx;
		}
	}
}
