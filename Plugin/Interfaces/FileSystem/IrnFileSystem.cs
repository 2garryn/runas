namespace FileSystem;
public interface IrnFileSystem
{
    public IrnDirectory RootDirectory();
    public IrnDirectory PluginDirectory();
    public bool CreateDirectory(IrnDirectory directory);
    public IEnumerable<IrnDirectory> ListDirectories(IrnDirectory directory);
    public IEnumerable<IrnFile> ListFiles(IrnDirectory directory);
    public bool Createfile(IrnFile file);
    public void Subscribe(IrnDirectory directory, IFsNotifySubscriber subscriber);
    public void Unsubscribe(IrnDirectory directory, IFsNotifySubscriber subscriber);
}