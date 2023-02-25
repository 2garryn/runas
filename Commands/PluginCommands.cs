namespace Commands;

using System.IO;
using System.Collections.Immutable;
using Plugin;

public class PluginCommands : ICommandService
{
    private Dictionary<string, CommandInternal> _commands;

    private Func<Dictionary<string, string>, ICommandContext , IEnumerable<ICommandFile>, Task>? _uploadFIleCallback;
    private IEnumerable<CommandParameter>? _uploadFileParameters;

    public PluginCommands()
    {
        _commands = new Dictionary<string, CommandInternal>();
    }

    public void Command(string name, string description, Func<Dictionary<string, string>, ICommandContext, Task> callback, IEnumerable<CommandParameter>? parameters = null)
    {
        var parameters1 = parameters == null ? new List<CommandParameter>() : parameters;
        _commands[name] = new CommandInternal
        {
            Name = name,
            Description = description,
            Parameters = parameters1.ToDictionary(p => p.Name, p => p),
            Callback = callback
        };
    }
    public void CommandUploadFiles(Func<Dictionary<string, string>, ICommandContext , IEnumerable<ICommandFile>, Task> callback, IEnumerable<CommandParameter>? parameters = null)
    {
        if (_uploadFIleCallback != null) 
        {
            throw new InvalidOperationException("Upload file already registered");
        }
        _uploadFIleCallback = callback;
        _uploadFileParameters = parameters ?? new List<CommandParameter>();
    }
    public ImmutableDictionary<string, CommandInternal> GetCommands() 
        => _commands.ToImmutableDictionary();
    public Func<Dictionary<string, string>, ICommandContext , IEnumerable<ICommandFile>, Task>? GetUploadFilesCallback() 
        => _uploadFIleCallback;
    public IEnumerable<CommandParameter>? GetUploadFilesParameters() 
        => _uploadFileParameters;
}



public record CommandInternal
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Dictionary<string, CommandParameter> Parameters { get; init; }
    public required Func<Dictionary<string, string>, ICommandContext, Task> Callback { get; init; }


}