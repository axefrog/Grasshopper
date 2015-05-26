using System;
using System.Numerics;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using SimpleCube.Properties;

namespace SimpleCube
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.Graphics.CreateContext(enableDebugMode: true))
			using(var renderer = gfx.RenderHostFactory.CreateWindowed())
			{
				renderer.Window.Title = "Simple Cube";
				renderer.Window.ShowBordersAndTitle = true;
				renderer.Window.Visible = true;
				renderer.Window.Resizable = true;

				var material = new MaterialSpec("simple");
				material.VertexShader = new VertexShaderSpec(Resources.VertexShader);
				material.PixelShader = new PixelShaderSpec(Resources.PixelShader);
				gfx.MaterialManager.Add(material);
				gfx.MaterialManager.SetActive(material.Id);

				// Procedurally create a simple cube mesh (12 triangles, 36 vertices, 8 vertex colours). Add it to a new
				// mesh group which we then pass to the buffer manager for initialization and activation. Mesh buffers
				// are uniform blocks of mesh vertex data packed together so we can avoid switching buffers too often,
				// but in our case we only pack one mesh into the buffer.
				var cube = Cube.Unit("cube", Color.Red, Color.LimeGreen, Color.Yellow, Color.Orange,
					Color.Blue, Color.Magenta, Color.White, Color.Cyan);
				var meshGroup = new MeshGroup("default", cube);
				gfx.MeshGroupBufferManager.Add(meshGroup);
				gfx.MeshGroupBufferManager.SetActive(meshGroup.Id);

				// As mesh buffers can hold many meshes, we have to identify which of the active buffer's meshes to draw
				// next. We only have one mesh in the buffer, but we still need to get an object representing its
				// location, as we'll use that to indicate what to draw.
				var meshLocationInBuffer = gfx.MeshGroupBufferManager.GetMeshLocation("cube");

				// There are many possible configurations of constant buffer and we define ours by specifying the struct
				// type that will represent it. Ours buffer holds a matrix, so we'll configure a constant buffer of type
				// Matrix4x4, as the data is copied directly from such a struct to the constant buffer in GPU memory.
				using(var constantBufferManager = gfx.ConstantBufferManagerFactory.Create<Matrix4x4>())
				{
					// Create our initial world, view and projection matrices that will represent the cube and camera
					// location and orientation
					var world = Matrix4x4.Identity;
					var view = Matrix4x4.CreateLookAt(new Vector3(0, 1.25f, -3f), new Vector3(0, 0, 0), Vector3.UnitY);
					var proj = CreateProjectionMatrix(renderer.Window);
					var viewproj = view * proj;

					// The aspect ratio changes when the window is resized, so we need to reculate the projection matrix
					renderer.Window.SizeChanged += win => viewproj = view * CreateProjectionMatrix(win);

					// We can now create and activate our constant buffer which will hold the final world-view-projection
					// matrix for use by the vertex shader
					constantBufferManager.Add("worldviewproj", world);
					constantBufferManager.SetActive("worldviewproj");

					// Let's define an arbitrary rotation speed for the cube
					const float rotationsPerSecond = .04f;
					const float twoPI = (float)Math.PI * 2;
					
					// Let the looping begin!
					app.Run(renderer, context =>
					{
						// Each frame we need to update the cube's rotation, then push the changes onto the GPU
						world = Matrix4x4.CreateRotationY(app.ElapsedSeconds * rotationsPerSecond * twoPI);
						var wvp = world * viewproj;
						wvp = Matrix4x4.Transpose(wvp);
						constantBufferManager.Update("worldviewproj", wvp);

						context.Clear(Color.CornflowerBlue);
						context.Draw(meshLocationInBuffer);
						context.Present();
					});
				}
			}
		}

		static Matrix4x4 CreateProjectionMatrix(IAppWindow window)
		{
			return Matrix4x4.CreatePerspectiveFieldOfView(.65f, (float)window.ClientWidth / window.ClientHeight, 0.1f, 1000f);
		}
	}
}
