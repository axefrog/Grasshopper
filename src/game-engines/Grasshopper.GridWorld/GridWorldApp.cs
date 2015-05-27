using System.Collections.Generic;
using Grasshopper.Graphics.Geometry.Primitives;

namespace Grasshopper.GridWorld
{
	public class GridWorldApp : GrasshopperApp
	{
		
	}

	public class TexturedPlane
	{
		public Quad Quad { get; set; }
		public string MaterialId { get; set; }

		/// <summary>
		/// Creates a horizontal texture plane exactly one grid unit in width and length
		/// </summary>
		/// <param name="depth">The depth of the plane into the grid space. 1.0 is at the top, 0.0 is at the bottom.</param>
		/// <param name="materialId">The id of the material for this texture plane</param>
		/// <returns>A new TexturePlane object</returns>
		public static TexturedPlane CreateFloor(float depth, string materialId)
		{
			return new TexturedPlane
			{
				Quad = Quad.From(
					Vertex.From(0.0f, depth, 1.0f, 0.0f, 0.0f),
					Vertex.From(1.0f, depth, 1.0f, 1.0f, 0.0f),
					Vertex.From(1.0f, depth, 0.0f, 1.0f, 1.0f),
					Vertex.From(0.0f, depth, 0.0f, 0.0f, 1.0f))
			};
		}
	}

	public class GridLocation
	{
		public List<TexturedPlane> TexturePlanes { get; set; }
	}

	public class WorldZone
	{
		private readonly GridLocation[, ,] _locations;

		public WorldZone(int width, int length, int depth)
		{
			_locations = new GridLocation[width, length, depth];
		}

		public GridLocation this[int x, int y, int z]
		{
			get { return _locations[x, y, z]; }
			set { _locations[x, y, z] = value; }
		}

		public void Fill(GridLocation loc)
		{
			var length = _locations.GetUpperBound(0) + 1;
			var width = _locations.GetUpperBound(1) + 1;
			var depth = _locations.GetUpperBound(2) + 1;

			for(var x = 0; x < width; x++)
				for(var y=0; y<length; y++)
					for(var z = 0; z < depth; z++)
						_locations[x, y, z] = loc;
		}
	}
}
