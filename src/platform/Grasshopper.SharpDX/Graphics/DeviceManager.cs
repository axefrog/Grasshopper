using System;
using Grasshopper.Graphics;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace Grasshopper.SharpDX.Graphics
{
    public class DeviceManager : IDeviceManager
    {
        public Device1 Device { get; private set; }
        public DeviceContext1 Context { get; private set; }
        public bool IsInitialized { get; private set; }

        public event Action Initialized;

        public void Initialize()
        {
            DestroyResources();

            var flags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            flags |= DeviceCreationFlags.Debug;
#endif

            var featureLevels = new[]
            {
                FeatureLevel.Level_11_1,
                FeatureLevel.Level_11_0
            };

            using(var device = new Device(DriverType.Hardware, flags, featureLevels))
            {
                Device = device.QueryInterface<Device1>();
                Context = device.ImmediateContext.QueryInterface<DeviceContext1>();
            }

            IsInitialized = true;

            // todo: Reinitialize all dependent resources by having them hook this event
            var handler = Initialized;
            if(handler != null)
                handler();
        }

        public void GetDeviceCapabilities()
        {
        }

        private void DestroyResources()
        {
            IsInitialized = false;
            if(Device != null)
            {
                Device.Dispose();
                Device = null;
            }
        }

        public void Dispose()
        {
            DestroyResources();
        }
    }
}
