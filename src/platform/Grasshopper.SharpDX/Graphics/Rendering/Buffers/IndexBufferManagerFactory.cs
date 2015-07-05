using System;
using Grasshopper.Graphics.Rendering.Buffers;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    class IndexBufferManagerFactory : IIndexBufferManagerFactory
    {
        private readonly DeviceManager _deviceManager;

        public IndexBufferManagerFactory(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IIndexBufferManager Create()
        {
            return new IndexBufferManager(_deviceManager);
        }
    }
}