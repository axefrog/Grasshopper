using System;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
    public interface IMaterialManager : IPlatformResourceManager<Material>
    {
        Material Create(string id);
    }
}
