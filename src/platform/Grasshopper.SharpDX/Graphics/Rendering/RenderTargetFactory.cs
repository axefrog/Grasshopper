using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class RenderTargetFactory : IRenderTargetFactory
	{
		private readonly GraphicsContext _graphicsContext;
		private readonly IInputContext _input;

		public RenderTargetFactory(GraphicsContext graphicsContext, IInputContext input)
		{
			_graphicsContext = graphicsContext;
			_input = input;
		}

		public IWindowRenderTarget CreateWindow()
		{
			var renderer = new WindowRenderTarget(_graphicsContext, _input);
			renderer.Initialize();
			return renderer;
		}

		//public IWindowRenderTarget CreateCompositeWindow()
		//{
		//	var renderer = new WindowRenderTarget(_graphicsContext.DeviceManager, _input);
		//	renderer.Initialize();
		//	return renderer;
		//}

		public ITextureRenderTarget CreateTexture(int width, int height)
		{
			var renderHost = new TextureRenderTarget(_graphicsContext.DeviceManager, width, height);
			renderHost.Initialize();
			return renderHost;
		}
	}
}
