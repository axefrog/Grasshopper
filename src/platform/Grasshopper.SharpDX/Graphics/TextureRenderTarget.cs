using Grasshopper.Graphics.Rendering;
using Grasshopper.SharpDX.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	class TextureRenderTarget : RenderTarget<ITextureDrawingContext>, ITextureRenderTarget
	{
		private readonly TextureDrawingContext _drawingContext;

		public TextureRenderTarget(DeviceManager deviceManager, int width, int height)
			: this(new TextureDrawingContext(deviceManager, width, height))
		{
		}

		private TextureRenderTarget(TextureDrawingContext drawingContext) : base(drawingContext)
		{
			_drawingContext = drawingContext;
		}

		public void Initialize()
		{
			_drawingContext.Initialize();
		}

		public void ActivateTextureResource(int index)
		{
			
		}
	}
}
