using System;

namespace Grasshopper.Graphics
{
	public interface ITexture : IDisposable
	{
		string Path { get; }
	}
}