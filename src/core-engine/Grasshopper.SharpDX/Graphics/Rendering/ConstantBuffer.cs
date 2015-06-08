using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class ConstantBufferResource<T> : IndexActivatableD3DResource, IConstantBufferResource<T>
		where T : struct
	{
		private readonly DeviceManager _deviceManager;
		private static readonly int _sizeofT = Utilities.SizeOf<T>();
		private T _data;

		public ConstantBufferResource(DeviceManager deviceManager, string id)
			: base(deviceManager, id)
		{
			_deviceManager = deviceManager;
		}

		public Buffer Buffer { get; private set; }

		protected override void InitializeInternal()
		{
			Buffer = new Buffer(_deviceManager.Device, _sizeofT, ResourceUsage.Default, BindFlags.ConstantBuffer,
				CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofT);
			_deviceManager.Context.UpdateSubresource(ref _data, Buffer);
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
			_deviceManager.Context.VertexShader.SetConstantBuffer(index, Buffer);
		}

		public void Update(ref T newData)
		{
			_data = newData;
			if(IsInitialized)
				DeviceManager.Context.UpdateSubresource(ref _data, Buffer);
		}
	}
}
