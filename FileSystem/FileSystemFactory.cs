
using Plugin;

public class FileSystemFactory {
    private string _rootDir;
    private Action<FileSystem.IrnFile> _notifyUpdated;

    public FileSystemFactory(string rootDir, Action<FileSystem.IrnFile> notifyUpdated) {
        _rootDir = rootDir;
        _notifyUpdated = notifyUpdated;
    }

    public FileSystemImpl New(IrnPlugin plugin, IPluginMetadata metadata) {
        return new FileSystemImpl(_rootDir, metadata.PluginId, _notifyUpdated);
    }

}