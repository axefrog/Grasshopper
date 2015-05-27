using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Grasshopper.Assets
{
	// todo: it should be possible to mark asset groups as dormant in order to let the rendering layer know it is safe to unload them for a while
	// todo: when an entity is created that uses assets in a dormant group, the group should be automatically reactivated

	public class AssetGroup<T> : IDisposable, IEnumerable<T> where T : class, IAsset
	{
		private readonly Dictionary<string, T> _assets = new Dictionary<string, T>();
		private readonly Dictionary<string, AssetGroup<T>> _groups = new Dictionary<string, AssetGroup<T>>();

		public AssetGroup(string id)
		{
			Id = id;
		}

		public event AssetChangeEventHandler<T> AssetChange;
		protected string Id { get; private set; }

		protected string[] SplitId(string id)
		{
			return id.Split('/').Select(s => s.Trim()).ToArray();
		}

		protected void Add(string[] ids, T asset)
		{
			if(AssetExists(ids[0]))
				throw new ArgumentException("Cannot use id '" + ids[0] + "' in this context as it is in use by an existing asset. Delete the existing asset or choose a different id.");
			if(ids.Length == 1 && SubgroupExists(ids[0]))
				throw new ArgumentException("Cannot use id '" + ids[0] + "' in this context as it is in use by an existing asset subgroup. Delete the existing subgroup or choose a different id.");

			if(ids.Length > 1)
			{
				AssetGroup<T> subgroup;
				if(!_groups.TryGetValue(ids[0], out subgroup))
				{
					subgroup = new AssetGroup<T>(ids[0]);
					_groups.Add(ids[0], subgroup);
				}
				subgroup.Add(ids.Skip(1).ToArray(), asset);
			}
			else
			{
				if(asset == null)
				{
					var newGroup = new AssetGroup<T>(ids[0]);
					_groups.Add(ids[0], newGroup);
					newGroup.AssetChange += arg =>
					{
						var change = new AssetChangeEvent<T>(newGroup.Id + "/" + arg.AssetId, arg.Asset, arg.ChangeType);
						PostAssetChange(change);
					};
				}
				else
				{
					_assets.Add(ids[0], asset);
					var change = new AssetChangeEvent<T>(asset.Id, asset, AssetChangeType.Added);
					PostAssetChange(change);
				}
			}
		}

		protected void PostAssetChange(AssetChangeEvent<T> change)
		{
			var handler = AssetChange;
			if(handler == null) return;
			handler(change);
		}

		protected bool AssetExists(string[] ids)
		{
			if(ids.Length > 1)
				return SubgroupExists(ids.Skip(1).ToArray());
			return _assets.ContainsKey(ids[0]);
		}

		public bool AssetExists(string id)
		{
			return AssetExists(SplitId(id));
		}

		protected bool SubgroupExists(string[] ids)
		{
			if(ids.Length > 1)
				return SubgroupExists(ids.Skip(1).ToArray());
			return _groups.ContainsKey(ids[0]);
		}

		public bool SubgroupExists(string id)
		{
			return SubgroupExists(SplitId(id));
		}

		public void Add(string id, T asset)
		{
			if(asset == null)
				throw new ArgumentNullException("asset");

			var ids = SplitId(id);
			if(asset.Id == null)
				asset.SetId(ids.Last());
			Add(ids, asset);
		}

		public void AddGroup(string id)
		{
			Add(SplitId(id), null);
		}

		protected T this[string[] ids]
		{
			get
			{
				if(ids.Length > 1)
				{
					AssetGroup<T> subgroup;
					return _groups.TryGetValue(ids[0], out subgroup) ? subgroup[ids.Skip(1).ToArray()] : null;
				}
				T asset;
				return _assets.TryGetValue(ids[0], out asset) ? asset : null;
			}
		}

		public T this[string id]
		{
			get
			{
				return this[SplitId(id)];
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _assets.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		protected event Action Disposing;
		protected bool IsDisposed { get; private set; }

		public void Dispose()
		{
			var handler = Disposing;
			if(handler != null)
				handler();

			foreach(var asset in _assets.Values)
			{
				var disposable = asset as IDisposable;
				if(disposable != null)
					disposable.Dispose();
			}
			_assets.Clear();

			foreach(var group in _groups.Values)
				group.Dispose();
			_groups.Clear();

			IsDisposed = true;
		}
	}

	public abstract class AssetLibrary<T> : AssetGroup<T> where T : class, IAsset, new()
	{
		protected AssetLibrary() : base(null)
		{
		}

		public T Add(string id)
		{
			var ids = SplitId(id);
			var asset = CreateAssetFromId(ids.Last());
			Add(asset.Id, asset);
			return asset;
		}

		protected T CreateAssetFromId(string id)
		{
			var asset = new T();
			asset.SetId(id);
			return asset;
		}
	}

	public delegate void AssetChangeEventHandler<T>(AssetChangeEvent<T> change) where T : class, IAsset;

	public class AssetChangeEvent<T>
	{
		public AssetChangeEvent(string assetId, T asset, AssetChangeType changeType)
		{
			AssetId = assetId;
			Asset = asset;
			ChangeType = changeType;
		}

		public string AssetId { get; private set; }
		public T Asset { get; private set; }
		public AssetChangeType ChangeType { get; private set; }
	}

	public enum AssetChangeType
	{
		Added,
		Removed,
		Updated
	}

	public enum AssetGroupChange
	{
		Added,
		Removed,
		Activated,
		Deactivated
	}
}