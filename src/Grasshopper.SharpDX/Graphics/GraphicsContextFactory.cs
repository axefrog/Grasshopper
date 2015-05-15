using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	class GraphicsContextFactory : IGraphicsContextFactory
	{
		public IGraphicsContext Create()
		{
			var gfx = new GraphicsContext();
			gfx.Initialize();
			return gfx;
		}
	}
}
