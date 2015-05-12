using Grasshopper.Graphics;
using Microsoft.Practices.Unity;

namespace Grasshopper.SharpDX.Graphics
{
	public class AppWindowFactory : IAppWindowFactory
	{
		private readonly IUnityContainer _container;

		public AppWindowFactory(IUnityContainer container)
		{
			container.RegisterType<IAppWindow, AppWindow>(new TransientLifetimeManager());
			_container = container;
		}

		public IAppWindow Create()
		{
			return _container.Resolve<IAppWindow>();
		}
	}
}
