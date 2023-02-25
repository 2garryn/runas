namespace RunasLib;

using Plugin;
using Commands;

public class ServiceRegisry : IServiceRegistry
{
    private readonly IFsFileSystem _fileSystem;
    private readonly IDocumentDb _documentDb;
    private readonly ICommandService _commands;
    public ServiceRegisry(IFsFileSystem fileSystem, IDocumentDb documentDb, ICommandService commands)
    {
        _fileSystem = fileSystem;
        _documentDb = documentDb;
        _commands = commands;
    }
    public IFsFileSystem GetFileSystem() => _fileSystem;
    public IDocumentDb GetDocumentDb() => _documentDb;
    public ICommandService GetCommands() => _commands;
}