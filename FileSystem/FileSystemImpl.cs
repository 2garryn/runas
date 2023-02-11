
using FileSystem;
using Plugin;
using System.IO;



public class FileSystemImpl : FileSystem.IrnFileSystem
{

    private readonly string _rootDir;
    private readonly string _pluginId;
    private FileNotifierList _notifierList;

    public FileSystemImpl(string rootDir, string pluginId, FileNotifierList notifierList) 
    {
        _rootDir = rootDir;
        _pluginId = pluginId;
        _notifierList = notifierList;
        CreatePluginDir();
    }

    private void CreatePluginDir()
    {
        CreateDirectory(PluginDirectory());
    }

    public bool CreateDirectory(IrnDirectory directory)
    {
        if (directory.Exists()) {
            return false;
        };
        Directory.CreateDirectory(directory.RawPath());
        return true;

    }

    public bool Createfile(IrnFile file)
    {
        if (!File.Exists(file.RawPath()))
        {
            var fs = File.Create(file.RawPath());
            fs.Close();
            return false;
        }
        return true;
    }

    public IEnumerable<IrnDirectory> ListDirectories(IrnDirectory directory)
    {
        string[] dirs = Directory.GetDirectories(directory.RawPath(), "*", SearchOption.TopDirectoryOnly);
        return dirs.Select((dir) => {
            var relDir = Path.Join(directory.RelativePath(), Path.GetFileName(Path.GetDirectoryName(dir)));
            return (IrnDirectory)(new DirectoryImpl(relDir, dir, _notifierList));
        });
    }

    public IEnumerable<IrnFile> ListFiles(IrnDirectory directory)
    {
        throw new NotImplementedException();
    }

    public IrnDirectory PluginDirectory() 
    {
        var relPath = Path.Join("/", "plugins", _pluginId);
        var rawPath = Path.Join(_rootDir, relPath);
        return new DirectoryImpl(relPath, rawPath, _notifierList);
    }


    public IrnDirectory RootDirectory() => new DirectoryImpl("/", _rootDir, _notifierList);
}