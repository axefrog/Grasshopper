using System;
using Grasshopper.Graphics.Rendering.Buffers;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering.Buffers
{
    class VertexBufferResource<T> : IndexActivatableD3DResource, IVertexBufferResource<T>
        where T : struct
    {
        private static readonly int _sizeofVertex = Utilities.SizeOf<T>();
        private int _totalVerticesInBuffer;

        public VertexBufferResource(DeviceManager deviceManager, string id) : base(deviceManager, id)
        {
        }

        public Buffer Buffer { get; private set; }
        public VertexBufferBinding Binding { get; private set; }

        protected override void InitializeInternal()
        {
        }

        protected override void UninitializeInternal()
        {
            if(Buffer != null)
            {
                Buffer.Dispose();
                Buffer = null;
            }
            Binding = default(VertexBufferBinding);
        }

        protected override void ActivateAtIndex(int index)
        {
            DeviceManager.Context.InputAssembler.SetVertexBuffers(index, Binding);
        }

        public IVertexBufferDataWriter<T> BeginWrite(int totalItemsInBuffer)
        {
            if(_totalVerticesInBuffer != totalItemsInBuffer)
            {
                _totalVerticesInBuffer = totalItemsInBuffer;
                if(Buffer != null)
                {
                    Buffer.Dispose();
                    Buffer = null;
                }
            }

            if(Buffer == null)
            {
                Buffer = new Buffer(DeviceManager.Device, _sizeofVertex * _totalVerticesInBuffer, ResourceUsage.Dynamic,
                    BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, _sizeofVertex);
            }

            var writer = new VertexBufferDataWriter(Id, DeviceManager, Buffer);
            writer.Disposed += dataWriter =>
            {
                Buffer = dataWriter.Buffer;
                Binding = new VertexBufferBinding(Buffer, _sizeofVertex, 0);
            };
            return writer;
        }

        class VertexBufferDataWriter : IVertexBufferDataWriter<T>
        {
            private readonly DeviceManager _deviceManager;
            private readonly Buffer _buffer;
            private DataStream _stream;

            public VertexBufferDataWriter(string id, DeviceManager deviceManager, Buffer buffer)
            {
                _deviceManager = deviceManager;
                _buffer = buffer;
                deviceManager.Context.MapSubresource(_buffer, MapMode.WriteDiscard, MapFlags.None, out _stream);
                Id = id;
            }

            public string Id { get; private set; }
            public int TotalVertices { get; private set; }

            public event Action<VertexBufferDataWriter> Disposed;

            public void Write(ref T data)
            {
                _stream.Write(data);
            }

            public void Write(T[] data)
            {
                _stream.WriteRange(data);
            }

            public long Position
            {
                get { return _stream.Position; }
                set { _stream.Position = value; }
            }

            public long Length
            {
                get { return Buffer.Description.SizeInBytes; }
            }

            public Buffer Buffer
            {
                get { return _buffer; }
            }

            public void Dispose()
            {
                _stream.Position = 0;
                _deviceManager.Context.UnmapSubresource(_buffer, 0);
                _stream.Dispose();
                _stream = null;

                Disposed(this);
            }
        }
    }
}