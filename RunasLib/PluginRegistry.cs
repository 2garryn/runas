namespace RunasLib;


using Plugin;
using FsImplementation;
using DocumentDb;

public class PluginRegistry
{
    private FileSystemFactory _fileSystemFactory;
    private DocumentDbFactory _documentDbFactory;
    public PluginRegistry(FileSystemFactory fileSystemFactory, DocumentDbFactory documentDbFactory)
    {
        _fileSystemFactory = fileSystemFactory;
        _documentDbFactory = documentDbFactory;

    }

    public void RegisterPlugin(IrnPlugin plugin, IPluginMetadata metadata)
    {
        var fs = _fileSystemFactory.New(plugin, metadata);
        var docDb = _documentDbFactory.New(metadata.PluginId);
        var registry = new ServiceRegisry(fs, docDb);
        plugin.Registered(registry);

    }
}
