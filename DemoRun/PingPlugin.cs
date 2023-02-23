using Common;
using Plugin;

public class PingPlugin : IrnPlugin
{
    public void Registered(IrnServiceRegistry serviceRegistry)
    {
        var cmds = serviceRegistry.GetCommands();
        var prs = new List<Commands.CommandParameter>()
        {
            new Commands.CommandParameter{Name = "response", Description = "Ping Response", DefaultValue = "pong", Required = false},
        };
        cmds.Command("ping", "Simple ping for plugin", Pong, prs);
        cmds.Command("current_time", "Get Current Server Time", CurrentTime);
    }

    public async Task Pong(Dictionary<string, string> parameters, Commands.IrnContext context)
        => await context.SendStringAsync($"Current Time: {parameters["response"]}\n");

    public async Task CurrentTime(Dictionary<string, string> parameters, Commands.IrnContext context)
        => await context.SendStringAsync(DateTime.UtcNow.ToString());
}

public class PingMetadata : IPluginMetadata
{
    public string PluginId { get => "ping"; }
    public bool FullAccess { get => false; }
}