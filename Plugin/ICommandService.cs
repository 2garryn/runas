namespace Plugin;

public interface ICommandService
{
    //    public void Command(string method, string description, Func<Dictionary<string, string>, IrnContext, Task> callback);
    public void Command(string method, string description, Func<Dictionary<string, string>, ICommandContext, Task> callback, IEnumerable<CommandParameter>? parameters = null);
    public void CommandUploadFiles(Func<Dictionary<string, string>, ICommandContext, IEnumerable<ICommandFile>, Task> callback, 
        IEnumerable<CommandParameter>? parameters = null);
}

public record CommandParameter
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string DefaultValue { get; init; }
    public required bool Required { get; init; }
}

public interface ICommandFile
{
    public Stream Data { get; }
    //
    // Summary:
    //     Gets the file name.
    public string FileName { get; }
    //
    // Summary:
    //     Gets the name.
    public string Name { get; }
    //
    // Summary:
    //     Gets the content-type. Defaults to text/plain if unspecified.
    public string ContentType { get; }
}