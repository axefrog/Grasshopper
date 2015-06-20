using System;
using Grasshopper.Graphics.Rendering.Buffers;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class ConstantBufferResource<T> : IndexActivatableD3DResource, IConstantBufferResource<T>
		where T : struct
	{
		private static readonly int _sizeofT = Utilities.SizeOf<T>();

		public ConstantBufferResource(DeviceManager deviceManager, string id)
			: base(deviceManager, id)
		{
		}

		public Buffer Buffer { get; private set; }

		public void Update(T data)
		{
			Update(ref data);
		}

		public void Update(ref T data)
		{
			if(!IsInitialized)
				throw new Exception();
			DeviceManager.Context.UpdateSubresource(ref data, Buffer);
		}

		protected override void InitializeInternal()
		{
			Buffer = new Buffer(DeviceManager.Device, _sizeofT, ResourceUsage.Default, BindFlags.ConstantBuffer,
				CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofT);
		}

		protected override void UninitializeInternal()
		{
			if(Buffer != null)
			{
				Buffer.Dispose();
				Buffer = null;
			}
		}

		protected override void ActivateAtIndex(int index)
		{
			DeviceManager.Context.VertexShader.SetConstantBuffer(index, Buffer);
		}

		private class ConstantBufferWriter : IConstantBufferDataWriter<T>
		{
			private DeviceManager _deviceManager;
			private Buffer _buffer;

			public ConstantBufferWriter(string id, DeviceManager deviceManager, Buffer buffer)
			{
				_deviceManager = deviceManager;
				_buffer = buffer;
				Id = id;
			}

			public string Id { get; private set; }
			public void WriteData(ref T data)
			{
				_deviceManager.Context.UpdateSubresource(ref data, _buffer);
			}

			public void Dispose()
			{
				_deviceManager = null;
				_buffer = null;
			}
		}
	}
}
