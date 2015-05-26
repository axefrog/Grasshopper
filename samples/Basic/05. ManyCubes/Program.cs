using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using ManyCubes.Properties;

namespace ManyCubes
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.Graphics.CreateContext(enableDebugMode: true))
			using(var renderer = gfx.RenderHostFactory.CreateWindowed())
			{
				renderer.Window.Title = "100,000 Cubes";
				renderer.Window.ShowBordersAndTitle = true;
				renderer.Window.Visible = true;
				renderer.Window.Resizable = true;

				// Prepare our default material which will simply render out using the vertex colour. We
				// then set the material active, which sets the active shaders in GPU memory, ready for
				// drawing with. Note the declaration of vertex shader input elements for each cube
				// instance. See the matching CubeInstance struct at the bottom of this file.
				var material = new MaterialSpec("simple");
				material.VertexShader = new VertexShaderSpec(Resources.VertexShader, new []
				{
					new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
					new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
					new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
				});
				material.PixelShader = new PixelShaderSpec(Resources.PixelShader);
				gfx.MaterialManager.Add(material);
				gfx.MaterialManager.SetActive(material.Id);

				// Procedurally create a simple cube mesh (12 triangles, 36 vertices, 8 vertex colours).
				// Add it to a new mesh group which we then pass to the buffer manager for initialization
				// and activation. Mesh buffers are uniform blocks of mesh vertex data packed together so
				// we can avoid switching buffers too often, but in our case we only pack one mesh into
				// the buffer.
				var cube = Cube.Unit("cube", Color.Red, Color.LimeGreen, Color.Yellow, Color.Orange,
					Color.Blue, Color.Magenta, Color.White, Color.Cyan);
				cube.Scale(0.25f); // make the cube a bit smaller (there are gonna be a lot on screen!)
				var meshGroup = new MeshGroup("default", cube);
				gfx.MeshGroupBufferManager.Add(meshGroup);
				gfx.MeshGroupBufferManager.SetActive(meshGroup.Id);

				// As mesh buffers can hold many meshes, we have to identify which of the active buffer's
				// meshes to draw next. We only have one mesh in the buffer, but we still need to get an
				// object representing its location, as we'll use that to indicate what to draw.
				var meshLocationInBuffer = gfx.MeshGroupBufferManager.GetMeshLocation("cube");

				// There are many possible configurations of constant buffer and we define ours by
				// specifying the struct type that will represent it. Ours buffer holds our scene data, so
				// we'll configure a constant buffer of struct type SceneData (defined at the bottom of
				// this file), as the data is copied directly from there to GPU memory.
				using(var constantBufferManager = gfx.ConstantBufferManagerFactory.Create<SceneData>())

				// Unlike regular mesh buffers, which just hold the base vertex information, we have to
				// manually define the shape of our instance buffer and get a manager object which can
				// create and manage instance buffers that hold that configuration of instance data. In
				// our case, our instances contain a view matrix, a projection matrix, and the number of
				// seconds that have elapsed. This information is defined in the SceneData struct (at the
				// bottom of this file) so our buffer manager is configured for type SceneData. See the
				// shader source (VertexShader.hlsl) to see how the shader receives the instance data.
				using(var instanceBufferManager = gfx.MeshInstanceBufferManagerFactory.Create<CubeInstance>())
				{
					// Create and populate the instance buffer with 100,000 cube instances, then set the
					// buffer as active for the next draw calls.
					var rand = new Random();
					const int instanceCount = 100000;
					instanceBufferManager.Add("cubes",
						Enumerable.Range(0, instanceCount)
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
						})
						.ToArray());
					instanceBufferManager.SetActive("cubes");

					// Create our initial view matrix defining the camera location and orientation and
					// populate our SceneData structure with it.
					var view = Matrix4x4.CreateLookAt(new Vector3(0, 1.25f, -75f), new Vector3(0, 0, 0), Vector3.UnitY);
					var data = new SceneData { View = Matrix4x4.Transpose(view) };
					var hasWindowSizeChanged = true;

					// The aspect ratio changes when the window is resized, so we need to reculate the
					// projection matrix, or our scene will look squashed
					renderer.Window.SizeChanged += win => hasWindowSizeChanged = true;

					// We can now create and activate our constant buffer which will hold the scene data
					// for use by the vertex shader
					constantBufferManager.Add("scene");
					constantBufferManager.SetActive("scene");

					// Let the looping begin!
					app.Run(renderer, context =>
					{
						// This is always true the first time, so our projection matrix is created here.
						// Each iteration of the render loop we'll update the constant buffer with the
						// current number of elapsed seconds, and the current view and projection matrices
						if(hasWindowSizeChanged)
						{
							hasWindowSizeChanged = false;
							data.Projection = Matrix4x4.Transpose(CreateProjectionMatrix(context.Window));
						}
						data.SecondsElapsed = app.ElapsedSeconds;
						constantBufferManager.Update("scene", data);

						context.Clear(Color.CornflowerBlue);
						context.DrawInstanced(meshLocationInBuffer, instanceCount);
						context.Present();
					});
				}
			}
		}

		static Matrix4x4 CreateProjectionMatrix(IAppWindow window)
		{
			const float fieldOfView = 0.65f;
			const float nearPlane = 0.1f;
			const float farPlane = 1000f;
			var aspectRatio = (float)window.ClientWidth / window.ClientHeight;
			return Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SceneData
	{
		public Matrix4x4 View;
		public Matrix4x4 Projection;
		public float SecondsElapsed;
		private readonly Vector3 _pad0;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct CubeInstance
	{
		public Vector4 Position;
		public Vector4 Rotation;
		public Vector4 Scale;
	}
}
