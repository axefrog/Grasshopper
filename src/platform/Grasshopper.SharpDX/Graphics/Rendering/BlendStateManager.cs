using System;
using Grasshopper.Graphics.Rendering.Blending;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class BlendStateManager : ActivatablePlatformResourceManager<IBlendState>, IBlendStateManager
    {
        private readonly DeviceManager _deviceManager;

        public BlendStateManager(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IBlendState Create(string id, IBlendSettings settings)
        {
            return Add(new BlendState(_deviceManager, id, settings));
        }
    }
}
