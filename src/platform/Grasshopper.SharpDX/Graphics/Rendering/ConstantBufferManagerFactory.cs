using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class ConstantBufferManagerFactory : IConstantBufferManagerFactory
	{
		private readonly DeviceManager _deviceManager;

		public ConstantBufferManagerFactory(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public IConstantBufferManager<T> Create<T>() where T : struct
		{
			return new ConstantBufferManager<T>(_deviceManager);
		}
	}
}