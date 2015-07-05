using Grasshopper.Platform;

namespace Grasshopper.WindowsFileSystem
{
    public class FileStore : IFileStore
    {
        public IFileSource GetFile(string path)
        {
            return new FileSource(path);
        }
    }
}
