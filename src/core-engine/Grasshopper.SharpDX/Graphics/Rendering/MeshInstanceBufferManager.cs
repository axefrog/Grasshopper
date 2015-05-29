using System.Collections.Generic;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshInstanceBufferManager<T> : ActivatablePlatformResourceManager<IMeshInstanceCollection<T>>, IMeshInstanceBufferManager<T>
		where T : struct
	{
		private readonly DeviceManager _deviceManager;

		public MeshInstanceBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override IMeshInstanceCollection<T> CreateResource(string id)
		{
			return new MeshInstanceBuffer<T>(_deviceManager, id);
		}

		public IMeshInstanceCollection<T> Create(string id, List<T> instances)
		{
			return CreateAndAdd(id, resource =>
			{
				resource.SetData(instances);
				resource.Initialize();
			});
		}

		public void SetData(string id, List<T> instances)
		{
			this[id].SetData(instances);
		}
	}
}