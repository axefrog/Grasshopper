using System;
using System.Collections;
using System.Collections.Generic;
using Grasshopper.Graphics;
using Grasshopper.Math;

namespace Grasshopper.StateManagement
{
	public class InstanceOfModel : IRenderable
	{
		public InstanceOfModel(Model model, Transformation transform)
		{
			MeshTransformOverrides = new Dictionary<string, Transformation>();
		}

		public Transformation ModelTransform { get; set; }
		public Dictionary<string, Transformation> MeshTransformOverrides { get; private set; }

		public IEnumerator<MeshInstance> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
