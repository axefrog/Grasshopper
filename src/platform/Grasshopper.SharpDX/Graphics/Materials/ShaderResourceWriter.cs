using System.IO;
using Grasshopper.Graphics.Materials;
using SharpDX;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    internal class ShaderResourceWriter<T> : IShaderResourceWriter
        where T : Resource
    {
        private readonly DeviceManager _deviceManager;
        private readonly T _resource;
        private readonly DataStream _stream;
        private DataBox _dataBox;

        public ShaderResourceWriter(DeviceManager deviceManager, T resource)
        {
            _deviceManager = deviceManager;
            _resource = resource;
            _dataBox = deviceManager.Context.MapSubresource(resource, 0, MapMode.WriteDiscard, MapFlags.None, out _stream);
        }

        public void Write<T>(T value) where T : struct
        {
            _stream.Write(value);
        }

        public void WriteRange<T>(T[] values) where T : struct
        {
            _stream.WriteRange(values);
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            _stream.Seek(offset, origin);
        }

        public void Dispose()
        {
            _deviceManager.Context.UnmapSubresource(_resource, 0);
            _stream.Dispose();
        }
    }
}