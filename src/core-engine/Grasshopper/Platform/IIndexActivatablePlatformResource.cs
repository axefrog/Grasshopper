namespace Grasshopper.Platform
{
	/// <summary>
	/// Represents an <see cref="IPlatformResource"/> that is registered as active via a hardware location index.
	/// Only one resource of a given type can be active in the hardware slot of that type at a time. Multiple
	/// resources of a given type can be simultaneously active, with one active resource per slot.
	/// </summary>
	public interface IIndexActivatablePlatformResource : IPlatformResource
	{
		event IndexActivatablePlatformResourceEventHandler Activated;
		void Activate(int index);
	}

	public delegate void IndexActivatablePlatformResourceEventHandler(IPlatformResource resource, int index);
}