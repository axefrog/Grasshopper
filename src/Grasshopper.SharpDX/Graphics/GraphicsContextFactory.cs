using System;
using Grasshopper.Assets;
using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	class GraphicsContextFactory : IGraphicsContextFactory
	{
		private readonly Lazy<IAssetResourceFactory> _assets;

		public GraphicsContextFactory(Lazy<IAssetResourceFactory> assets)
		{
			_assets = assets;
		}

		public IGraphicsContext Create()
		{
			var gfx = new GraphicsContext(_assets.Value);
			gfx.Initialize();
			return gfx;
		}
	}
}
