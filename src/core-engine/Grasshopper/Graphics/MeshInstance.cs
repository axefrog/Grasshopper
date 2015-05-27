using System;
using System.Collections.Generic;
using Artemis.Interface;
using Grasshopper.Math;

namespace Grasshopper.Graphics
{
	public class MeshInstance : IComponent
	{
		public MeshInstance()
		{
		}

		public MeshInstance(string meshId, string materialId)
		{
			MeshId = meshId;
			MaterialId = materialId;
		}

		public Guid InstanceId { get; set; }
		public string MeshId { get; set; }
		public string MaterialId { get; set; }
		public Transformation Transformation { get; set; }
	}

	public interface IRenderable : IEnumerable<MeshInstance>
	{
	}
}