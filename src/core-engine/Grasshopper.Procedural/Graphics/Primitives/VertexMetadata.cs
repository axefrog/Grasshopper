using System.Numerics;
using Grasshopper.Graphics.Primitives;

namespace Grasshopper.Procedural.Graphics.Primitives
{
    public class VertexMetadata
    {
        public VertexMetadata()
        {
        }

        public VertexMetadata(float x, float y, float z, float u, float v, Color color, uint faceIndex, uint triangleIndex)
        {
            Position = new Vector4(x, y, z, 1f);
            TextureCoordinate = new Vector2(u, v);
            Color = color;
            FaceIndex = faceIndex;
            TriangleIndex = triangleIndex;
        }

        public Vector4 Position { get; set; }
        public Vector2 TextureCoordinate { get; set; }
        public Color Color { get; set; }
        public uint FaceIndex { get; set; }
        public uint TriangleIndex { get; set; }

        protected bool Equals(VertexMetadata other)
        {
            return Position.Equals(other.Position) && TextureCoordinate.Equals(other.TextureCoordinate) && Color.Equals(other.Color) && FaceIndex == other.FaceIndex && TriangleIndex == other.TriangleIndex;
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != this.GetType()) return false;
            return Equals((VertexMetadata)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ TextureCoordinate.GetHashCode();
                hashCode = (hashCode * 397) ^ Color.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)FaceIndex;
                hashCode = (hashCode * 397) ^ (int)TriangleIndex;
                return hashCode;
            }
        }
    }
}
