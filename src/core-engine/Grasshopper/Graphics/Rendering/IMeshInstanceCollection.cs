using System.Collections.Generic;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering
{
	public interface IMeshInstanceCollection<T> : IActivatablePlatformResource where T : struct
	{
		void SetData(List<T> instances);
	}
}