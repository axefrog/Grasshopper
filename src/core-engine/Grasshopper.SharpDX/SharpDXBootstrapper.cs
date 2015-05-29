using System;
using Grasshopper.Platform;
using Grasshopper.SharpDX.Graphics;

namespace Grasshopper.SharpDX
{
	public static class SharpDXBootstrapper
	{
		public static T UseSharpDX<T>(this T app) where T : GrasshopperApp
		{
			var factory = new GraphicsContextFactory(new Lazy<IFileStore>(() =>
			{
				if(app.Files == null)
					throw new InvalidOperationException("No implementation for IFileStore was initialized. Did you forget to reference and call app.UseWindowsFileSystem() or a platform-specific alternative?");
				return app.Files;
			}));
			app.Graphics = factory;
			return app;
		}
	}
}
