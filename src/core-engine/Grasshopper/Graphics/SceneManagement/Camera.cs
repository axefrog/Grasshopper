using System.Numerics;

namespace Grasshopper.Graphics.SceneManagement
{
    public class Camera
    {
        private Vector3 _position;
        private Vector3 _direction;
        private Vector3 _up;
        private float _fieldOfView;
        private float _nearPlane;
        private float _farPlane;
        private float _aspectRatio;

        private Matrix4x4? _world;
        private Matrix4x4? _view;
        private Matrix4x4? _proj;
        private Matrix4x4? _viewProj;
        private Matrix4x4? _worldViewProj;

        public Camera(Vector3 position, Vector3 direction, Vector3 up, float fieldOfView = 0.65f, float nearPlane = 0.01f, float farPlane = 1000f)
        {
            _position = position;
            _direction = direction;
            _up = up;
            _fieldOfView = fieldOfView;
            _nearPlane = nearPlane;
            _farPlane = farPlane;
        }

        public Camera() : this(Vector3.UnitZ, -Vector3.UnitZ, Vector3.UnitY)
        {
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; _world = _view =  _viewProj = _worldViewProj = null; }
        }

        public Vector3 Direction
        {
            get { return _direction; }
            set { _direction = value; _world = _view =  _viewProj = _worldViewProj = null; }
        }

        public Vector3 Up
        {
            get { return _up; }
            set { _up = value; _world = _view =  _viewProj = _worldViewProj = null; }
        }

        public float FieldOfView
        {
            get { return _fieldOfView; }
            set { _fieldOfView = value; _proj = _viewProj = _worldViewProj = null; }
        }

        public float NearPlane
        {
            get { return _nearPlane; }
            set { _nearPlane = value; _proj = _viewProj = _worldViewProj = null; }
        }

        public float FarPlane
        {
            get { return _farPlane; }
            set { _farPlane = value; _proj = _viewProj = _worldViewProj = null; }
        }

        public float AspectRatio
        {
            get { return _aspectRatio; }
            set { _aspectRatio = value; _proj = null; _viewProj = null; _worldViewProj = null; }
        }

        public Camera Move(float x, float y, float z)
        {
            Position = new Vector3(Position.X + x, Position.Y + y, Position.Z + z);
            return this;
        }

        public Camera Move(float amount)
        {
            Position += Direction * amount;
            return this;
        }

        public Camera Pitch(float delta)
        {
            var axis = Vector3.Normalize(Vector3.Cross(_up, _direction));
            var rot = Matrix4x4.CreateFromAxisAngle(axis, delta);
            var dir = Vector4.Transform(_direction, rot);
            var up = Vector4.Transform(_up, rot);
            Direction = new Vector3(dir.X, dir.Y, dir.Z);
            Up = new Vector3(up.X, up.Y, up.Z);
            return this;
        }

        public Camera Roll(float delta)
        {
            var up = Vector4.Transform(_up, Matrix4x4.CreateFromAxisAngle(_direction, delta));
            Up = new Vector3(up.X, up.Y, up.Z);
            return this;
        }

        public Camera Yaw(float delta)
        {
            var dir = Vector4.Transform(_direction, Matrix4x4.CreateFromAxisAngle(_up, delta));
            Direction = new Vector3(dir.X, dir.Y, dir.Z);
            return this;
        }

        public Matrix4x4 ViewMatrix
        {
            get
            {
                if(!_view.HasValue)
                    _view = Matrix4x4.CreateLookAt(_position, _position + _direction, _up);
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
                    _worldViewProj = WorldMatrix * ViewProjectionMatrix;
                return _worldViewProj.Value;
            }
        }
    }
}
