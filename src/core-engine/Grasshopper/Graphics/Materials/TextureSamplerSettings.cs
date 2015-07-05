namespace Grasshopper.Graphics.Materials
{
    public class TextureSamplerSettings
    {
        private TextureSamplerSettings()
        {
        }

        public TextureSamplerSettings(TextureWrapping wrapU, TextureWrapping wrapV, TextureWrapping wrapW, TextureFiltering filter)
        {
            WrapU = wrapU;
            WrapV = wrapV;
            WrapW = wrapW;
            Filter = filter;
        }

        public TextureWrapping WrapU { get; private set; }
        public TextureWrapping WrapV { get; private set; }
        public TextureWrapping WrapW { get; private set; }
        public TextureFiltering Filter { get; private set; }

        public static TextureSamplerSettings Default()
        {
            return new TextureSamplerSettings
            {
                WrapU = TextureWrapping.Clamp,
                WrapV = TextureWrapping.Clamp,
                WrapW = TextureWrapping.Clamp,
                Filter = TextureFiltering.MinMagMipLinear
            };
        }

        protected bool Equals(TextureSamplerSettings other)
        {
            return WrapU == other.WrapU && WrapV == other.WrapV && WrapW == other.WrapW && Filter == other.Filter;
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != this.GetType()) return false;
            return Equals((TextureSamplerSettings)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int)WrapU;
                hashCode = (hashCode * 397) ^ (int)WrapV;
                hashCode = (hashCode * 397) ^ (int)WrapW;
                hashCode = (hashCode * 397) ^ (int)Filter;
                return hashCode;
            }
        }
    }
}