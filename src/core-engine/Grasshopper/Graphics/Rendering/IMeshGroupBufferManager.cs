using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering
{
	public interface IMeshGroupBufferManager : IActivatablePlatformResourceManager<IMeshGroup>
	{
		IMeshGroup Create(string id, IEnumerable<Mesh> meshes);
		IMeshGroup Create(string id, params Mesh[] mesh);
	}
}