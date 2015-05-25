using System;
using System.Collections.Generic;

namespace Grasshopper.Graphics.Rendering
{
	public interface IMeshInstanceBufferManager<T> : IDisposable
	{
		void Add(string id, T[] instances);
		void Remove(string id);
		void SetActive(string id);
	}
}