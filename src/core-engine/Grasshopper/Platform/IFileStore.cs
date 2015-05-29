namespace Grasshopper.Platform
{
	public interface IFileStore
	{
		IFileSource GetFile(string path);
	}
}
