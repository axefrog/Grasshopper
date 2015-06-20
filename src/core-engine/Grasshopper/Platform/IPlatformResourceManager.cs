using System;
using System.Collections.Generic;

namespace Grasshopper.Platform
{
	/// <summary>
	/// Represents a library of IPlatformResources of a given type
	/// </summary>
	public interface IPlatformResourceManager<T> : IEnumerable<T>, IDisposable
		where T : IPlatformResource
	{
		/// <summary>
		/// Removes the resource from control by the resource manager, but does not uninitialize or dispose it.
		/// </summary>
		/// <param name="id">The id of the resource to remove</param>
		/// <returns>The removed resource</returns>
		T Remove(string id);
		/// <summary>
		/// Removes and disposes of the specified resource
		/// </summary>
		/// <param name="id">The id of the resource to remove</param>
		void RemoveAndDispose(string id);
		void Initialize(string id);
		void Uninitialize(string id);
		bool Exists(string id);
		bool IsInitialized(string id);
		T this[string id] { get; }
		event PlatformResourceEventHandler<T> ResourceAdded;
		event PlatformResourceEventHandler<T> ResourceRemoved;
		event PlatformResourceEventHandler<T> ResourceInitialized;
		event PlatformResourceEventHandler<T> ResourceUninitialized;
		event PlatformResourceEventHandler<T> ResourceDisposed;
		event Action Disposing;
		event Action Disposed;
	}

	public delegate void PlatformResourceEventHandler<T>(T resource) where T : IPlatformResource;
}