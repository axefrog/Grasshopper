namespace Grasshopper.Graphics.Rendering.Rasterization
{
	public class RasterizerSettings : IRasterizerSettings
	{
		private WindingOrder _windingOrder;
		private bool _enableDepthTest;
		private bool _renderWireframe;
		private Antialiasing _antialiasing;
		private TriangleCulling _triangleCulling;

		public bool IsDirty { get; set; }

		public WindingOrder WindingOrder
		{
			get { return _windingOrder; }
			set
			{
				if(value != _windingOrder)
					IsDirty = true;
				_windingOrder = value;
			}
		}

		public bool RenderWireframe
		{
			get { return _renderWireframe; }
			set
			{
				if(value != _renderWireframe)
					IsDirty = true;
				_renderWireframe = value;
			}
		}

		public bool EnableDepthTest
		{
			get { return _enableDepthTest; }
			set
			{
				if(value != _enableDepthTest)
					IsDirty = true;
				_enableDepthTest = value;
			}
		}

		public TriangleCulling TriangleCulling
		{
			get { return _triangleCulling; }
			set
			{
				if(value != _triangleCulling)
					IsDirty = true;
				_triangleCulling = value;
			}
		}

		public Antialiasing Antialiasing
		{
			get { return _antialiasing; }
			set
			{
				if(value != _antialiasing)
					IsDirty = true;
				_antialiasing = value;
			}
		}

		public RasterizerSettings Clone()
		{
			return new RasterizerSettings
			{
				Antialiasing = Antialiasing,
				EnableDepthTest = EnableDepthTest,
				RenderWireframe = RenderWireframe,
				TriangleCulling = TriangleCulling,
				WindingOrder = WindingOrder
			};
		}

		public static RasterizerSettings Default()
		{
			return new RasterizerSettings
			{
				WindingOrder = WindingOrder.Clockwise,
				Antialiasing = Antialiasing.Multisample,
				RenderWireframe = false,
				TriangleCulling = TriangleCulling.DrawFrontFacing,
				IsDirty = true
			};
		}

		protected bool Equals(RasterizerSettings other)
		{
			return _windingOrder == other._windingOrder && _enableDepthTest.Equals(other._enableDepthTest) && _renderWireframe.Equals(other._renderWireframe) && _antialiasing == other._antialiasing && _triangleCulling == other._triangleCulling;
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(obj.GetType() != GetType()) return false;
			return Equals((RasterizerSettings)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				// warnings regarding the hash code changing can be ignored because if any value changes, we *want* this class instance to be treated as different, so that it can be used as a key
// ReSharper disable NonReadonlyFieldInGetHashCode
				var hashCode = (int)_windingOrder;
				hashCode = (hashCode * 397) ^ _enableDepthTest.GetHashCode();
				hashCode = (hashCode * 397) ^ _renderWireframe.GetHashCode();
				hashCode = (hashCode * 397) ^ (int)_antialiasing;
				hashCode = (hashCode * 397) ^ (int)_triangleCulling;
// ReSharper restore NonReadonlyFieldInGetHashCode
				return hashCode;
			}
		}
	}
}
