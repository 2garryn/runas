namespace RunasLib;


using Plugin;
using FsImplementation;

public class PluginRegistry
{
    private FileSystemFactory _fileSystemFactory;
    public PluginRegistry(FileSystemFactory fileSystemFactory)
    {
        _fileSystemFactory = fileSystemFactory;

    }

    public void RegisterPlugin(IrnPlugin plugin, IPluginMetadata metadata)
    {
        var fs = _fileSystemFactory.New(plugin, metadata);
        var registry = new ServiceRegisry(fs);
        plugin.Registered(registry);

    }
}
