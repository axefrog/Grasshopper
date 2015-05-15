using System;

namespace Grasshopper.Graphics.Materials
{
	public interface ITexture : IDisposable
	{
		string Path { get; }
	}
}