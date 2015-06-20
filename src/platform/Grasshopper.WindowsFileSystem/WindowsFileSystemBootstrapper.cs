namespace Grasshopper.WindowsFileSystem
{
	public static class WindowsFileSystemBootstrapper
	{
		public static T UseWindowsFileSystem<T>(this T app) where T : GrasshopperApp
		{
			var assetReader = new FileStore();
			app.Files = assetReader;
			return app;
		}
	}
}
