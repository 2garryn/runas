namespace Commands;


public class PluginRegistry
{
    private Dictionary<string, PluginCommands> _plugins;

    public PluginRegistry()
    {
        _plugins = new Dictionary<string, PluginCommands>();
    }

    public PluginCommands AddPlugin(string pluginId)
    {
        var commands = new PluginCommands();
        _plugins[pluginId] = commands;
        Console.WriteLine($"Plugin added: {pluginId}");
        return commands;
    }

    public Dictionary<string, PluginCommands> GetPlugins() => _plugins;
}