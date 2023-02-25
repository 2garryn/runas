namespace Plugin;
public interface IFsFileSystem
{
    public IFsDirectory RootDirectory();
    public IFsDirectory PluginDirectory();
    public bool CreateDirectory(IFsDirectory directory);
    public IEnumerable<IFsDirectory> ListDirectories(IFsDirectory directory);
    public IEnumerable<IFsFile> ListFiles(IFsDirectory directory);
    public bool Createfile(IFsFile file);
    public void Subscribe(IFsDirectory directory, IFsNotifySubscriber subscriber);
    public void Unsubscribe(IFsDirectory directory, IFsNotifySubscriber subscriber);
}