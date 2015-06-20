using System.Collections.Generic;
using System.Linq;

namespace Grasshopper.Platform
{
	public abstract class IndexActivatablePlatformResourceManager<T> : PlatformResourceManager<T>, IIndexActivatablePlatformResourceManager<T>
		where T : IIndexActivatablePlatformResource
	{
		public event AssignablePlatformResourceEventHandler<T> ResourceAssigned;

		protected IndexActivatablePlatformResourceManager()
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

		private void OnResourceActivated(IPlatformResource resource, int index)
		{
			var handler = ResourceAssigned;
			if(handler != null)
				handler((T)resource, index);
		}

		public void Activate(int index, string id)
		{
			var resource = this[id];
			resource.Activate(index);
		}

		public void Activate(int firstIndex, params string[] ids)
		{
			Activate(firstIndex, (IEnumerable<string>)ids);
		}

		public void Activate(int firstIndex, IEnumerable<string> ids)
		{
			Activate(firstIndex, ids.Select(id => this[id]));
		}

		protected virtual void Activate(int firstIndex, IEnumerable<T> resources)
		{
			var index = firstIndex;
			foreach(var resource in resources)
				resource.Activate(index++);
		}
	}
}