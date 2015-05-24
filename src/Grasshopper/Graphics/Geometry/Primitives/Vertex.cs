using System.Numerics;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex
	{
		public Vector4 Position;
		public TextureCoordinate TextureCoordinate;
		public Color Color;

		public void SetColor(Color color)
		{
			Color = color;
		}

		public static Vertex From(Vector4 pos, TextureCoordinate coord)
		{
			return From(pos, coord, Color.White);
		}

		public static Vertex From(Vector4 pos, TextureCoordinate coord, Color color)
		{
			return new Vertex
			{
				Position = pos,
				TextureCoordinate = coord,
				Color = color
			};
		}

		public static Vertex From(float x, float y, float z, float u, float v)
		{
			return From(new Vector4(x, y, z, 0.0f), TextureCoordinate.From(u, v));
		}

		public bool Equals(Vertex other)
		{
			return Position.Equals(other.Position) && TextureCoordinate.Equals(other.TextureCoordinate);
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			return obj is Vertex && Equals((Vertex)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Position.GetHashCode();
				hashCode = (hashCode*397) ^ TextureCoordinate.GetHashCode();
				return hashCode;
			}
		}
	}
}