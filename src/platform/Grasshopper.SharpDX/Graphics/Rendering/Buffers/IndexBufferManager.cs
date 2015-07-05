using System;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    class IndexBufferManager : ActivatablePlatformResourceManager<IIndexBufferResource>, IIndexBufferManager
    {
        private readonly DeviceManager _deviceManager;

        public IndexBufferManager(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IIndexBufferResource Create(string id)
        {
            return Add(new IndexBufferResource(_deviceManager, id));
        }
    }
}