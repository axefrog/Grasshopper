using System.Collections.Generic;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshGroupBufferManager : ActivatablePlatformResourceManager<IMeshGroup>, IMeshGroupBufferManager
	{
		private readonly DeviceManager _deviceManager;

		public MeshGroupBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override IMeshGroup CreateResource(string id)
		{
			var resource = new MeshGroupBuffer(_deviceManager, id);
			return resource;
		}

		public IMeshGroup Create(string id, IEnumerable<Mesh> meshes)
		{
			return CreateAndAdd(id, buffer =>
			{
				buffer.AddRange(meshes);
				buffer.Initialize();
			});
		}

		public IMeshGroup Create(string id, params Mesh[] mesh)
		{
			return Create(id, (IEnumerable<Mesh>)mesh);
		}
	}
}
