using System;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class MeshInstanceBuffer<T> : IDisposable where T : struct
	{
		private readonly T[] _instances;
		private static readonly int _sizeofT = Utilities.SizeOf<T>();

		public MeshInstanceBuffer(T[] instances)
		{
			_instances = instances;
		}

		public VertexBufferBinding InstanceBufferBinding { get; private set; }
		public Buffer InstanceBuffer { get; private set; }
		public bool Initialized { get; private set; }

		public void Initialize(DeviceManager deviceManager)
		{
			var len = _sizeofT * _instances.Length;
			DataStream stream;
			InstanceBuffer = new Buffer(deviceManager.Device, len, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
			deviceManager.Context.MapSubresource(InstanceBuffer, MapMode.WriteDiscard, MapFlags.None, out stream);
			foreach(var item in _instances)
				stream.Write(item);
			stream.Position = 0;
			deviceManager.Device.ImmediateContext.UnmapSubresource(InstanceBuffer, 0);
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