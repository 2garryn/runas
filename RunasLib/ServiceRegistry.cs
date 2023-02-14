namespace RunasLib;

using Common;
using Plugin;

public class ServiceRegisry : IrnServiceRegistry
{
    private readonly FileSystem.IrnFileSystem _fileSystem;
    private readonly DocumentDb.IrbDocumentDb _documentDb;
    public ServiceRegisry(FileSystem.IrnFileSystem fileSystem, DocumentDb.IrbDocumentDb documentDb)
    {
        _fileSystem = fileSystem;
        _documentDb = documentDb;
    }
    public FileSystem.IrnFileSystem GetFileSystem() => _fileSystem;
    public DocumentDb.IrbDocumentDb GetDocumentDb() => _documentDb;
}