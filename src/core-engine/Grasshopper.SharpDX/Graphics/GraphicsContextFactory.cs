using System;
using Grasshopper.Graphics;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics
{
	class GraphicsContextFactory : IGraphicsContextFactory
	{
		private readonly Lazy<IFileStore> _assets;

		public GraphicsContextFactory(Lazy<IFileStore> assets)
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
