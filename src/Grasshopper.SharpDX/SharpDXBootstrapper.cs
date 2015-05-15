using Grasshopper.SharpDX.Graphics;

namespace Grasshopper.SharpDX
{
	public static class SharpDXBootstrapper
	{
		public static T UseSharpDX<T>(this T app) where T : GrasshopperApp
		{
			var factory = new GraphicsContextFactory();
			app.GraphicsContextFactory = factory;
			return app;
		}
	}
}
