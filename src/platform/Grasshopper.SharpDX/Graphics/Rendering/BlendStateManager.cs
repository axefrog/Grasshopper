using Grasshopper.Graphics.Rendering.Blending;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class BlendStateManager : ActivatablePlatformResourceManager<IBlendState>, IBlendStateManager
	{
		private readonly DeviceManager _deviceManager;

		public BlendStateManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override IBlendState CreateResource(string id)
		{
			return new BlendState(_deviceManager, id);
		}

		public IBlendState Create(string id, IBlendSettings settings)
		{
			return CreateAndAdd(id, resource =>
			{
				var state = (BlendState)resource;
				state.Settings = settings;
				state.Initialize();
			});
		}
	}
}
