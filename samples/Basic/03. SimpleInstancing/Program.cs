using System.Collections.Generic;
using System.Numerics;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.SharpDX;
using SimpleInstancing.Properties;

namespace SimpleInstancing
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.Graphics.CreateContext(enableDebugMode: true))
			using(var renderHost = gfx.RenderHostFactory.CreateWindowed())
			{
				renderHost.Window.Title = "Instanced Quads";
				renderHost.Window.ShowBordersAndTitle = true;
				renderHost.Window.Visible = true;
				renderHost.Window.Resizable = true;

				// Prepare our default material which will simply render out using the vertex colour. We then set
				// the material active, which sets the active shaders in GPU memory, ready for drawing with.
				var material = gfx.MaterialManager.Create("simple");
				material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, new[]
				{
					// Specify the vertex shader input elements for each instance that we'll draw. We don't have
					// to specify the layout for the base vertex itself because Grasshopper uses a standard vertex
					// format which never changes, so we only have to indicate what is included in each instance.
					new ShaderInputElementSpec(ShaderInputElementFormat.Matrix4x4),
				});
				material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
				material.Activate();

				// Create a mesh which is simply a quad (4 vertices, each of a different colour). Add it to a new
				// mesh group which we then pass to the buffer manager for initialization and activation. Mesh buffers
				// are uniform blocks of mesh vertex data packed together so we can avoid switching buffers too often,
				// but in our case we only pack one mesh into the buffer.
				var quad = Quad.Homogeneous(Color.Red, Color.Green, Color.Blue, Color.Yellow);
				var mesh = quad.ToMesh("quad");
				var meshes = gfx.MeshGroupBufferManager.Create("default", mesh);
				meshes.Activate();

				// We want to draw the same mesh multiple times, with slightly different data for each instance, so we'll define
				// an array of four transformation matrices that will be provide the data for each of four instances we'll draw.
				var scaled = (Matrix4x4.Identity * 0.3f);
				scaled.M44 = 1.0f;
				const float dist = .4f;
				var instance1 = scaled * Matrix4x4.CreateTranslation(-dist, dist, 0);
				var instance2 = scaled * Matrix4x4.CreateTranslation(dist, dist, 0);
				var instance3 = scaled * Matrix4x4.CreateTranslation(-dist, -dist, 0);
				var instance4 = scaled * Matrix4x4.CreateTranslation(dist, -dist, 0);
				var instances = new List<Matrix4x4> { instance1, instance2, instance3, instance4 };

				// Unlike regular mesh buffers, which just hold the base vertex information, we have to manually
				// define the shape of our instance buffer and get a manager object which can create and manage
				// instance buffers that hold that configuration of instance data. In our case, our instances
				// only contain a transformation matrix, so our buffer manager is configured for Matrix4x4. See
				// The shader source (VertexShader.hlsl) to see how the shader receives the instance data.
				using(var instanceBufferManager = gfx.MeshInstanceBufferManagerFactory.Create<Matrix4x4>())
				{
					// We can of course reuse this buffer manager object for instances of anything that have this
					// configuration of data for each instance of what is drawn. In our case, we're just using it
					// for drawing instances of our quad mesh, so we'll create and activate a buffer containing
					// that information.
					instanceBufferManager
						.Create("quads", instances)
						.Activate();

					app.Run(renderHost, context =>
					{
						context.Clear(Color.CornflowerBlue);
						meshes.DrawInstanced(mesh.Id, instances.Count);
						context.Present();
					});
				}
			}
		}
	}
}
