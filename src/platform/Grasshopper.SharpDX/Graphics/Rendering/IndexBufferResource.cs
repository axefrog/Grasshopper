using System;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Platform;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D11.Buffer;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class IndexBufferManagerFactory : IIndexBufferManagerFactory
	{
		private readonly DeviceManager _deviceManager;

		public IndexBufferManagerFactory(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public IIndexBufferManager Create()
		{
			return new IndexBufferManager(_deviceManager);
		}
	}

	class IndexBufferManager : ActivatablePlatformResourceManager<IIndexBufferResource>, IIndexBufferManager
	{
		private readonly DeviceManager _deviceManager;

		public IndexBufferManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override IIndexBufferResource CreateResource(string id)
		{
			return new IndexBufferResource(_deviceManager, id);
		}

		public IIndexBufferResource Create(string id)
		{
			return CreateAndAdd(id, resource => resource.Initialize());
		}
	}

	class IndexBufferResource : ActivatableD3DResource, IIndexBufferResource
	{
		private static readonly int _sizeofIndex = Utilities.SizeOf<int>();
		private int _totalIndicesInBuffer;

		public IndexBufferResource(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
		}

		public Buffer Buffer { get; private set; }

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
		}

		public IIndexBufferDataWriter BeginWrite(int totalIndicesInBuffer)
		{
			if(_totalIndicesInBuffer != totalIndicesInBuffer)
			{
				_totalIndicesInBuffer = totalIndicesInBuffer;
				if(Buffer != null)
				{
					Buffer.Dispose();
					Buffer = null;
				}
			}

			if(Buffer == null)
			{
				Buffer = new Buffer(DeviceManager.Device, _sizeofIndex * _totalIndicesInBuffer, ResourceUsage.Dynamic,
					BindFlags.IndexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, _sizeofIndex);
			}

			var writer = new IndexBufferDataWriter(Id, DeviceManager, Buffer);
			writer.Disposed += dataWriter => Buffer = dataWriter.Buffer;
			return writer;
		}

		protected override void SetActive()
		{
			DeviceManager.Context.InputAssembler.SetIndexBuffer(Buffer, Format.R32_UInt, 0);
		}

		class IndexBufferDataWriter : IIndexBufferDataWriter
		{
			private readonly DeviceManager _deviceManager;
			private readonly Buffer _buffer;
			private DataStream _stream;

			public IndexBufferDataWriter(string id, DeviceManager deviceManager, Buffer buffer)
			{
				_deviceManager = deviceManager;
				_buffer = buffer;
				deviceManager.Context.MapSubresource(_buffer, MapMode.WriteDiscard, MapFlags.None, out _stream);
				Id = id;
			}

			public string Id { get; private set; }
			public int TotalVertices { get; private set; }

			public event Action<IndexBufferDataWriter> Disposed;

			public void Write(uint data)
			{
				_stream.Write(data);
			}

			public void Write(uint[] data)
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