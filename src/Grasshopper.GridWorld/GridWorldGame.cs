using System.Collections.Generic;

namespace Grasshopper.GridWorld
{
	public class GridWorldGame
	{
		
	}

	public class TexturePlane
	{
		
	}

	public class GridLocation
	{
		public List<TexturePlane> TexturePlanes { get; set; }
	}

	public class WorldZone
	{
		private readonly GridLocation[, ,] _locations;

		public WorldZone(int width, int height, int depth)
		{
			_locations = new GridLocation[width, height, depth];
		}

		public GridLocation this[int x, int y, int z]
		{
			get { return _locations[x, y, z]; }
		}
	}
}
