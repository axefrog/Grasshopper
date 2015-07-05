using System;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    class VertexBufferManager<T> : IndexActivatablePlatformResourceManager<IVertexBufferResource<T>>, IVertexBufferManager<T>
        where T : struct
    {
        private readonly DeviceManager _deviceManager;

        public VertexBufferManager(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IVertexBufferResource<T> Create(string id)
        {
            return Add(new VertexBufferResource<T>(_deviceManager, id));
        }
    }
}