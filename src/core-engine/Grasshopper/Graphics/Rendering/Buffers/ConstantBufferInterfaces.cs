using System;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
	public interface IConstantBufferManagerFactory
	{
		IConstantBufferManager<T> Create<T>()
			where T : struct;
	}

	public interface IConstantBufferManager<T> : IIndexActivatablePlatformResourceManager<IConstantBufferResource<T>>
		where T : struct
	{
		IConstantBufferResource<T> Create(string id);
		void Update(string id, T data);
		void Update(string id, ref T data);
	}

	public interface IConstantBufferResource<T> : IIndexActivatablePlatformResource
		where T : struct
	{
		void Update(T data);
		void Update(ref T data);
	}

	public interface IConstantBufferDataWriter<T> : IDisposable
		where T : struct
	{
		string Id { get; }
		void WriteData(ref T data);
	}
}