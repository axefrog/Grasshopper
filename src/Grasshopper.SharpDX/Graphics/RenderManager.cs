using Grasshopper.Graphics.Materials;
using Grasshopper.SharpDX.Graphics.Materials;

namespace Grasshopper.SharpDX.Graphics
{
	public class RenderManager : Grasshopper.Graphics.Rendering.RenderManager
	{
		private readonly GraphicsContext _graphics;
		private readonly ShaderManager _shaders;

		public RenderManager(GraphicsContext graphics) : base(graphics)
		{
			_graphics = graphics;
			_shaders = new ShaderManager(graphics.DeviceManager);
		}

		public override void PrepareMaterial(MaterialSpec material)
		{
			if(_shaders.Exists(material.Id))
				_shaders.Remove(material.Id);
			_shaders.Add(material);
		}

		public override void SetMaterial(MaterialSpec material)
		{
			_shaders.SetActive(material.Id);
		}
	}
}
