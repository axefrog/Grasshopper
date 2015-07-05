using System;
using System.Collections.Generic;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
    public abstract class Material : ActivatablePlatformResource
    {
        protected Material(string id) : base(id)
        {
            Textures = new List<string>();
            Samplers = new List<string>();
        }

        public bool IsTranslucent { get; set; }
        public List<string> Textures { get; set; }
        public List<string> Samplers { get; set; }
        public VertexShaderSpec VertexShaderSpec { get; set; }
        public PixelShaderSpec PixelShaderSpec { get; set; }
    }
}