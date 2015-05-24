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
			using(var gfx = app.Graphics.CreateContext())
			using(var renderHost = gfx.RenderHostFactory.CreateWindowed())
			{
				renderHost.Window.Title = "Simple Quad";
				renderHost.Window.ShowBordersAndTitle = true;
				renderHost.Window.Visible = true;
				renderHost.Window.Resizable = true;

				var material = new MaterialSpec("simple");
				material.PixelShader = new ShaderSpec(Resources.PixelShader);
				material.VertexShader = new VertexShaderSpec(Resources.VertexShader, new[]
				{
					ShaderInputElementPurpose.Position.Spec(),
					ShaderInputElementPurpose.Color.Spec(),
				});
				gfx.MaterialManager.Initialize(material);
				gfx.MaterialManager.SetActive(material.Id);

				var quad = Quad.Homogeneous().SetColors(Color.Red, Color.Green, Color.Blue, Color.Yellow);
				var mesh = quad.ToMesh("quad");
				var meshGroup = new MeshGroup("default", mesh);
				gfx.MeshGroupBufferManager.Initialize(meshGroup);
				gfx.MeshGroupBufferManager.SetActive(meshGroup.Id);
				var bufferLocation = gfx.MeshGroupBufferManager.GetMeshLocation(mesh.Id);

				app.Run(renderHost, context =>
				{
					context.Clear(Color.CornflowerBlue);
					context.Draw(bufferLocation);
					context.Present();
				});
			}
		}
	}
}
