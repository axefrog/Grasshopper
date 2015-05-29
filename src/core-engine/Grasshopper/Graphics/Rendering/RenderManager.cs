using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Materials;

namespace Grasshopper.Graphics.Rendering
{
	public abstract class RenderManager
	{
		private readonly IGraphicsContext _graphics;

		protected RenderManager(IGraphicsContext graphics)
		{
			_graphics = graphics;
			// todo: subscribe to changes to asset libraries and build a dirty list for each library which will be processed on the next call to UpdateAssets. also observe changes to group dormancy.
			// 1. if a mesh has been removed, check the asset group and if it's empty, destroy the buffer. ignore removed meshes; we'll leave them in place for performance reasons.
			// 2. if a mesh has been added or updated, rebuild the buffer for now. later we may be able to optimise to do in-place buffer updates if it's an issue, but it probably won't be.
		}

		/// <summary>
		/// Update buffer/shader data (and associated quick-access index for internal use) in GPU memory from asset libraries.
		/// </summary>
		public void UpdateAssets()
		{
			// todo: process the dirty list and update indexes of asset resources in GPU memory, ready for rendering, then clear the list.
		}

		private RenderQueue ConstructRenderQueue(IEnumerable<IRenderable> items)
		{
			throw new NotImplementedException();
		}

		private void Render(RenderQueue queue)
		{
		}

		public void Render()
		{
		}

		public void Render(IEnumerable<IRenderable> items)
		{
			var queue = ConstructRenderQueue(items);
			Render(queue);
			// todo: implement render queue sorting. persist render queue between frames and only update it when the set of renderable items changes.
			// todo: construct/update instance buffers when instance data is out of date
		}

		// todo: the render queue will end up with a structure that defines what to draw, how many instances of it to draw, and so forth, and will pass that information the following method for final instanced rendering
		// protected abstract void Render(...)
	}

	// Switching shaders appears to be more costly than switching buffers, so we'll optimise on that basis.
	internal class RenderQueue : IDisposable
	{
		// 1. sort by material
		// 2. sort by vertex/index buffer
		// 3. the set of instances
		//Dictionary<string, MaterialMetadata> 

		internal class MaterialMetadata
		{

		}

		internal class BufferMetadata
		{
		}

		public void Dispose()
		{
		}
	}
}