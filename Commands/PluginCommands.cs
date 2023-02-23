namespace Commands;

using System.IO;
using System.Collections.Immutable;
using Plugin;

public class PluginCommands : Commands.IrnCommandService
{
    private Dictionary<string, CommandInternal> _commands;

    public PluginCommands()
    {
        _commands = new Dictionary<string, CommandInternal>();
    }

    public void Command(string name, string description, Func<Dictionary<string, string>, Commands.IrnContext, Task> callback, IEnumerable<Commands.CommandParameter>? parameters = null)
    {
        var parameters1 = parameters == null ? new List<Commands.CommandParameter>() : parameters;
        _commands[name] = new CommandInternal
        {
            Name = name,
            Description = description,
            Parameters = parameters1.ToDictionary(p => p.Name, p => p),
            Callback = callback
        };
    }

    public void CommandUploadFile(string method, string description, Func<Dictionary<string, string>, Commands.IrnContext, StreamReader, Task> callback, IEnumerable<CommandParameter>? parameters = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableDictionary<string, CommandInternal> GetCommands() => _commands.ToImmutableDictionary();
}



public record CommandInternal
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Dictionary<string, CommandParameter> Parameters { get; init; }
    public required Func<Dictionary<string, string>, Commands.IrnContext, Task> Callback { get; init; }


}