namespace Grasshopper.WindowsFileSystem
{
	public static class WindowsFileSystemBootstrapper
	{
		public static GrasshopperApp UseWindowsFileSystem(this GrasshopperApp app)
		{
			var assetReader = new AssetReader();
			app.AssetReader = assetReader;
			return app;
		}
	}
}
