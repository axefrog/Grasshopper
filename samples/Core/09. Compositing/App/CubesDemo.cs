using System;
using System.Linq;
using System.Numerics;
using CompositingSample.Properties;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Procedural.Graphics.Primitives;

namespace CompositingSample.App
{
	public class CubesDemo : GrasshopperApp
	{
		private readonly int _totalCubes;
		private IGraphicsContext _gfx;
		private IWindowRenderTarget _windowRenderTarget;
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
			get { return _windowRenderTarget.Window; }
		}

		public void InitializeResources()
		{
			_gfx = Graphics.CreateContext(true);
			_windowRenderTarget = _gfx.RenderTargetFactory.CreateWindow();

			_sceneDataConstantBuffers = _gfx.ConstantBufferManagerFactory.Create<SceneData>();
			_cubeInstanceBuffers = _gfx.MeshInstanceBufferManagerFactory.Create<CubeInstance>();

			InitializeWindow();

			CreateAndActivateMaterial();
			CreateAndActivateMeshBuffer();
			CreateAndActivateCubeInstances();
			CreateAndActivateSceneDataConstantBuffer();
		}

		private void InitializeWindow()
		{
			var window = _windowRenderTarget.Window;
			window.Title = _totalCubes.ToString("0,000") + " Textured Cubes";
			window.ShowBordersAndTitle = true;
			window.Visible = true;
			window.Resizable = true;
			window.SetWindowMaximized();
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

		private void CreateAndActivateMeshBuffer()
		{
			var cube = Cube.Unit("cube", Color.Red, Color.LimeGreen, Color.Yellow, Color.Orange,
				Color.Blue, Color.Magenta, Color.White, Color.Cyan);
			cube.Scale(0.25f);
			var buffers = _gfx.MeshGroupBufferManager.Create("default", cube);

			// Here we're introducing an additional mesh into our mesh buffer. This mesh
			// will be used for compositing
			//var quad

			buffers.Activate();
		}

		private void CreateAndActivateCubeInstances()
		{
			var rand = new Random();
			var buffers = _cubeInstanceBuffers.Create("default", Enumerable.Range(0, _totalCubes)
				.Select(i =>
				{
					var rotX = rand.Next(200) - 100.0f;
					var rotY = rand.Next(200) - 100.0f;
					var rotZ = rand.Next(200) - 100.0f;
					var rotS = (rand.Next(200)) / 5000.0f;
					var posX = (rand.Next(20000) - 10000) / 100.0f;
					var posY = (rand.Next(20000) - 10000) / 100.0f;
					var posZ = (rand.Next(20000) - 10000) / 100.0f;
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

				_windowRenderTarget.Render(context =>
				{
					context.Clear(Color.CornflowerBlue);
					meshes.DrawInstanced("cube", _totalCubes);
					context.Present();
				});
				
				return !_windowRenderTarget.Terminated;
			});
		}

		public override void Dispose()
		{
			_sceneDataConstantBuffers.Dispose();
			_cubeInstanceBuffers.Dispose();
			_windowRenderTarget.Dispose();
			_gfx.Dispose();
		}
	}
}
