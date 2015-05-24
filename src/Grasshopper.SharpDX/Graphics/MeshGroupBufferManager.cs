using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Rendering;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics
{
	class MeshGroupBufferManager : IMeshGroupBufferManager
	{
		private readonly DeviceManager _deviceManager;
		private readonly Dictionary<string, MeshGroupBuffer> _vertexBuffers = new Dictionary<string, MeshGroupBuffer>();
		private MeshGroupBuffer _activeGroup;

		public MeshGroupBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public void Add(MeshGroup meshes)
		{
			var resource = new MeshGroupBuffer(meshes);
			resource.Initialize(_deviceManager);
			_vertexBuffers.Add(meshes.Id, resource);
		}

		public void Remove(string id)
		{
			MeshGroupBuffer resource;
			if(_vertexBuffers.TryGetValue(id, out resource))
			{
				resource.Dispose();
				_vertexBuffers.Remove(id);
			}
		}

		public void SetActive(string meshGroupId)
		{
			MeshGroupBuffer group;
			if(!_vertexBuffers.TryGetValue(meshGroupId, out group))
				throw new ArgumentOutOfRangeException("meshGroupId", "The specified mesh group was not found");

			_activeGroup = group;
			_deviceManager.Context.InputAssembler.SetVertexBuffers(0, group.VertexBufferBinding);
			_deviceManager.Context.InputAssembler.SetIndexBuffer(group.IndexBuffer, Format.R32_UInt, 0);
		}

		public VertexBufferLocation GetMeshLocation(string id)
		{
			if(_activeGroup == null)
				throw new InvalidOperationException("Cannot get mesh location; there is no active group. Did you forget to call SetActive?");
			return _activeGroup[id];
		}

		public void Dispose()
		{
			_activeGroup = null;
			foreach(var material in _vertexBuffers.Values)
				material.Dispose();
			_vertexBuffers.Clear();
		}
	}
}
