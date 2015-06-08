using System;
using System.Reactive.Linq;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Input;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using Grasshopper.WindowsFileSystem;

namespace FlappyCraft
{
	class Program
	{
		// Let's start by defining some starting values
		static float x = 0, y = 0; // our position on the map
		static float angle = 0; // the angle (in radians) of the direction we're facing
		static float height = 1; // our height above the baseline ground height
		static GameState gameState = GameState.StartScreen;
		static Camera camera = new Camera();
		private static IMeshGroup meshes;

		static void Main(string[] args)
		{
			GenerateNewMap();

			using(var app = new GrasshopperApp()
				.UseSharpDX()
				.UseWindowsFileSystem())
			using(var gfx = app.Graphics.CreateContext())
			using(var renderTarget = gfx.RenderTargetFactory.CreateWindow())
			{
				ConfigureInput(app);
				PrepareAssets(gfx);

				renderTarget.Window.Visible = true;
				renderTarget.Window.Width = 1280;
				renderTarget.Window.Height = 768;

				app.Run(renderTarget, context =>
				{
					context.Clear(Color.Tomato);
					DrawMap(context);
					if(gameState == GameState.InGame)
						DrawPlayer(context);
					context.Present();
				});
			}
		}

		private static void PrepareAssets(IGraphicsContext gfx)
		{
			var box = Cube.Unit("cube");
			
			gfx.MeshGroupBufferManager
				.Create("meshes", box)
				.Activate();

			gfx.TextureResourceManager.Create("grass-side", "Textures/grass-side.png");
			gfx.TextureResourceManager.Create("grass-top", "Textures/grass-top.png");
			gfx.TextureResourceManager.Create("dirt", "Textures/dirt.png");
			gfx.TextureResourceManager.Create("wood", "Textures/wood.png");
			gfx.TextureResourceManager.Create("leaves", "Textures/leaves.png");

			gfx.TextureSamplerManager
				.Create("8bit", new TextureSamplerSettings(TextureWrapping.Clamp, TextureWrapping.Clamp, TextureWrapping.Clamp, TextureFiltering.MinMagMipLinear))
				.Activate(0);

			var floorMaterial = gfx.MaterialManager.Create("floor");
			var ceilingMaterial = gfx.MaterialManager.Create("ceiling");
			var wallMaterial = gfx.MaterialManager.Create("wall");
		}

		private static void ConfigureInput(GrasshopperApp app)
		{
			var onMouseDown = app.Input.MouseEvents
				.Where(ev => ev.ButtonState == ButtonState.Down);

			// handle the click event on the start screen
			onMouseDown.Where(ev => gameState == GameState.StartScreen)
				.Subscribe(ev => { gameState = GameState.InGame; });

			// handle the click event on the game over screen
			onMouseDown.Where(ev => gameState == GameState.GameOver)
				.Subscribe(ev =>
				{
					gameState = GameState.StartScreen;
					ResetPlayer();
					GenerateNewMap();
				});

			// handle the click event while in game
			onMouseDown.Where(ev => gameState == GameState.InGame)
				.Subscribe(TriggerFlap);

			// quit the application if the user hits the escape key
			app.Input.KeyboardEvents
				.Where(ev => ev.Key == Key.Escape)
				.Subscribe(ev => app.Exit());
		}

		static void DrawMap(IWindowDrawingContext context)
		{
			DrawFloor(context);
			DrawCeiling(context);
			DrawWalls(context);
		}

		private static void DrawFloor(IWindowDrawingContext context)
		{
			context.Graphics.MaterialManager["floor"].Activate();
		}

		private static void DrawCeiling(IWindowDrawingContext context)
		{
			context.Graphics.MaterialManager["ceiling"].Activate();
		}

		private static void DrawWalls(IWindowDrawingContext context)
		{
			context.Graphics.MaterialManager["wall"].Activate();
		}

		private static void DrawPlayer(IWindowDrawingContext context)
		{
		}

		static void ResetPlayer()
		{
		}

		static void GenerateNewMap()
		{
		}

		static void TriggerFlap(MouseEvent ev)
		{
		}
	}

	enum GameState
	{
		StartScreen,
		InGame,
		GameOver
	}

	class Map
	{

	}

	class MapChunk
	{
		public MapChunk(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Locations = new MapLocation[width, height];
		}

		public MapLocation[,] Locations { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }
	}

	class MapLocation
	{
		public MapLocation(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; private set; }
		public int Y { get; private set; }
	}

	struct FloorInstance
	{
	}
}
