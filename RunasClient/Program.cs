// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using System.Text.Json;
class Program
{
    static async Task Main(string[] args)
    {
        var url = "http://localhost:8089";
        // Display the number of command line arguments.
        Console.WriteLine(args.Length);
        if (args.Count() == 0) {
            await AllCommands(url);
        }


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
}



public class AllCommandsView
{
    public List<PluginView>? plugins {get;set;}
}

public class PluginView 
{
    public string? plugin_id {get;set;}
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