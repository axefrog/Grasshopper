namespace Grasshopper.Platform
{
	public interface IActivatablePlatformResource : IPlatformResource
	{
		event ActivatablePlatformResourceEventHandler Activated;
		void Activate();
	}

	public delegate void ActivatablePlatformResourceEventHandler(IPlatformResource resource);
}