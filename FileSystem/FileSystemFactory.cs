namespace FsImplementation;
using Plugin;

public class FileSystemFactory
{
    private string _rootDir;
    private FileLocker _fileLocker;
    private Notificator _notificator;
    private AttrStorage _attrStorage;

    public FileSystemFactory(string rootDir, AttrStorage attrStorage)
    {
        _rootDir = rootDir;
        _notificator = new Notificator();
        _fileLocker = new FileLocker(_notificator);
        _attrStorage = attrStorage;

    }

    public FileSystemImpl New(IPlugin plugin, IPluginMetadata metadata)
    {
        return new FileSystemImpl(_rootDir, metadata.PluginId, _fileLocker, _notificator, _attrStorage);
    }

}