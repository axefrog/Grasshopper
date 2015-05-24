using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Geometry.Primitives;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshRenderer : Grasshopper.Graphics.MeshRenderer
	{
		private readonly DeviceManager _deviceManager;
		private Buffer _vertexBuffer;
		private VertexBufferBinding _vertexBufferBinding;
		private Buffer _indexBuffer;

		public MeshRenderer(Mesh mesh, DeviceManager deviceManager) : base(mesh)
		{
			_deviceManager = deviceManager;
		}

		protected override void InitializeResources()
		{
			_vertexBuffer = _deviceManager.CreateAndPopulateBuffer(BindFlags.VertexBuffer, Mesh.Vertices);
			_vertexBufferBinding = new VertexBufferBinding(_vertexBuffer, Utilities.SizeOf<Vertex>(), 0);
			_indexBuffer = _deviceManager.CreateAndPopulateBuffer(BindFlags.IndexBuffer, Mesh.Indices);
		}

		protected override void DestroyResources()
		{
			_vertexBufferBinding = default(VertexBufferBinding);

			if(_vertexBuffer != null)
			{
				_vertexBuffer.Dispose();
				_vertexBuffer = null;
			}
			if(_indexBuffer != null)
			{
				_indexBuffer.Dispose();
				_indexBuffer = null;
			}
		}

		protected override void PerformRender()
		{
			var context = _deviceManager.Context;
			context.InputAssembler.SetVertexBuffers(0, _vertexBufferBinding);
			context.InputAssembler.SetIndexBuffer(_indexBuffer, Format.R32_UInt, 0);
		}
	}

	internal class RendererFactory : IRendererFactory
	{
		private readonly DeviceManager _deviceManager;

		public RendererFactory(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public Grasshopper.Graphics.MeshRenderer CreateMeshRenderer(Mesh mesh)
		{
			return new MeshRenderer(mesh, _deviceManager);
		}
	}
}
