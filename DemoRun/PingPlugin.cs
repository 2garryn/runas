using Common;
using Plugin;
using System.Text;

public class PingPlugin : IrnPlugin
{
    private DocumentDb.IrnDocumentDb _docDb;
    public void Registered(IrnServiceRegistry serviceRegistry)
    {
        _docDb = serviceRegistry.GetDocumentDb();
        var cmds = serviceRegistry.GetCommands();
        var prs = new List<Commands.CommandParameter>()
        {
            new Commands.CommandParameter{Name = "response", Description = "Ping Response", DefaultValue = "pong", Required = false},
        };
        cmds.Command("ping", "Simple ping for plugin", Pong, prs);
        cmds.Command("current_time", "Get Current Server Time", CurrentTime);

        var setValParams = new List<Commands.CommandParameter>()
        {
            new Commands.CommandParameter{Name = "val1", Description = "Value 1", DefaultValue = "", Required = true},
            new Commands.CommandParameter{Name = "val2", Description = "Value 2", DefaultValue = "", Required = true},
        };
        cmds.Command("set_values", "Set Test Values", SetTestValues, setValParams);
        cmds.Command("get_values", "Get Test Values", GetTestValues);
        cmds.Command("delete_values", "Delete Test Values", DeleteTestValues);
    }

    public async Task Pong(Dictionary<string, string> parameters, Commands.IrnContext context)
        => await context.SendStringAsync($"{parameters["response"]}\n");

    public async Task CurrentTime(Dictionary<string, string> parameters, Commands.IrnContext context)
        => await context.SendStringAsync(DateTime.UtcNow.ToString());

    public async Task SetTestValues(Dictionary<string, string> parameters, Commands.IrnContext context)
    {
        var col = _docDb.GetCollection<Values>("test_values");
        parameters.ToList().ForEach(kvp => col.Upsert(new Values{Id = kvp.Key, Value = kvp.Value}));
        await context.SendStringAsync($"ok\n");
    }
    public async Task GetTestValues(Dictionary<string, string> parameters, Commands.IrnContext context)
    {
        var strb = new StringBuilder();
        _docDb.GetCollection<Values>("test_values")
            .Query()
            .ToList()
            .ForEach(v => strb.Append($"{v.Id} = {v.Value}\n"));
        await context.SendStringAsync(strb.ToString());
    }
    public async Task DeleteTestValues(Dictionary<string, string> parameters, Commands.IrnContext context)
        => _docDb.GetCollection<Values>("test_values").DeleteAll();
    
}

public class PingMetadata : IPluginMetadata
{
    public string PluginId { get => "ping"; }
    public bool FullAccess { get => false; }
}

public class Values 
{
    public string Id {get;set;}
    public string Value {get;set;}
}