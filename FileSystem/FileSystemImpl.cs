namespace FsImplementation;
using Plugin;
using System.IO;
using LiteDB;



public class FileSystemImpl : IFsFileSystem
{

    private readonly string _rootDir;
    private readonly string _pluginId;
    private FileLocker _locker;
    private Notificator _notificator;
    private AttrStorage _attrStorage;

    public FileSystemImpl(string rootDir, string pluginId, FileLocker locker, Notificator notificator, AttrStorage attrStorage)
    {
        _rootDir = rootDir;
        _pluginId = pluginId;
        _locker = locker;
        _notificator = notificator;
        _attrStorage = attrStorage;
        CreatePluginDir();
    }

    private void CreatePluginDir()
    {
        CreateDirectory(PluginDirectory());
    }

    public bool CreateDirectory(IFsDirectory directory)
    {
        if (directory.Exists())
        {
            return false;
        };
        Directory.CreateDirectory(directory.RawPath());
        return true;

    }

    public bool Createfile(IFsFile file)
    {
        if (!File.Exists(file.RawPath()))
        {
            var fs = File.Create(file.RawPath());
            fs.Close();
            _notificator.Notify(new NotifyFileCreated { File = file });
            return false;
        }
        return true;
    }

    public IEnumerable<IFsDirectory> ListDirectories(IFsDirectory directory)
    {
        string[] dirs = Directory.GetDirectories(directory.RawPath(), "*", SearchOption.TopDirectoryOnly);
        return dirs.Select((dir) =>
        {
            var relDir = Path.Join(directory.RelativePath(), Path.GetFileName(Path.GetDirectoryName(dir)));
            return (IFsDirectory)(new DirectoryImpl(relDir, dir, _locker, _attrStorage));
        });
    }

    public IEnumerable<IFsFile> ListFiles(IFsDirectory directory)
    {
        throw new NotImplementedException();
    }

    public IFsDirectory PluginDirectory()
    {
        var relPath = Path.Join("/", "plugins", _pluginId);
        var rawPath = Path.Join(_rootDir, relPath);
        return new DirectoryImpl(relPath, rawPath, _locker, _attrStorage);
    }

    public IFsDirectory RootDirectory() => new DirectoryImpl("/", _rootDir, _locker, _attrStorage);
    public void Subscribe(IFsDirectory directory, IFsNotifySubscriber subscriber) => _notificator.Subscribe(directory, subscriber);
    public void Unsubscribe(IFsDirectory directory, IFsNotifySubscriber subscriber) => _notificator.Unsubscribe(directory, subscriber);
}