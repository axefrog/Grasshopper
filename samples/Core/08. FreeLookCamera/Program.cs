using System;
using System.Linq;
using System.Numerics;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using FreeLookCamera.Properties;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Input;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using Grasshopper.WindowsFileSystem;

namespace FreeLookCamera
{
	// The code in this sample is primarily the same as the code in the
	// ManyTextureCubes sample (07), but with the addition of input to
	// control the camera position and orientation. Windowed fullscreen
	// mode is also demonstrated. Use the mouse and AWSD keys to fly
	// around, and the ESC key to unattach the mouse from the view, and
	// a second time to exit the program.

	class Program : IDisposable
	{
		static void Main()
		{
			using(var program = new Program())
			{
				program.Run();
			}
		}

		private readonly GrasshopperApp _app;
		private readonly IGraphicsContext _graphics;
		private readonly IWindowRenderTarget _renderTarget;
		private readonly IConstantBufferManager<SceneData> _constantBufferManager;
		private readonly MeshBufferManager<Vertex> _meshBufferManager;
		private readonly IVertexBufferManager<CubeInstance> _instanceBufferManager;

		// Below we add a number of new values for controlling camera
		// position and orientation, and rotation and movement speed

		private readonly Camera _camera = new Camera();
		private Throttle _throttle = Throttle.None;
		private Rotation _rotation = Rotation.None;
		float _prevElapsedSeconds;
		float? _currentTravelSpeed = 0;
		float? _currentRotationSpeed = 0;
		const float ForwardSpeed = 8;
		const float BackwardSpeed = 5;
		const float RotationSpeed = 2;

		public Program()
		{
			_app = new GrasshopperApp()
				.UseSharpDX()
				.UseWindowsFileSystem();

			_graphics = _app.Graphics.CreateContext(enableDebugMode: true);
			_renderTarget = _graphics.RenderTargetFactory.CreateWindow();
			_constantBufferManager = _graphics.ConstantBufferManagerFactory.Create<SceneData>();
			_meshBufferManager = new MeshBufferManager<Vertex>(_graphics);
			_instanceBufferManager = _graphics.VertexBufferManagerFactory.Create<CubeInstance>();

			_renderTarget.Window.Title = "Flying Through Cubes";
			_renderTarget.Window.Visible = true;
			_renderTarget.Window.SetFullScreen();
			_renderTarget.Window.SizeChanged += UpdateAspectRatio;

			UpdateAspectRatio(_renderTarget.Window);
		}

		private void UpdateAspectRatio(IAppWindow window)
		{
			_camera.AspectRatio = (float)window.ClientWidth / window.ClientHeight;
		}

		public void Run()
		{
			ConfigureInput();
			CreateAndActivateDefaultMaterial();

			// Use Grasshopper's procedural tools to create a cube mesh
			var mesh = CubeBuilder.New
				.Size(0.25f)
				.ToMesh("cube", v => new Vertex(v.Position, v.Color, v.TextureCoordinate));

			// A mesh buffer manager allows us to pack multiple meshes
			// into a vertex buffer and accompanying index buffer, and
			// keep track of the offset locations of each mesh in each
			// buffer. In our case we're only storing one mesh.
			var meshBuffer = _meshBufferManager.Create("default", mesh);
			meshBuffer.Activate();
			var cubeData = meshBuffer["cube"];

			// Create a set of cube instance data with different sizes
			// and rotation values for each cube instance
			var rand = new Random();
			const int instanceCount = 100000;
			var instances = Enumerable.Range(0, instanceCount)
				.Select(i =>
				{
					// rotation on each axis
					var rotX = rand.Next(200) - 100.0f;
					var rotY = rand.Next(200) - 100.0f;
					var rotZ = rand.Next(200) - 100.0f;
					// rotation speed
					var rotS = (rand.Next(200)) / 5000.0f;
					// cube offset from the origin
					var posX = (rand.Next(20000) - 10000) / 100.0f;
					var posY = (rand.Next(20000) - 10000) / 100.0f;
					var posZ = (rand.Next(20000) - 10000) / 100.0f;
					// cube size variation
					var scale = (rand.Next(8) == 0 ? (rand.Next(1500) + 100.0f) : rand.Next(200) + 50.0f) / 100.0f;

					return new CubeInstance
					{
						Position = new Vector4(posX, posY, posZ, 0),
						Rotation = new Vector4(rotX, rotY, rotZ, rotS),
						Scale = new Vector4(scale, scale, scale, 1),
						Texture = rand.Next(5)
					};
				});

			// Create an instance buffer and write the instance data to
			// it, then active the buffer in slot 1, so that its values
			// will be included for each vertex in the vertex shader
			var instanceBuffer = _instanceBufferManager.Create("default");
			using(var writer = instanceBuffer.BeginWrite(instanceCount))
				writer.Write(instances.ToArray());
			instanceBuffer.Activate(1);

			// Create our initial view and projection matrices that
			// will represent the camera view, location and orientation
			var view = Matrix4x4.CreateLookAt(new Vector3(0, 1.25f, -75f), new Vector3(0, 0, 0), Vector3.UnitY);

			// Create and activate our constant buffer which will hold
			// the world-view-projection data and time siagnture for
			// use by the vertex shader
			var constantBuffer = _constantBufferManager.Create("cube");
			constantBuffer.Activate(0);

			// Define the data that will go into the constant buffer
			var sceneData = new SceneData { View = Matrix4x4.Transpose(view) };
			constantBuffer.Update(ref sceneData);

			// Let the looping begin!
			_app.Run(_renderTarget, context =>
			{
				MoveCamera();

				sceneData.Projection = Matrix4x4.Transpose(_camera.ProjectionMatrix);
				sceneData.View = Matrix4x4.Transpose(_camera.ViewMatrix);
				sceneData.SecondsElapsed = _app.ElapsedSeconds;
				constantBuffer.Update(ref sceneData);

				context.Clear(Color.CornflowerBlue);
				context.SetDrawType(cubeData.DrawType);
				context.DrawIndexedInstanced(cubeData.IndexCount, instanceCount, cubeData.IndexBufferOffset, cubeData.VertexBufferOffset, 0);
				context.Present();
			});
		}

		private void MoveCamera()
		{
			var deltaSec = _app.ElapsedSeconds - _prevElapsedSeconds;

			switch(_throttle)
			{
				case Throttle.Forward: _currentTravelSpeed = ForwardSpeed; break;
				case Throttle.Backward: _currentTravelSpeed = -BackwardSpeed; break;
				case Throttle.None: _currentTravelSpeed = null; break;
			}

			switch(_rotation)
			{
				case Rotation.Left: _currentRotationSpeed = -RotationSpeed; break;
				case Rotation.Right: _currentRotationSpeed = RotationSpeed; break;
				case Rotation.None: _currentRotationSpeed = null; break;
			}

			_prevElapsedSeconds = _app.ElapsedSeconds;
			if(_currentTravelSpeed.HasValue)
				_camera.Move(deltaSec * _currentTravelSpeed.Value);
			if(_currentRotationSpeed.HasValue)
				_camera.Roll(deltaSec * _currentRotationSpeed.Value);
		}

		private void ConfigureInput()
		{
			var isFocusLocked = true;

			_renderTarget.Window.SetCursorPositionToCenter();
			_renderTarget.Window.LockCursor = true;
			_renderTarget.Window.ShowCursor = false;

			// Grasshopper uses Reactive Extensions to expose input events as an
			// observable stream. In your own projects, you'll need to make sure
			// the Reactive Extensions NuGet packages are referenced in order to
			// access all the extension methods for observables.

			// Handle the two possible results of pressing the ESC key
			_app.Input.KeyboardEvents
				.Where(ev => ev.Key == Key.Escape && ev.State == KeyState.Down)
				.Subscribe(ev =>
				{
					if(isFocusLocked) // unlock the input controls from the view
					{
						_renderTarget.Window.LockCursor = false;
						_renderTarget.Window.ShowCursor = true;
						isFocusLocked = false;
					}
					else
						_app.Exit();
				});

			// Create observables for keypresses that only trigger when the controls are locked to the view
			var whenKeyHeldDown = _app.Input.KeyboardEvents.Where(ev => ev.State == KeyState.Down && isFocusLocked);
			var whenKeyReleased = _app.Input.KeyboardEvents.Where(ev => ev.State == KeyState.Up && isFocusLocked);

			Func<KeyboardEvent, bool> isForwardKey = ev => ev.Key == Key.Up || ev.Key == Key.W;
			Func<KeyboardEvent, bool> isBackwardKey = ev => ev.Key == Key.Down || ev.Key == Key.S;
			Func<KeyboardEvent, bool> isRollLeftKey = ev => ev.Key == Key.Left || ev.Key == Key.A;
			Func<KeyboardEvent, bool> isRollRightKey = ev => ev.Key == Key.Right || ev.Key == Key.D;

			whenKeyHeldDown.Where(isForwardKey).Subscribe(ev => _throttle = Throttle.Forward);
			whenKeyHeldDown.Where(isBackwardKey).Subscribe(ev => _throttle = Throttle.Backward);
			whenKeyHeldDown.Where(isRollLeftKey).Subscribe(ev => _rotation = Rotation.Left);
			whenKeyHeldDown.Where(isRollRightKey).Subscribe(ev => _rotation = Rotation.Right);

			whenKeyReleased.Where(isForwardKey).Subscribe(ev => _throttle = Throttle.None);
			whenKeyReleased.Where(isBackwardKey).Subscribe(ev => _throttle = Throttle.None);
			whenKeyReleased.Where(isRollLeftKey).Subscribe(ev => _rotation = Rotation.None);
			whenKeyReleased.Where(isRollRightKey).Subscribe(ev => _rotation = Rotation.None);

			// Handle mouse events for looking around ("free look")
			var whenMouseMoved = _app.Input.MouseEvents
				.Where(ev => ev.Type == MouseEventType.Move && ev.Window == null && isFocusLocked);

			whenMouseMoved.Where(ev => ev.DeltaX != 0)
				.Subscribe(ev => _camera.Yaw(-(float)(ev.DeltaX * Math.PI / 180)/7.5f));

			whenMouseMoved.Where(ev => ev.DeltaY != 0)
				.Subscribe(ev => _camera.Pitch((float)(ev.DeltaY * Math.PI / 180)/7.5f));

			// Handle the two possible results of clicking the mouse button
			_app.Input.MouseEvents
				.Where(ev => ev.Type == MouseEventType.Button && ev.ButtonState == ButtonState.Down)
				.Subscribe(ev =>
				{
					if(isFocusLocked) // reset the camera and position
					{
						_camera.Position = Vector3.Zero;
						_camera.Direction = -Vector3.UnitZ;
						_camera.Up = Vector3.UnitY;
					}
					else // lock the input controls back to the view
					{
						isFocusLocked = true;
						_renderTarget.Window.LockCursor = true;
						_renderTarget.Window.ShowCursor = false;
						_renderTarget.Window.SetCursorPositionToCenter();
					}
				});
		}

		private void CreateAndActivateDefaultMaterial()
		{
			// Load some textures and a sampler into GPU memory for use by materials
			_graphics.TextureResourceManager.Create("rabbit", "Textures/rabbit.jpg");
			_graphics.TextureResourceManager.Create("fish", "Textures/fish.jpg");
			_graphics.TextureResourceManager.Create("dog", "Textures/dog.jpg");
			_graphics.TextureResourceManager.Create("cat", "Textures/cat.jpg");
			_graphics.TextureResourceManager.Create("snail", "Textures/snail.jpg");

			// Prepare our default material which will simply render
			// out using the vertex colour. We then set the material
			// active, which sets the active shaders in GPU memory,
			// ready for drawing with.
			var material = _graphics.MaterialManager.Create("simple");
			material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, Vertex.ShaderInputLayout, CubeInstance.ShaderInputLayout);
			material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
			material.Textures.AddRange(new[] { "rabbit", "fish", "dog", "cat", "snail" });
			material.Activate();
		}

		static Matrix4x4 CreateProjectionMatrix(IAppWindow window)
		{
			const float fieldOfView = 0.65f;
			const float nearPlane = 0.1f;
			const float farPlane = 1000f;
			var aspectRatio = (float)window.ClientWidth / window.ClientHeight;
			return Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);
		}

		public void Dispose()
		{
			_constantBufferManager.Dispose();
			_meshBufferManager.Dispose();
			_renderTarget.Dispose();
			_graphics.Dispose();
			_app.Dispose();
		}
	}

	enum Throttle
	{
		None,
		Forward,
		Backward,
	}

	enum Rotation
	{
		None,
		Left,
		Right
	}

	[StructLayout(LayoutKind.Sequential)]
	struct Vertex
	{
		public Vector4 Position;
		public Color4 Color;
		public Vector2 TextureCoordinate;
		public Vector2 _pad0;

		public Vertex(Vector4 position, Color4 color, Vector2 textureCoordinate)
		{
			Position = position;
			Color = color;
			TextureCoordinate = textureCoordinate;
			_pad0 = Vector2.Zero;
		}

		public static readonly ShaderInputElementSpec[] ShaderInputLayout =
		{
			ShaderInputElementPurpose.Position.CreateSpec(),
			ShaderInputElementPurpose.Color.CreateSpec(),
			ShaderInputElementPurpose.TextureCoordinate.CreateSpec(),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float2, ShaderInputElementPurpose.Padding),
		};
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SceneData
	{
		public Matrix4x4 View;
		public Matrix4x4 Projection;
		public float SecondsElapsed;
		public Vector3 _pad0;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct CubeInstance
	{
		public Vector4 Position;
		public Vector4 Rotation;
		public Vector4 Scale;
		public int Texture;
		public Vector3 _pad0;

		public static readonly ShaderInputElementSpec[] ShaderInputLayout =
		{
			new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
			new ShaderInputElementSpec(ShaderInputElementFormat.Int32),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float3, ShaderInputElementPurpose.Padding),
		};
	}
}
