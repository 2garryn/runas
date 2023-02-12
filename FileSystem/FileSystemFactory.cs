namespace FsImplementation;
using Plugin;

public class FileSystemFactory {
    private string _rootDir;
    private FileLocker _fileLocker;
    private Notificator _notificator;

    public FileSystemFactory(string rootDir) {
        _rootDir = rootDir;
        _notificator = new Notificator();
        _fileLocker = new FileLocker(_notificator);


    }

    public FileSystemImpl New(IrnPlugin plugin, IPluginMetadata metadata) {
        return new FileSystemImpl(_rootDir, metadata.PluginId, _fileLocker, _notificator);
    }

}