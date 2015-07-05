using System;
using Grasshopper.Graphics;
using Grasshopper.Input;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics
{
    class GraphicsContextFactory : IGraphicsContextFactory
    {
        private readonly Lazy<IFileStore> _assets;
        private readonly IInputContext _input;

        public GraphicsContextFactory(Lazy<IFileStore> assets, IInputContext input)
        {
            _assets = assets;
            _input = input;
        }

        public IGraphicsContext CreateContext(bool enableDebugMode = false)
        {
            var gfx = new GraphicsContext(_assets, _input);
            gfx.Initialize();
            return gfx;
        }
    }
}
