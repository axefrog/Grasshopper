using System.Collections.Generic;

namespace Grasshopper.Platform
{
	public interface IActivatablePlatformResourceManager<T> : IPlatformResourceManager<T>
		where T : class, IActivatablePlatformResource
	{
		event ActivatablePlatformResourceEventHandler<T> ResourceActivated;
		void Activate(string id);
		void Activate(params string[] ids);
		void Activate(IEnumerable<string> ids);
	}

	public delegate void ActivatablePlatformResourceEventHandler<T>(T resource) where T : IPlatformResource;
}