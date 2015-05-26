using System;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class ConstantBuffer<T> : IDisposable
		where T : struct
	{
		private readonly DeviceManager _deviceManager;
		private T _data;
		private static readonly int _sizeofT = Utilities.SizeOf<T>();

		public ConstantBuffer(DeviceManager deviceManager, T data)
		{
			_deviceManager = deviceManager;
			_data = data;
		}

		public Buffer Buffer { get; private set; }
		public bool Initialized { get; private set; }

		public void Initialize()
		{
			Buffer = new Buffer(_deviceManager.Device, _sizeofT, ResourceUsage.Default, BindFlags.ConstantBuffer,
				CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofT);
			_deviceManager.Context.UpdateSubresource(ref _data, Buffer);
		}

		public void Update(T newData)
		{
			_data = newData;
			_deviceManager.Context.UpdateSubresource(ref _data, Buffer);
		}

		public void Uninitialize()
		{
			if(Buffer != null)
			{
				Buffer.Dispose();
				Buffer = null;
			}
			Initialized = false;
		}

		public void Dispose()
		{
			Uninitialize();
		}
	}
}
