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

	/// <summary>
	/// Represents a group of meshes that will be accessed and update in a similar way and which have similar lifespans.
	/// </summary>
	public abstract class MeshGroup : ActivatablePlatformResource, IMeshGroup, IEnumerable<Mesh>
	{
		private readonly List<Mesh> _meshes = new List<Mesh>();

		protected MeshGroup(string id, params Mesh[] meshes) : this(id, (IEnumerable<Mesh>)meshes)
		{
		}

		protected MeshGroup(string id, IEnumerable<Mesh> meshes) : base(id)
		{
			_meshes.AddRange(meshes);
		}

		public int Count { get { return _meshes.Count; } }

		private void AssertIsUninitialized()
		{
			if(IsInitialized)
				throw new InvalidOperationException("This mesh group's resources are currently initialized. Call Uninitialize() first, make changes, then call Initialize() again.");
		}

		public void Add(Mesh mesh)
		{
			AssertIsUninitialized();
			_meshes.Add(mesh);
		}

		public void AddRange(IEnumerable<Mesh> meshes)
		{
			AssertIsUninitialized();
			_meshes.AddRange(meshes);
		}

		public void Remove(Mesh mesh)
		{
			AssertIsUninitialized();
			_meshes.Remove(mesh);
		}

		public abstract void Draw(string id);
		public abstract void DrawInstanced(string id, int count);

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