namespace RunasLib;

using Common;
using Plugin;
using Commands;

public class ServiceRegisry : IrnServiceRegistry
{
    private readonly FileSystem.IrnFileSystem _fileSystem;
    private readonly DocumentDb.IrnDocumentDb _documentDb;
    private readonly Commands.IrnCommandService _commands;
    public ServiceRegisry(FileSystem.IrnFileSystem fileSystem, DocumentDb.IrnDocumentDb documentDb, Commands.IrnCommandService commands)
    {
        _fileSystem = fileSystem;
        _documentDb = documentDb;
        _commands = commands;
    }
    public FileSystem.IrnFileSystem GetFileSystem() => _fileSystem;
    public DocumentDb.IrnDocumentDb GetDocumentDb() => _documentDb;
    public IrnCommandService GetCommands() => _commands;
}