using System;
using System.Numerics;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
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

				// Prepare our default material which will simply render out using the vertex colour. We
				// then set the material active, which sets the active shaders in GPU memory, ready for
				// drawing with.
				var material = gfx.MaterialManager.Create("simple");
				material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader);
				material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
				material.Activate();

				// Procedurally create a simple cube mesh (12 triangles, 36 vertices, 8 vertex colours).
				// Add it to a new mesh group which we then pass to the buffer manager for initialization
				// and activation. Mesh buffers are uniform blocks of mesh vertex data packed together so
				// we can avoid switching buffers too often, but in our case we only pack one mesh into
				// the buffer.
				var cube = Cube.Unit("cube", Color.Red, Color.LimeGreen, Color.Yellow, Color.Orange,
					Color.Blue, Color.Magenta, Color.White, Color.Cyan);
				var meshes = gfx.MeshGroupBufferManager.Create("default", cube);
				meshes.Activate();

				// There are many possible configurations of constant buffer and we define ours by
				// specifying the struct type that will represent it. Ours buffer holds a matrix, so we'll
				// configure a constant buffer of type Matrix4x4, as the data is copied directly from such
				// a struct to the constant buffer in GPU memory.
				using(var constantBufferManager = gfx.ConstantBufferManagerFactory.Create<Matrix4x4>())
				{
					// Create our initial world, view and projection matrices that will represent the cube
					// and camera location and orientation
					var world = Matrix4x4.Identity;
					var view = Matrix4x4.CreateLookAt(new Vector3(0, 1.25f, 3f), new Vector3(0, 0, 0), Vector3.UnitY);
					var proj = CreateProjectionMatrix(renderer.Window);
					var viewproj = view * proj;

					// The aspect ratio changes when the window is resized, so we need to reculate the
					// projection matrix, or our cube will look squashed
					renderer.Window.SizeChanged += win => viewproj = view * CreateProjectionMatrix(win);

					// We can now create and activate our constant buffer which will hold the final world-
					// view-projection matrix for use by the vertex shader
					constantBufferManager.Create("worldviewproj", ref world);
					constantBufferManager.Activate(0, "worldviewproj");

					// Let's define an arbitrary rotation speed for the cube
					const float rotationsPerSecond = .04f;
					const float twoPI = (float)Math.PI * 2;
					
					// Let the looping begin!
					app.Run(renderer, context =>
					{
						// Each frame we need to update the cube's rotation, then push the changes to the
						// constant buffer data onto the GPU
						world = Matrix4x4.CreateRotationY(app.ElapsedSeconds * rotationsPerSecond * twoPI);
						var wvp = world * viewproj;
						wvp = Matrix4x4.Transpose(wvp);
						constantBufferManager.Update("worldviewproj", ref wvp);

						context.Clear(Color.CornflowerBlue);
						meshes.Draw(cube.Id);
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
}
