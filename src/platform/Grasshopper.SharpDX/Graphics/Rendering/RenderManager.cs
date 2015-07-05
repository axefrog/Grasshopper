using System;
using Grasshopper.SharpDX.Graphics.Materials;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class RenderManager : Grasshopper.Graphics.Rendering.RenderManager
    {
        private readonly GraphicsContext _graphics;
        private readonly MaterialManager _materials;

        public RenderManager(GraphicsContext graphics) : base(graphics)
        {
            if(graphics == null) throw new ArgumentNullException("deviceManager");
            _graphics = graphics;
            _materials = new MaterialManager(graphics);
        }
    }
}
