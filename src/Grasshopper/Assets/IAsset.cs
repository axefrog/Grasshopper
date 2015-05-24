using System;
using System.Collections.Generic;

namespace Grasshopper.Assets
{
	public interface IAsset
	{
		string Id { get; }
		void SetId(string id);
	}

	public interface IHierarchicalAsset : IAsset
	{
		IHierarchicalAsset Parent { get; }
		IEnumerable<IHierarchicalAsset> Children { get; }
	}

	public abstract class Asset : IAsset
	{
		public string Id { get; private set; }
		void IAsset.SetId(string id)
		{
			if(Id != null)
				throw new InvalidOperationException("Id is immutable and cannot be changed");
			Id = id;
		}
	}

	public abstract class HierarchicalAsset : Asset, IHierarchicalAsset
	{
		public IHierarchicalAsset Parent { get; private set; }
		public IEnumerable<IHierarchicalAsset> Children { get; private set; }
	}
}