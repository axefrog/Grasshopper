using System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace Grasshopper.SharpDX.Graphics
{
	public class DeviceManager : IDisposable
	{
		public bool EnableDebugMode { get; set; }
		
		public Device1 Device { get; private set; }
		public DeviceContext1 Context { get; private set; }
		public bool IsInitialized { get; private set; }

		public void Initialize()
		{
			DestroyResources();

			var flags = DeviceCreationFlags.BgraSupport;
			if(EnableDebugMode) flags &= DeviceCreationFlags.Debug;

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
