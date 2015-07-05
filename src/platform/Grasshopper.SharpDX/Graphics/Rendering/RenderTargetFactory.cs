using System;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class RenderTargetFactory : IRenderTargetFactory
    {
        private readonly GraphicsContext _graphics;
        private readonly IInputContext _input;

        public RenderTargetFactory(GraphicsContext graphics, IInputContext input)
        {
            if(graphics == null) throw new ArgumentNullException("graphics");
            if(input == null) throw new ArgumentNullException("input");
            _graphics = graphics;
            _input = input;
        }

        public IWindowRenderTarget CreateWindow()
        {
            var renderer = new WindowRenderTarget(_graphics, _input);
            renderer.Initialize();
            return renderer;
        }

        //public IWindowRenderTarget CreateCompositeWindow()
        //{
        //    var renderer = new WindowRenderTarget(_graphicsContext.DeviceManager, _input);
        //    renderer.Initialize();
        //    return renderer;
        //}

        public ITextureRenderTarget CreateTexture(int width, int height)
        {
            var renderHost = new TextureRenderTarget(_graphics.DeviceManager, width, height);
            renderHost.Initialize();
            return renderHost;
        }
    }
}
