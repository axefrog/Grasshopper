using System.Collections.Generic;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
	public interface IMeshGroupBufferManager : IActivatablePlatformResourceManager<IMeshGroup>
	{
		IMeshGroup Create(string id, IEnumerable<Mesh> meshes);
		IMeshGroup Create(string id, params Mesh[] mesh);
	}
}