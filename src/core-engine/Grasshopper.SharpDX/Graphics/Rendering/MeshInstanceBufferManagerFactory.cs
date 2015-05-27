using Grasshopper.Graphics.Rendering;
using SharpDX.D3DCompiler;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshInstanceBufferManagerFactory : IMeshInstanceBufferManagerFactory
	{
		private readonly DeviceManager _deviceManager;

		public MeshInstanceBufferManagerFactory(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public IMeshInstanceBufferManager<T> Create<T>() where T : struct
		{
			return new MeshInstanceBufferManager<T>(_deviceManager);
		}
	}
}
