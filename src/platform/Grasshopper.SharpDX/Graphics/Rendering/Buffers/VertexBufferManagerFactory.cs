using System;
using Grasshopper.Graphics.Rendering.Buffers;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    internal class VertexBufferManagerFactory : IVertexBufferManagerFactory
    {
        private readonly DeviceManager _deviceManager;

        public VertexBufferManagerFactory(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IVertexBufferManager<T> Create<T>() where T : struct
        {
            return new VertexBufferManager<T>(_deviceManager);
        }
    }
}