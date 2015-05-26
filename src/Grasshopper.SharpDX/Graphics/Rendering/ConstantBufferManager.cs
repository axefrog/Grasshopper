using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class ConstantBufferManager<T> : IConstantBufferManager<T>
		where T : struct
	{
		private readonly DeviceManager _deviceManager;
		private readonly Dictionary<string, ConstantBuffer<T>> _buffers = new Dictionary<string, ConstantBuffer<T>>();

		public ConstantBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public void Add(string id, T data)
		{
			var resource = new ConstantBuffer<T>(_deviceManager, data);
			resource.Initialize();
			_buffers.Add(id, resource);
		}

		public void Update(string id, T data)
		{
			ConstantBuffer<T> resource;
			if(!_buffers.TryGetValue(id, out resource))
				throw new ArgumentOutOfRangeException("id", string.Format("The specified constant buffer \"{0}\" was not found", id));
			resource.Update(data);
		}

		public void Remove(string id)
		{
			ConstantBuffer<T> resource;
			if(_buffers.TryGetValue(id, out resource))
			{
				resource.Dispose();
				_buffers.Remove(id);
			}
		}

		public void SetActive(string id, int slot = 0)
		{
			ConstantBuffer<T> resource;
			if(!_buffers.TryGetValue(id, out resource))
				throw new ArgumentOutOfRangeException("id", string.Format("The specified constant buffer \"{0}\" was not found", id));
			_deviceManager.Context.VertexShader.SetConstantBuffers(slot, resource.Buffer);
		}

		public void SetActive(params string[] ids)
		{
			for(var slot = 0; slot < ids.Length; slot++)
				SetActive(ids[slot], slot);
		}

		public void Dispose()
		{
			foreach(var buffer in _buffers.Values)
				buffer.Dispose();
			_buffers.Clear();
		}
	}
}
