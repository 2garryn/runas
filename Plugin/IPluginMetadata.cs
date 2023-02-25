namespace Plugin;

public interface IPluginMetadata
{
    public string PluginId { get; }

    public bool FullAccess { get; }
};