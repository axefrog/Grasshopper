using System;
using Grasshopper.Graphics.Rendering.Buffers;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    class ConstantBufferManagerFactory : IConstantBufferManagerFactory
    {
        private readonly DeviceManager _deviceManager;

        public ConstantBufferManagerFactory(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IConstantBufferManager<T> Create<T>() where T : struct
        {
            return new ConstantBufferManager<T>(_deviceManager);
        }
    }
}