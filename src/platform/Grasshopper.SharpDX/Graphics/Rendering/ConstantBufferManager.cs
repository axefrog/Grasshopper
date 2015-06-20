using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class ConstantBufferManager<T> : IndexActivatablePlatformResourceManager<IConstantBufferResource<T>>, IConstantBufferManager<T> where T : struct
	{
		private readonly DeviceManager _deviceManager;

		public ConstantBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override IConstantBufferResource<T> CreateResource(string id)
		{
			return new ConstantBufferResource<T>(_deviceManager, id);
		}

		public IConstantBufferResource<T> Create(string id)
		{
			var resource = CreateAndAttachEventHandlers(id);
			resource.Initialize();
			AddAndNotifySubscribers(resource);
			return resource;
		}

		public void Update(string id, T data)
		{
			this[id].Update(ref data);
		}

		public void Update(string id, ref T data)
		{
			this[id].Update(ref data);
		}

		protected override void Activate(int firstIndex, IEnumerable<IConstantBufferResource<T>> resources)
		{
			_deviceManager.Context.VertexShader.SetConstantBuffers(firstIndex, resources.Select((r, i) =>
			{
				var resource = (ConstantBufferResource<T>)r;
				resource.SetActivatedExternally(firstIndex + i);
				return resource.Buffer;
			}).ToArray());
		}
	}

}
