using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;
using Grasshopper.SharpDX.Input;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class RenderHostFactory : IRenderHostFactory
	{
		private readonly GraphicsContext _graphicsContext;
		private readonly IInputContext _input;

		public RenderHostFactory(GraphicsContext graphicsContext, IInputContext input)
		{
			_graphicsContext = graphicsContext;
			_input = input;
		}

		public IWindowRenderHost CreateWindowed()
		{
			var renderer = new WindowRenderHost(_graphicsContext.DeviceManager, _input);
			renderer.Initialize();
			return renderer;
		}
	}
}
