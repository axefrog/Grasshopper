using Grasshopper.Graphics;

namespace Grasshopper
{
	public class ServiceLocator
	{
		public IAppWindowFactory Windows { get; set; }
		public IRendererFactory Renderers { get; set; }
	}
}