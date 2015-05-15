using System;
using Grasshopper.Assets;
using Grasshopper.SharpDX.Graphics;

namespace Grasshopper.SharpDX
{
	public static class SharpDXBootstrapper
	{
		public static T UseSharpDX<T>(this T app) where T : GrasshopperApp
		{
			var factory = new GraphicsContextFactory(new Lazy<IAssetResourceFactory>(() => app.Assets));
			app.Graphics = factory;
			return app;
		}
	}
}
