using Grasshopper.SharpDX.Graphics;
using Microsoft.Practices.Unity;

namespace Grasshopper.SharpDX
{
	public static class SharpDXBootstrapper
	{
		public static GrasshopperApp CreateGrasshopperApp()
		{
			var services = new ServiceLocator();
			PopulateServiceLocator(services);
			var app = new GrasshopperApp(services);
			return app;
		}

		public static void PopulateServiceLocator(ServiceLocator services)
		{
			var container = new UnityContainer();
			services.Windows = new AppWindowFactory(container);
		}
	}
}
