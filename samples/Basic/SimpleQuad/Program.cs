using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Geometry.Primitives;
using Grasshopper.Graphics.Materials;
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
				var material = new MaterialSpec("simple");
				material.VertexShader = new VertexShaderSpec(Resources.VertexShader);
				material.PixelShader = new PixelShaderSpec(Resources.PixelShader);
				gfx.MaterialManager.Add(material);
				gfx.MaterialManager.SetActive(material.Id);

				// Create a mesh which is simply a quad (4 vertices, each of a different colour). Add it to a new
				// mesh group which we then pass to the buffer manager for initialization and activation. Mesh buffers
				// are uniform blocks of mesh vertex data packed together so we can avoid switching buffers too often,
				// but in our case we only pack one mesh into the buffer.
				var quad = Quad.Homogeneous(
					color1: Color.Red,
					color2: Color.Green,
					color3: Color.Blue,
					color4: Color.Yellow);
				var mesh = quad.ToMesh("quad");
				var meshGroup = new MeshGroup("default", mesh);
				gfx.MeshGroupBufferManager.Add(meshGroup);
				gfx.MeshGroupBufferManager.SetActive(meshGroup.Id);

				// As mesh buffers can hold many meshes, we have to identify which of the active buffer's meshes to draw next.
				// We only have one mesh in the buffer, but we still need to get an object representing its location, as we'll
				// use that to indicate what to draw.
				var location = gfx.MeshGroupBufferManager.GetMeshLocation(mesh.Id);

				// Our quad is already defined using homogeneous coordinates, so we'll just draw it as is.
				app.Run(renderHost, context =>
				{
					context.Clear(Color.CornflowerBlue);
					context.Draw(location);
					context.Present();
				});
			}
		}
	}
}
