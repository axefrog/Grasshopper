using Grasshopper.Graphics.Rendering.Rasterization;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class RasterizerStateManager : ActivatablePlatformResourceManager<IRasterizerState>, IRasterizerStateManager
	{
		private readonly DeviceManager _deviceManager;

		public RasterizerStateManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override IRasterizerState CreateResource(string id)
		{
			return new RasterizerState(_deviceManager, id);
		}

		public IRasterizerState Create(string id, IRasterizerSettings settings)
		{
			return CreateAndAdd(id, resource =>
			{
				var state = (RasterizerState)resource;
				state.Settings = settings;
				state.Initialize();
			});
		}
	}
}