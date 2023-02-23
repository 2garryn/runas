namespace Commands;



public interface IrnCommandService
{
    //    public void Command(string method, string description, Func<Dictionary<string, string>, IrnContext, Task> callback);
    public void Command(string method, string description, Func<Dictionary<string, string>, IrnContext, Task> callback, IEnumerable<CommandParameter>? parameters = null);
    public void CommandUploadFile(string method, string description, Func<Dictionary<string, string>, IrnContext, StreamReader, Task> callback, IEnumerable<CommandParameter>? parameters = null);
}

public record CommandParameter
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string DefaultValue { get; init; }
    public required bool Required { get; init; }
}