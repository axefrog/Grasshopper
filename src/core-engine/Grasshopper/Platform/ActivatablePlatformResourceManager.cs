using System.Collections.Generic;
using System.Linq;

namespace Grasshopper.Platform
{
	public abstract class ActivatablePlatformResourceManager<T> : PlatformResourceManager<T>, IActivatablePlatformResourceManager<T>
		where T : IActivatablePlatformResource
	{
		protected ActivatablePlatformResourceManager()
		{
			ResourceAdded += resource =>
			{
				resource.Activated += OnResourceActivated;
			};
			ResourceRemoved += resource =>
			{
				resource.Activated -= OnResourceActivated;
			};
		}

		private void OnResourceActivated(IPlatformResource resource)
		{
			var handler = ResourceActivated;
			if(handler != null)
				handler((T)resource);
		}

		public event ActivatablePlatformResourceEventHandler<T> ResourceActivated;

		public T Activate(string id)
		{
			var resource = this[id];
			resource.Activate();
			return resource;
		}

		public void Activate(params string[] ids)
		{
			Activate((IEnumerable<string>)ids);
		}

		public void Activate(IEnumerable<string> ids)
		{
			Activate(ids.Select(id => this[id]));
		}

		protected virtual void Activate(IEnumerable<T> resources)
		{
			foreach(var resource in resources)
				resource.Activate();
		}
	}
}