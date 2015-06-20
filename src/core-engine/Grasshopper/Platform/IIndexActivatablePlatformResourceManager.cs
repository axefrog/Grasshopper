using System.Collections.Generic;

namespace Grasshopper.Platform
{
	public interface IIndexActivatablePlatformResourceManager<T> : IPlatformResourceManager<T>
		where T : IIndexActivatablePlatformResource
	{
		event AssignablePlatformResourceEventHandler<T> ResourceAssigned;
		void Activate(int index, string id);
		void Activate(int firstIndex, params string[] ids);
		void Activate(int firstIndex, IEnumerable<string> ids);
	}

	public delegate void AssignablePlatformResourceEventHandler<T>(T resource, int index) where T : IPlatformResource;
}