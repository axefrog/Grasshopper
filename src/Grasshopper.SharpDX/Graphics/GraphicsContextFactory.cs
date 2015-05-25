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

		public IGraphicsContext CreateContext(bool enableDebugMode = false)
		{
			var gfx = new GraphicsContext(_assets.Value, enableDebugMode);
			gfx.Initialize();
			return gfx;
		}
	}
}
