﻿namespace Commands;
using System;
using EmbedIO;
using EmbedIO.WebApi;
using EmbedIO.Actions;
using System.Text;
using Plugin;
public class CommandService
{
    private string _url;
    private string _prefix;
    private PluginRegistry _pluginRegistry;
    public CommandService(string url, string prefix)
    {
        _url = url;
        _prefix = prefix;
        _pluginRegistry = new PluginRegistry();
    }

    public PluginRegistry GetPluginRegistry() => _pluginRegistry;

    public async Task RunAsync()
    {
        using (var server = CreateWebServer())
        {
            await server.RunAsync();
        }
    }


    private WebServer CreateWebServer()
    {
        return new WebServer(o => o
            .WithUrlPrefix(_url)
            .WithMode(HttpListenerMode.EmbedIO))
            .WithModule(new ActionModule("/command", HttpVerbs.Post, this.CommandPage))
            .WithModule(new ActionModule("/all_commands", HttpVerbs.Post, this.AllPage));

    }

    private async Task AllPage(IHttpContext context)
    {
       var query = context.GetRequestQueryData();
       if(query["format"] == "text") 
       {
            await AllText(context);
       } 
       else 
       {
            await AllJson(context);
       }
    }
    private async Task AllJson(IHttpContext context)
    {
        await context.SendDataAsync(new
        {
            plugins = _pluginRegistry.GetPlugins().Select(pl => new 
            {
                plugin_id = pl.Key,
                commands = pl.Value.GetCommands().Select(c => new
                {
                    name = c.Value.Name,
                    description = c.Value.Description,
                    parameters = c.Value.Parameters.Select(p => new
                    {
                        name = p.Key,
                        description = p.Value.Description,
                        required = p.Value.Required,
                        default_value = p.Value.DefaultValue
                    })
                })
            })
        });
    }
    private async Task AllText(IHttpContext context)
    {
        var sb = new StringBuilder("PLUGINS\n");
        var plugins = _pluginRegistry.GetPlugins();
        foreach (var plugin in plugins)
        {
            sb.AppendLine($" {plugin.Key}");
            sb.AppendLine($"  Commands:");
            foreach (var commands in plugin.Value.GetCommands())
            {
                sb.AppendLine($"    {commands.Value.Name} - {commands.Value.Description}");
                foreach (var parameter in commands.Value.Parameters)
                {
                    sb.AppendLine($"      {parameter.Key} | Required: {parameter.Value.Required} | Default Value: {parameter.Value.DefaultValue} | {parameter.Value.Description}");
                }
            }
        }
        await context.SendStringAsync(sb.ToString(), "text/plain", Encoding.UTF8);
    }

    private async Task CommandPage(IHttpContext context)
    {
        var data = await context.GetRequestDataAsync<CommandRequest>();
        PluginCommands? cmds;
        var found = _pluginRegistry.GetPlugins().TryGetValue(data.plugin, out cmds);
        if (!found)
        {
            await context.SendStringAsync("Plugin not found", "text/plain", Encoding.UTF8);
            return;
        }
        CommandInternal cmd;
        found = cmds!.GetCommands().TryGetValue(data.command, out cmd!);
        if (!found)
        {
            await context.SendStringAsync("Command not found", "text/plain", Encoding.UTF8);
            return;
        }
        if (cmd.Parameters.Count == 0)
        {
            await cmd.Callback(new Dictionary<string, string>(), new Context(context));
            return;
        }
        foreach (var paramDef in cmd.Parameters)
        {
            string? paramvalue;
            found = data.parameters.TryGetValue(paramDef.Value.Name, out paramvalue);
            if (!found)
            {
                if (paramDef.Value.Required)
                {
                    await context.SendStringAsync($"Parameter is required: {paramDef.Value.Name}", "text/plain", Encoding.UTF8);
                    return;
                }
                data.parameters[paramDef.Value.Name] = paramDef.Value.DefaultValue;
            }
        }
        await cmd.Callback(data.parameters, new Context(context));
    }
}

public class Context : Commands.IrnContext
{
    private IHttpContext _context;
    public Context(IHttpContext context)
        => _context = context;
    public async Task SendDataAsync(object data)
        => await _context.SendDataAsync(data);
    public async Task SendStringAsync(string content)
        => await _context.SendStringAsync(content, "text/plain", Encoding.UTF8);

}


public class CommandRequest
{
    public string plugin { get; set; } = "";
    public string command { get; set; } = "";
    public Dictionary<string, string> parameters { get; set; } = new Dictionary<string, string>();
}