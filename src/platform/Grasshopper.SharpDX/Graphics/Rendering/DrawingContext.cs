using System;
using Grasshopper.Graphics.Rendering;
using SharpDX.Direct3D;
using Color = Grasshopper.Graphics.Primitives.Color;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public abstract class DrawingContext : IDrawingContext
	{
		protected DeviceManager DeviceManager { get; private set; }

		protected DrawingContext(DeviceManager deviceManager)
		{
			DeviceManager = deviceManager;
		}

		public void Activate()
		{
			MakeTargetsActive();
		}

		public void SetDrawType(DrawType drawType)
		{
			switch(drawType)
			{
				case DrawType.Points:
					DeviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.PointList;
					break;
				case DrawType.LineList:
					DeviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
					break;
				case DrawType.LineStrip:
					DeviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineStrip;
					break;
				case DrawType.Triangles:
					DeviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
					break;
				case DrawType.TriangleStrip:
					DeviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
					break;
				default:
					throw new NotSupportedException("Unexpected draw type: " + drawType);
			}
		}

		public void Draw(int vertexCount, int vertexStartLocation)
		{
			DeviceManager.Context.Draw(vertexCount, vertexStartLocation);
		}

		public void DrawInstanced(int vertexCount, int instanceCount, int vertexStartLocation, int instanceStartLocation)
		{
			DeviceManager.Context.DrawInstanced(vertexCount, instanceCount, vertexStartLocation, instanceStartLocation);
		}

		public void DrawIndexed(int indexCount, int indexStartLocation, int vertexStartLocation)
		{
			DeviceManager.Context.DrawIndexed(indexCount, indexStartLocation, vertexStartLocation);
		}

		public void DrawIndexedInstanced(int indexCountPerInstance, int instanceCount, int indexStartLocation, int vertexStartLocation, int instanceStartLocation)
		{
			DeviceManager.Context.DrawIndexedInstanced(indexCountPerInstance, instanceCount, indexStartLocation, vertexStartLocation, instanceStartLocation);
		}

		protected abstract void MakeTargetsActive();
		public abstract void Clear(Color color);

		protected bool IsDisposed { get; private set; }
		protected abstract void DestroyResources();

		public void Dispose()
		{
			DestroyResources();
			IsDisposed = true;
		}
	}
}