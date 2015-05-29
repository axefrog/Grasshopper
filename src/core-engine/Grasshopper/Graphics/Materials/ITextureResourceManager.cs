using System;
using System.Collections.Generic;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
	public interface ITextureResourceManager : IIndexActivatablePlatformResourceManager<ITextureResource>
	{
		ITextureResource Create(string id, string path);
		ITextureResource Create(string id, IFileSource fileSource);
	}
}