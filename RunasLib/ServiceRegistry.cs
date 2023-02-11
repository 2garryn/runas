namespace RunasLib;

using Common;
using Plugin;

public class ServiceRegisry : IrnServiceRegistry
{
    private FileSystem.IrnFileSystem _fileSystem;
    public ServiceRegisry(FileSystem.IrnFileSystem fileSystem) {
        _fileSystem = fileSystem;
    }
    public FileSystem.IrnFileSystem  GetFileSystem() => _fileSystem;
}