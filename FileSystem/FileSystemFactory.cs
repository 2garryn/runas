
using Plugin;

public class FileSystemFactory {
    private string _rootDir;
    private FileNotifierList _notifierList;

    public FileSystemFactory(string rootDir) {
        _rootDir = rootDir;
        _notifierList = new FileNotifierList(null);
    }

    public FileSystemImpl New(IrnPlugin plugin, IPluginMetadata metadata) {
        return new FileSystemImpl(_rootDir, metadata.PluginId, _notifierList);
    }

}