using System;
using System.Linq;
using System.Numerics;
using FreeLookCamera.Properties;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Procedural.Graphics.Primitives;

namespace FreeLookCamera.App
{
	public class CubesDemo : GrasshopperApp
	{
		private readonly int _totalCubes;
		private IGraphicsContext _gfx;
		private IWindowRenderHost _renderHost;
		private readonly Camera _camera;
		private IConstantBufferManager<SceneData> _sceneDataConstantBuffers;
		private IMeshInstanceBufferManager<CubeInstance> _cubeInstanceBuffers;
		private SceneData _sceneData;

		public CubesDemo(int totalCubes)
		{
			_totalCubes = totalCubes;
			_camera = new Camera();
		}

		public Camera Camera
		{
			get { return _camera; }
		}

		public IAppWindow Window
		{
			get { return _renderHost.Window; }
		}

		public void InitializeResources()
		{
			_gfx = Graphics.CreateContext(true);
			_renderHost = _gfx.RenderHostFactory.CreateWindowed();
			_sceneDataConstantBuffers = _gfx.ConstantBufferManagerFactory.Create<SceneData>();
			_cubeInstanceBuffers = _gfx.MeshInstanceBufferManagerFactory.Create<CubeInstance>();

			InitializeWindow();

			CreateAndActivateMaterial();
			CreateAndActivateCubeMeshBuffer();
			CreateAndActivateCubeInstances();
			CreateAndActivateSceneDataConstantBuffer();
		}

		private void InitializeWindow()
		{
			var window = _renderHost.Window;
			window.Title = "50,000 Textured Cubes";
			window.ShowBordersAndTitle = true;
			window.Visible = true;
			window.Resizable = true;
			window.SizeChanged += UpdateAspectRatio;
			UpdateAspectRatio(window);
		}

		private void UpdateAspectRatio(IAppWindow window)
		{
			Camera.AspectRatio = (float)window.ClientWidth / window.ClientHeight;
		}

		private void CreateAndActivateMaterial()
		{
			_gfx.TextureResourceManager.Create("rabbit", "Textures/rabbit.jpg");
			_gfx.TextureResourceManager.Create("fish", "Textures/fish.jpg");
			_gfx.TextureResourceManager.Create("dog", "Textures/dog.jpg");
			_gfx.TextureResourceManager.Create("cat", "Textures/cat.jpg");
			_gfx.TextureResourceManager.Create("snail", "Textures/snail.jpg");
			_gfx.TextureSamplerManager.Create("default", new TextureSamplerSettings(TextureWrapping.Clamp, TextureWrapping.Clamp, TextureWrapping.Clamp, TextureFiltering.Anisotropic));

			var material = _gfx.MaterialManager.Create("simple");
			material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, new[]
				{
					new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
					new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
					new ShaderInputElementSpec(ShaderInputElementFormat.Float4),
					new ShaderInputElementSpec(ShaderInputElementFormat.Int32),
					new ShaderInputElementSpec(ShaderInputElementFormat.Float3, ShaderInputElementPurpose.Padding),
				});
			material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
			material.Textures.AddRange(new[] { "rabbit", "fish", "dog", "cat", "snail" });
			material.Samplers.Add("default");
			material.Activate();
		}

		private void CreateAndActivateCubeMeshBuffer()
		{
			var cube = Cube.Unit("cube", Color.Red, Color.LimeGreen, Color.Yellow, Color.Orange,
				Color.Blue, Color.Magenta, Color.White, Color.Cyan);
			cube.Scale(0.25f); // make the cube a bit smaller (there are gonna be a lot on screen!)
			var buffers = _gfx.MeshGroupBufferManager.Create("default", cube);
			buffers.Activate();
		}

		private void CreateAndActivateCubeInstances()
		{
			var rand = new Random();
			var buffers = _cubeInstanceBuffers.Create("default", Enumerable.Range(0, _totalCubes)
				.Select(i =>
				{
					// rotation on each axis
					var rotX = rand.Next(200) - 100.0f;
					var rotY = rand.Next(200) - 100.0f;
					var rotZ = rand.Next(200) - 100.0f;
					// rotation speed
					var rotS = (rand.Next(190) + 10) / 100.0f;
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
				})
				.ToList());
			buffers.Activate();
		}

		private void CreateAndActivateSceneDataConstantBuffer()
		{
			var constantBuffer = _sceneDataConstantBuffers.Create("scenedata");
			constantBuffer.Activate(0);
		}

		public override void Run(Func<bool> main)
		{
			var meshes = _gfx.MeshGroupBufferManager["default"];

			base.Run(() =>
			{
				if(!main()) return false;

				_sceneData.Projection = Matrix4x4.Transpose(Camera.ProjectionMatrix);
				_sceneData.View = Matrix4x4.Transpose(Camera.ViewMatrix);
				_sceneData.SecondsElapsed = ElapsedSeconds;
				_sceneDataConstantBuffers.Update("scenedata", ref _sceneData);

				_renderHost.Render(context =>
				{
					context.Clear(Color.CornflowerBlue);
					meshes.DrawInstanced("cube", _totalCubes);
					context.Present();
				});
				
				return !_renderHost.ExitRequested;
			});
		}

		public override void Dispose()
		{
			_sceneDataConstantBuffers.Dispose();
			_cubeInstanceBuffers.Dispose();
			_renderHost.Dispose();
			_gfx.Dispose();
		}
	}
}
