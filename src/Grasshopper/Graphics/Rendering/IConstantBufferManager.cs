using System;

namespace Grasshopper.Graphics.Rendering
{
// ReSharper disable once TypeParameterCanBeVariant
	public interface IConstantBufferManager<T> : IDisposable
		where T : struct
	{
		void Add(string id, T data);
		void Update(string id, T data);
		void Remove(string id);
		void SetActive(string id, int slot = 0);
		void SetActive(params string[] id);
	}
}