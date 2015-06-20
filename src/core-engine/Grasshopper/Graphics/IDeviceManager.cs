using System;

namespace Grasshopper.Graphics
{
	public interface IDeviceManager : IDisposable
	{
		event Action Initialized;
		void Initialize();
	}
}