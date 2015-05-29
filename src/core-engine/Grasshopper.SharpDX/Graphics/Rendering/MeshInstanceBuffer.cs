using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshInstanceBuffer<T> : ActivatableD3DResource, IMeshInstanceCollection<T>
		where T : struct
	{
		private List<T> _instances;
		private static readonly int _sizeofT = Utilities.SizeOf<T>();

		public MeshInstanceBuffer(DeviceManager deviceManager, string id)
			: base(deviceManager, id)
		{
			Disposed += resource => _instances = null;
		}

		public VertexBufferBinding InstanceBufferBinding { get; private set; }
		public Buffer InstanceBuffer { get; private set; }

		public void SetData(List<T> instances)
		{
			if(instances == null)
				throw new ArgumentNullException("instances");

			_instances = new List<T>(instances);
		}

		protected override void InitializeInternal()
		{
			if(_instances == null)
				throw new InvalidOperationException("Cannot initialize index buffer; no instances have been assigned to this collection yet. Did you forget to call Update?");

			var len = _sizeofT * _instances.Count;
			DataStream stream;
			InstanceBuffer = new Buffer(DeviceManager.Device, len, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
			DeviceManager.Context.MapSubresource(InstanceBuffer, MapMode.WriteDiscard, MapFlags.None, out stream);
			foreach(var item in _instances)
				stream.Write(item);
			stream.Position = 0;
			DeviceManager.Device.ImmediateContext.UnmapSubresource(InstanceBuffer, 0);
			InstanceBufferBinding = new VertexBufferBinding(InstanceBuffer, _sizeofT, 0);
		}

		protected override void UninitializeInternal()
		{
			if(InstanceBuffer != null)
			{
				InstanceBuffer.Dispose();
				InstanceBuffer = null;
			}
			InstanceBufferBinding = default(VertexBufferBinding);
		}

		protected override void SetActive()
		{
			DeviceManager.Context.InputAssembler.SetVertexBuffers(1, InstanceBufferBinding);
		}
	}
}