using System;
using System.Collections.Generic;

namespace Grasshopper.Graphics.Rendering
{
// ReSharper disable once TypeParameterCanBeVariant
	public interface IMeshInstanceBufferManager<T> : IDisposable
		 where T : struct
	{
		void Add(string id, T[] instances);
		void Remove(string id);
		void SetActive(string id);
	}
}