using System.Collections;
using System.Collections.Generic;

namespace Grasshopper.Assets
{
	public abstract class AssetLibrary<T> : IEnumerable<T> where T : class
	{
		private readonly Dictionary<string, T> _assets = new Dictionary<string, T>();

		public void Add(string id, T asset)
		{
			_assets.Add(id, asset);
		}

		public T this[string id]
		{
			get { T asset; return _assets.TryGetValue(id, out asset) ? asset : null; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _assets.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}