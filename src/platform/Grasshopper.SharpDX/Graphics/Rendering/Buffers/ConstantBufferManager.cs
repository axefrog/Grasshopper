using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    class ConstantBufferManager<T> : IndexActivatablePlatformResourceManager<IConstantBufferResource<T>>, IConstantBufferManager<T> where T : struct
    {
        private readonly DeviceManager _deviceManager;

        public ConstantBufferManager(DeviceManager deviceManager)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            _deviceManager = deviceManager;
        }

        public IConstantBufferResource<T> Create(string id)
        {
            return Add(new ConstantBufferResource<T>(_deviceManager, id));
        }

        public void Update(string id, T data)
        {
            this[id].Update(ref data);
        }

        public void Update(string id, ref T data)
        {
            this[id].Update(ref data);
        }

        protected override void Activate(int firstIndex, IEnumerable<IConstantBufferResource<T>> resources)
        {
            _deviceManager.Context.VertexShader.SetConstantBuffers(firstIndex, resources.Select((r, i) =>
            {
                var resource = (ConstantBufferResource<T>)r;
                resource.SetActivatedExternally(firstIndex + i);
                return resource.Buffer;
            }).ToArray());
        }
    }

}
