using System;
using System.Collections;
using System.Collections.Generic;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Geometry
{
	public interface IMeshGroup : IActivatablePlatformResource, IEnumerable<Mesh>
	{
		int Count { get; }
		void Add(Mesh mesh);
		void AddRange(IEnumerable<Mesh> meshes);
		void Remove(Mesh mesh);
		void Draw(string id);
		void DrawInstanced(string id, int count);
	}
}