using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Primitives;

namespace Grasshopper.Procedural.Graphics.Primitives
{
	public class CubeBuilder : IEnumerable<VertexMetadata>
	{
		private Color[] _colors;
		private float _size = 1.0f;

		public static CubeBuilder New { get { return new CubeBuilder(); } }

		public CubeBuilder Colors(params Color[] colors)
		{
			if(colors.Length == 0)
				colors = null;
			_colors = colors ?? new[] { Color.Red, Color.LimeGreen, Color.Yellow, Color.Orange, Color.Blue, Color.Magenta, Color.White, Color.Cyan };
			return this;
		}

		public CubeBuilder Size(float length = 1.0f)
		{
			_size = length;
			return this;
		}

		public IEnumerator<VertexMetadata> GetEnumerator()
		{
			if(_colors == null) Colors();
			if(_colors.Length != 8)
			{
				var colors = _colors;
				_colors = new Color[8];
				for(var i = 0; i < 8; i++)
					_colors[i] = colors[i % colors.Length];
			}
			Color a = _colors[0], b = _colors[1], c = _colors[2], d = _colors[3],
				e = _colors[4], f = _colors[5], g = _colors[6], h = _colors[7];
			var half = _size / 2;
			return new[]
			{
				QuadBuilder.New.Face(0).XY(-half,  half, -half,  half,  half).Colors(a, b, c, d),
				QuadBuilder.New.Face(1).XY( half, -half, -half,  half, -half).Colors(e, f, g, h),
				QuadBuilder.New.Face(2).YZ(-half,  half, -half,  half, -half).Colors(f, a, d, g),
				QuadBuilder.New.Face(3).YZ(-half,  half,  half, -half,  half).Colors(b, e, h, c),
				QuadBuilder.New.Face(4).XZ(-half,  half,  half, -half,  half).Colors(f, e, b, a),
				QuadBuilder.New.Face(5).XZ(-half,  half, -half,  half, -half).Colors(d, c, h, g),
			}
			.SelectMany(v => v)
			.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}