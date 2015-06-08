using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics
{
	internal abstract class ActivatableD3DResource : ActivatablePlatformResource
	{
		protected DeviceManager DeviceManager { get; private set; }

		protected ActivatableD3DResource(DeviceManager deviceManager, string id)
			: base(id)
		{
			DeviceManager = deviceManager;
			DeviceManager.Initialized += () =>
			{
				if(IsInitialized)
					Initialize();
			};
		}
	}
}
