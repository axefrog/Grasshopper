using System.Numerics;

namespace Grasshopper.Graphics.SceneManagement
{
	public class Camera
	{
		private Vector3 _position;
		private float _fieldOfView;
		private float _nearPlane;
		private float _farPlane;
		private float _aspectRatio;
		private Quaternion _orientation;

		private Matrix4x4? _world;
		private Matrix4x4? _view;
		private Matrix4x4? _proj;
		private Matrix4x4? _viewProj;
		private Matrix4x4? _worldViewProj;

		public Camera(Vector3 position, float fieldOfView, float nearPlane, float farPlane)
		{
			_position = position;
			_fieldOfView = fieldOfView;
			_nearPlane = nearPlane;
			_farPlane = farPlane;
			_orientation = Quaternion.Identity;
		}

		public Camera(float x, float y, float z, float fieldOfView = 0.65f, float nearPlane = 0.01f, float farPlane = 1000f)
			: this(new Vector3(x, y, z), fieldOfView, nearPlane, farPlane)
		{
		}

		public Vector3 Position
		{
			get { return _position; }
			set { _position = value; _world = null; _worldViewProj = null; }
		}

		public float FieldOfView
		{
			get { return _fieldOfView; }
			set { _fieldOfView = value; _proj = null; _viewProj = null; _worldViewProj = null; }
		}

		public float NearPlane
		{
			get { return _nearPlane; }
			set { _nearPlane = value; _proj = null; _viewProj = null; _worldViewProj = null; }
		}

		public float FarPlane
		{
			get { return _farPlane; }
			set { _farPlane = value; _proj = null; _viewProj = null; _worldViewProj = null; }
		}

		public float AspectRatio
		{
			get { return _aspectRatio; }
			set { _aspectRatio = value; _proj = null; _viewProj = null; _worldViewProj = null; }
		}

		public Quaternion Orientation
		{
			get { return _orientation; }
			set { _orientation = value; _view = null; _viewProj = null; _worldViewProj = null; }
		}

		public Camera Move(float x, float y, float z)
		{
			Position = new Vector3(Position.X + x, Position.Y + y, Position.Z + z);
			return this;
		}

		public Camera Pitch(float delta)
		{
			Orientation = Quaternion.CreateFromYawPitchRoll(0, delta, 0) * Orientation;
			return this;
		}

		public Camera Roll(float delta)
		{
			Orientation = Quaternion.CreateFromYawPitchRoll(0, 0, delta) * Orientation;
			return this;
		}

		public Camera Yaw(float delta)
		{
			Orientation = Quaternion.CreateFromYawPitchRoll(delta, 0, 0) * Orientation;
			return this;
		}

		public Matrix4x4 ViewMatrix
		{
			get
			{
				if(!_view.HasValue)
					_view = Matrix4x4.CreateFromQuaternion(-Orientation);
				return _view.Value;
			}
		}

		public Matrix4x4 ProjectionMatrix
		{
			get
			{
				if(!_proj.HasValue)
					_proj = Matrix4x4.CreatePerspectiveFieldOfView(_fieldOfView, _aspectRatio, _nearPlane, _farPlane);
				return _proj.Value;
			}
		}

		public Matrix4x4 WorldMatrix
		{
			get
			{
				if(!_world.HasValue)
					_world = Matrix4x4.CreateTranslation(-_position);
				return _world.Value;
			}
		}

		public Matrix4x4 ViewProjectionMatrix
		{
			get
			{
				if(!_viewProj.HasValue)
					_viewProj = ViewMatrix * ProjectionMatrix;
				return _viewProj.Value;
			}
		}

		public Matrix4x4 WorldViewProjectionMatrix
		{
			get
			{
				if(!_worldViewProj.HasValue)
					_worldViewProj = WorldMatrix * WorldViewProjectionMatrix;
				return _worldViewProj.Value;
			}
		}
	}
}
