using System.Numerics;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Geometry.Primitives;
using Grasshopper.Graphics.Materials;
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
			using(var instanceBufferManager = gfx.MeshInstanceBufferManagerFactory.Create<Matrix4x4>())
			{
				renderHost.Window.Title = "Instanced Quads";
				renderHost.Window.ShowBordersAndTitle = true;
				renderHost.Window.Visible = true;
				renderHost.Window.Resizable = true;

				// Prepare our default material which will simply render out using the vertex colour
				var material = new MaterialSpec("simple");
				material.VertexShader = new VertexShaderSpec(Resources.VertexShader, new []
				{
					new ShaderInputElementSpec(ShaderInputElementFormat.Matrix4x4),
				});
				material.PixelShader = new PixelShaderSpec(Resources.PixelShader);
				gfx.MaterialManager.Add(material);
				gfx.MaterialManager.SetActive(material.Id);

				// Prepare mesh group consisting of only a simple quad mesh and load it into a mesh buffer
				var quad = Quad.Homogeneous(
					color1: Color.Red,
					color2: Color.LimeGreen,
					color3: Color.Blue,
					color4: Color.Yellow);
				var mesh = quad.ToMesh("quad");
				var meshGroup = new MeshGroup("default", mesh);
				gfx.MeshGroupBufferManager.Add(meshGroup);
				gfx.MeshGroupBufferManager.SetActive(meshGroup.Id);

				// Mesh buffers can hold many meshes, so we have to identify which of the active buffer's meshes to draw next
				var location = gfx.MeshGroupBufferManager.GetMeshLocation(mesh.Id);

				// Prepare an array of instances of the mesh that we'd like to draw, with each instance being a simple transform matrix to set its position on screen
				var scaled = (Matrix4x4.Identity * 0.8f);
				scaled.M44 = 1.0f;
				const float dist = .4f;
				var instance1 = scaled * Matrix4x4.CreateTranslation(-dist, dist, 0);
				var instance2 = scaled * Matrix4x4.CreateTranslation(dist, dist, 0);
				var instance3 = scaled * Matrix4x4.CreateTranslation(-dist, -dist, 0);
				var instance4 = scaled * Matrix4x4.CreateTranslation(dist, -dist, 0);
				var instances = new[] { instance1, instance2, instance3, instance4 };
				instanceBufferManager.Add("quads", instances);
				instanceBufferManager.SetActive("quads");

				app.Run(renderHost, context =>
				{
					context.Clear(Color.CornflowerBlue);
					context.DrawInstances(location, instances.Length);
					context.Present();
				});
			}
		}
	}
}
