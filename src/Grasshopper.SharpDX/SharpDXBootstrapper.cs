using Grasshopper.SharpDX.Graphics;

namespace Grasshopper.SharpDX
{
	public static class SharpDXBootstrapper
	{
		public static GrasshopperApp UseSharpDX(this GrasshopperApp app)
		{
			var factory = new GraphicsContextFactory();
			app.GraphicsContextFactory = factory;
			return app;
		}
	}
}
