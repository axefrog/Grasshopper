using System;

namespace Grasshopper.Graphics.Materials
{
    public abstract class ShaderSpec
    {
        protected ShaderSpec(string source)
            : this(Guid.NewGuid().ToString(), source)
        {
        }

        protected ShaderSpec(string id, string source)
        {
            Id = id;
            Source = source;
        }

        public string Id { get; private set; }
        public string Source { get; set; }
    }
}