using System.Collections;
using System.Collections.Generic;

namespace Grasshopper.Graphics.Geometry
{
	/// <summary>
	/// Represents a group of meshes that will be accessed and update in a similar way and which have similar lifespans.
	/// </summary>
	public class MeshGroup : IEnumerable<Mesh>
	{
		private readonly List<Mesh> _meshes = new List<Mesh>();
		public string Id { get; private set; }

		public MeshGroup(string id, params Mesh[] meshes) : this(id, (IEnumerable<Mesh>)meshes)
		{
		}

		public MeshGroup(string id, IEnumerable<Mesh> meshes)
		{
			Id = id;
			_meshes.AddRange(meshes);
		}

		public int Count { get { return _meshes.Count; } }

		public IEnumerator<Mesh> GetEnumerator()
		{
			return _meshes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}