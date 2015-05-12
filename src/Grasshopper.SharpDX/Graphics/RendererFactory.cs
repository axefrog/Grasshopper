using Grasshopper.Graphics;
using Microsoft.Practices.Unity;

namespace Grasshopper.SharpDX.Graphics
{
	public class RendererFactory : IRendererFactory
	{
		private readonly IUnityContainer _container;

		public RendererFactory(IUnityContainer container)
		{
			_container = container;
		}

		public IRenderer Create(IAppWindow window)
		{
			var deviceManager = new DeviceManager();
			deviceManager.Initialize();

			var viewportManager = new ViewportManager(window, deviceManager);
			viewportManager.Initialize();

			var renderer = new Renderer(deviceManager, viewportManager);
			renderer.Initialize();

			return renderer;
		}

		public IRenderer CreateWindowed()
		{
			var window = _container.Resolve<IAppWindow>();
			return Create(window);
		}
	}
}
