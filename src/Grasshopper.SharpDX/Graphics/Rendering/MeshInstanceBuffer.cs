using System;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class MeshInstanceBuffer<T> : IDisposable where T : struct
	{
		private readonly DeviceManager _deviceManager;
		private readonly T[] _instances;
		private static readonly int _sizeofT = Utilities.SizeOf<T>();

		public MeshInstanceBuffer(DeviceManager deviceManager, T[] instances)
		{
			_deviceManager = deviceManager;
			_instances = instances;
		}

		public VertexBufferBinding InstanceBufferBinding { get; private set; }
		public Buffer InstanceBuffer { get; private set; }
		public bool Initialized { get; private set; }

		public void Initialize()
		{
			var len = _sizeofT * _instances.Length;
			DataStream stream;
			InstanceBuffer = new Buffer(_deviceManager.Device, len, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
			_deviceManager.Context.MapSubresource(InstanceBuffer, MapMode.WriteDiscard, MapFlags.None, out stream);
			foreach(var item in _instances)
				stream.Write(item);
			stream.Position = 0;
			_deviceManager.Device.ImmediateContext.UnmapSubresource(InstanceBuffer, 0);
			InstanceBufferBinding = new VertexBufferBinding(InstanceBuffer, _sizeofT, 0);
		}

		public void Uninitialize()
		{
			if(InstanceBuffer != null)
			{
				InstanceBuffer.Dispose();
				InstanceBuffer = null;
			}
			InstanceBufferBinding = default(VertexBufferBinding);
			Initialized = false;
		}

		public void Dispose()
		{
			Uninitialize();
		}
	}
}