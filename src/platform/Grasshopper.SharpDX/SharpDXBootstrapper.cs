using System;
using Grasshopper.Platform;
using Grasshopper.SharpDX.Graphics;
using Grasshopper.SharpDX.Input;

namespace Grasshopper.SharpDX
{
    public static class SharpDXBootstrapper
    {
        public static T UseSharpDX<T>(this T app) where T : GrasshopperApp
        {
            app.Input = new InputContext();
            var factory = new GraphicsContextFactory(new Lazy<IFileStore>(() =>
            {
                if(app.Files == null)
                    throw new InvalidOperationException("No implementation for IFileStore was initialized. Did you forget to reference and call app.UseWindowsFileSystem() or a platform-specific alternative?");
                return app.Files;
            }), app.Input);
            app.Graphics = factory;
            return app;
        }
    }
}
