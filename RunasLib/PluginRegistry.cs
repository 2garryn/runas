namespace RunasLib;


using Plugin;
using FsImplementation;
using DocumentDb;
using Commands;

public class PluginRegistry
{
    private FileSystemFactory _fileSystemFactory;
    private DocumentDbFactory _documentDbFactory;
    private Commands.PluginRegistry _pluginRegistry;
    public PluginRegistry(FileSystemFactory fileSystemFactory, DocumentDbFactory documentDbFactory, Commands.PluginRegistry pluginRegistry)
    {
        _fileSystemFactory = fileSystemFactory;
        _documentDbFactory = documentDbFactory;
        _pluginRegistry = pluginRegistry;

    }

    public void RegisterPlugin(IPlugin plugin, IPluginMetadata metadata)
    {
        var fs = _fileSystemFactory.New(plugin, metadata);
        var docDb = _documentDbFactory.New(metadata.PluginId);
        var commandServ = _pluginRegistry.AddPlugin(metadata.PluginId);
        var registry = new ServiceRegisry(fs, docDb, commandServ);
        plugin.Registered(registry);

    }
}
