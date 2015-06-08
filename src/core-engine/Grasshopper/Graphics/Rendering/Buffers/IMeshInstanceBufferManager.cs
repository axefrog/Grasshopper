using System.Collections.Generic;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
// ReSharper disable once TypeParameterCanBeVariant

	public interface IMeshInstanceBufferManager<T> : IActivatablePlatformResourceManager<IMeshInstanceCollection<T>>
		where T : struct
	{
		IMeshInstanceCollection<T> Create(string id, List<T> instances);
		void SetData(string id, List<T> instances);
	}
}