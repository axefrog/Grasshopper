using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using ManyCubes.Properties;

namespace ManyCubes
{
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

		public Program()
		{
			_app = new GrasshopperApp().UseSharpDX();
			_graphics = _app.Graphics.CreateContext(enableDebugMode: true);
			_renderTarget = _graphics.RenderTargetFactory.CreateWindow();
			_constantBufferManager = _graphics.ConstantBufferManagerFactory.Create<SceneData>();
			_meshBufferManager = new MeshBufferManager<Vertex>(_graphics);
			_instanceBufferManager = _graphics.VertexBufferManagerFactory.Create<CubeInstance>();

			_renderTarget.Window.Title = "Many Cubes";
			_renderTarget.Window.Visible = true;
		}

		public void Run()
		{
			CreateAndActivateDefaultMaterial();

			// Use Grasshopper's procedural tools to create a cube mesh
			var mesh = CubeBuilder.New
				.Size(0.25f)
				.ToMesh("cube", v => new Vertex(v.Position, v.Color));

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
					var rotS = (rand.Next(190) + 10) / 100.0f;
					// cube offset from the origin
					var posX = (rand.Next(10000) - 5000) / 100.0f;
					var posY = (rand.Next(10000) - 5000) / 100.0f;
					var posZ = (rand.Next(10000) - 5000) / 100.0f;
					// cube size variation
					var scale = (rand.Next(8) == 0 ? (rand.Next(800) + 100.0f) : rand.Next(200) + 50.0f) / 100.0f;
					return new CubeInstance
					{
						Position = new Vector4(posX, posY, posZ, 0),
						Rotation = new Vector4(rotX, rotY, rotZ, rotS),
						Scale = new Vector4(scale, scale, scale, 1)
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

			// The aspect ratio changes when the window is resized, so
			// we need to reculate the projection matrix, or our cube
			// will look squashed
			var hasWindowSizeChanged = true;
			_renderTarget.Window.SizeChanged += win => hasWindowSizeChanged = true;

			// Define the data that will go into the constant buffer
			var sceneData = new SceneData { View = Matrix4x4.Transpose(view) };
			constantBuffer.Update(ref sceneData);

			// Let the looping begin!
			_app.Run(_renderTarget, context =>
			{
				// This is always true the first time, so our
				// projection matrix is created here. Each iteration of
				// the render loop we'll update the constant buffer
				// with the current number of elapsed seconds, and the
				// current view and projection matrices
				if(hasWindowSizeChanged)
				{
					hasWindowSizeChanged = false;
					sceneData.Projection = Matrix4x4.Transpose(CreateProjectionMatrix(context.Window));
				}
				sceneData.SecondsElapsed = _app.ElapsedSeconds;
				constantBuffer.Update(ref sceneData);

				context.Clear(Color.CornflowerBlue);
				context.SetDrawType(cubeData.DrawType);
				context.DrawIndexedInstanced(cubeData.IndexCount, instanceCount, cubeData.IndexBufferOffset, cubeData.VertexBufferOffset, 0);
				context.Present();
			});
		}

		private void CreateAndActivateDefaultMaterial()
		{
			// Prepare our default material which will simply render
			// out using the vertex colour. We then set the material
			// active, which sets the active shaders in GPU memory,
			// ready for drawing with.
			var material = _graphics.MaterialManager.Create("simple");
			material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, Vertex.ShaderInputLayout, CubeInstance.ShaderInputLayout);
			material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
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

	[StructLayout(LayoutKind.Sequential)]
	struct Vertex
	{
		public Vector4 Position;
		public Color4 Color;

		public Vertex(Vector4 position, Color4 color)
		{
			Position = position;
			Color = color;
		}

		/// <summary>
		/// Defines values that we'll use to tell the vertex shader how
		/// to map our vertex structure to the shader's input structure
		/// </summary>
		public static readonly ShaderInputElementSpec[] ShaderInputLayout =
		{
			ShaderInputElementPurpose.Position.CreateSpec(),
			ShaderInputElementPurpose.Color.CreateSpec()
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

		public static readonly ShaderInputElementSpec[] ShaderInputLayout =
		{
			new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
			new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
		};
	}
}
