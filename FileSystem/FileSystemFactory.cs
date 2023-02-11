
using Plugin;

public class FileSystemFactory {
    private string _rootDir;

    public FileSystemFactory(string rootDir) {
        _rootDir = rootDir;
    }

    public FileSystemImpl New(IrnPlugin plugin, IPluginMetadata metadata) {
        return new FileSystemImpl(_rootDir, metadata.PluginId);
    }

}