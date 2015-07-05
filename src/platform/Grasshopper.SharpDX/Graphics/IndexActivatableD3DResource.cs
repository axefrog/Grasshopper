using System;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics
{
    abstract class IndexActivatableD3DResource : IndexActivatablePlatformResource
    {
        protected DeviceManager DeviceManager { get; private set; }

        protected IndexActivatableD3DResource(DeviceManager deviceManager, string id) : base(id)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");

            DeviceManager = deviceManager;
            DeviceManager.Initialized += () =>
            {
                if(IsInitialized)
                    Initialize();
            };
        }
    }
}