
namespace Plugin;



public interface IServiceRegistry
{
    public IFsFileSystem GetFileSystem();
    public IDocumentDb GetDocumentDb();
    public ICommandService GetCommands();

}