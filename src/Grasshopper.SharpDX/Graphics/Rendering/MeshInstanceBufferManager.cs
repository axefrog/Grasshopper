using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class MeshInstanceBufferManager<T> : IMeshInstanceBufferManager<T> where T : struct
	{
		private readonly DeviceManager _deviceManager;
		private readonly Dictionary<string, MeshInstanceBuffer<T>> _buffers = new Dictionary<string, MeshInstanceBuffer<T>>();
		private MeshInstanceBuffer<T> _activeBuffer;

		public MeshInstanceBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public void Add(string id, T[] instances)
		{
			var resource = new MeshInstanceBuffer<T>(instances);
			resource.Initialize(_deviceManager);
			_buffers.Add(id, resource);
		}

		public void Remove(string id)
		{
			MeshInstanceBuffer<T> resource;
			if(_buffers.TryGetValue(id, out resource))
			{
				resource.Dispose();
				_buffers.Remove(id);
			}
		}

		public void SetActive(string id)
		{
			MeshInstanceBuffer<T> resource;
			if(!_buffers.TryGetValue(id, out resource))
				throw new ArgumentOutOfRangeException("id", "The specified mesh group was not found");

			_activeBuffer = resource;
			_deviceManager.Context.InputAssembler.SetVertexBuffers(1, resource.InstanceBufferBinding);
		}

		public void Dispose()
		{
			_activeBuffer = null;
			foreach(var material in _buffers.Values)
				material.Dispose();
			_buffers.Clear();
		}
	}
}