using System;
using Grasshopper.Graphics.Rendering.Rasterization;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class RasterizerStateManager : ActivatablePlatformResourceManager<IRasterizerState>, IRasterizerStateManager
    {
        private readonly DeviceManager _deviceManager;

        public RasterizerStateManager(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IRasterizerState Create(string id, IRasterizerSettings settings)
        {
            return Add(new RasterizerState(_deviceManager, id, settings));
        }
    }
}