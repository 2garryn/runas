// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using System.Text.Json;
class Program
{
    static async Task Main(string[] args)
    {
        var url = "http://localhost:8089";
        if (args.Count() == 0) {
            await AllCommands(url);
            return;
        }
        if (args.Count() == 1) 
        {
            Console.WriteLine("Command should be entered");
            return;
        }

        var pluginId = args[0];
        var command = args[1];
        var parameters = new List<string>();
        for(int i = 2; i < args.Count(); i++)
        {
            parameters.Add(args[i]);
        }
        await ExecuteCommand(url, pluginId, command, parameters);
    }


    static async Task AllCommands(string url)
    {
        HttpClient client = new HttpClient();
        using var result = await client.PostAsync(url + "/all_commands?format=json", null);
        var response = await result.Content.ReadFromJsonAsync<AllCommandsView>();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(response, options);
        Console.WriteLine(jsonString);
    }

    static async Task ExecuteCommand(string url, string pluginId, string command, List<string> parameters)
    {
        var data = new {
            plugin = pluginId,
            command = command,
            parameters = parameters.Select(s => 
            {
                var splitted = s.Split("=");
                return new KeyValuePair<string, string>(splitted[0].Trim(), splitted[1].Trim());
            }).ToDictionary(k => k.Key, v => v.Value)
        };
        var client = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(data), System.Text.Encoding.UTF8, "application/json");
        using var result = await client.PostAsync(url + "/command", content);
        var stream = await result.Content.ReadAsStreamAsync();
        stream.CopyTo(Console.OpenStandardOutput());
    }
}



public class AllCommandsView
{
    public List<PluginView>? plugins {get;set;}
}

public class PluginView 
{
    public string? plugin {get;set;}
    public List<CommandView>? commands {get;set;}
}

public class CommandView 
{
    public string? name {get;set;}
    public string? description {get;set;}

    public List<ParameterView>? parameters {get;set;}
}

public class ParameterView 
{
    public string? name {get;set;}
    public string? description {get;set;}
    public bool? required {get;set;}
    public string? default_value {get;set;}
}