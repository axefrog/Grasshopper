using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.SharpDX;
using SimpleQuad.Properties;

namespace SimpleQuad
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.Graphics.CreateContext(enableDebugMode: true))
			using(var renderHost = gfx.RenderHostFactory.CreateWindowed())
			{
				renderHost.Window.Title = "Simple Quad";
				renderHost.Window.ShowBordersAndTitle = true;
				renderHost.Window.Visible = true;
				renderHost.Window.Resizable = true;

				// Prepare our default material which will simply render out using the vertex colour. We then set
				// the material active, which sets the active shaders in GPU memory, ready for drawing with.
				var material = gfx.MaterialManager.Create("simple");
				material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader);
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

				// Our quad is already defined using homogeneous coordinates, so we'll just draw it as is.
				app.Run(renderHost, context =>
				{
					context.Clear(Color.CornflowerBlue);
					meshes.Draw(mesh.Id);
					context.Present();
				});
			}
		}
	}
}
