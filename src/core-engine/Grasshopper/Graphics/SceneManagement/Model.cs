using System;
using System.Collections;
using System.Collections.Generic;
using Grasshopper.Math;

namespace Grasshopper.Graphics.Geometry
{
	public class Model : IEnumerable<ModelMeshInstance>
	{
		public Model()
		{
			MeshInstances = new Dictionary<Guid, ModelMeshInstance>();
		}

		public Dictionary<Guid, ModelMeshInstance> MeshInstances { get; set; }

		public ModelMeshInstance WithMesh(string id)
		{
			var mesh = new ModelMeshInstance(this, id);
			MeshInstances.Add(mesh.InstanceId, mesh);
			return mesh;
		}

		public IEnumerator<ModelMeshInstance> GetEnumerator()
		{
			return MeshInstances.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class ModelMeshInstance : MeshInstance
	{
		internal ModelMeshInstance(Model ownerModel, string meshId)
		{
			InstanceId = Guid.NewGuid();
			OwnerModel = ownerModel;
			MeshId = meshId;
			Transformation = new Transformation();
		}

		internal Model OwnerModel { get; set; }

		public ModelMeshInstance WithMaterial(string materialId)
		{
			MaterialId = materialId;
			return this;
		}

		public Model And()
		{
			return OwnerModel;
		}
	}
}
