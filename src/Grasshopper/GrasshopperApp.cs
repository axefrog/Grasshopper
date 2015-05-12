using System;
using System.Threading;

namespace Grasshopper
{
	public class GrasshopperApp : IDisposable
	{
		public GrasshopperApp(ServiceLocator services)
		{
			Services = services;
		}

		public ServiceLocator Services { get; private set; }

		public void Dispose()
		{
		}

		public void Run(Func<bool> main)
		{
			using(var ev = new AutoResetEvent(false))
				while(main())
					ev.WaitOne(1);
		}
	}
}
