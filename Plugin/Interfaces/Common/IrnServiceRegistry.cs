
using FileSystem;
using Commands;
namespace Common;
using DocumentDb;


public interface IrnServiceRegistry
{
    public IrnFileSystem GetFileSystem();
    public IrnDocumentDb GetDocumentDb();
    public IrnCommandService GetCommands();

}