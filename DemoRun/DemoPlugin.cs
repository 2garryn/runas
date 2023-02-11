
using Common;
using Plugin;

public class DemoPlugin : IrnPlugin
{
    public void Registered(IrnServiceRegistry serviceRegistry)
    {
        var fs = serviceRegistry.GetFileSystem();
        if (fs == null) {
            throw new NullReferenceException("asdasd");
        }
        Console.WriteLine("Rootpath rawpath: " + fs.RootDirectory().RawPath());
        Console.WriteLine("Rootpath relapath: " + fs.RootDirectory().RelativePath());
        Console.WriteLine(" --------------------------------------- ");
        Console.WriteLine("pluginpath rawpath: " + fs.PluginDirectory().RawPath());
        Console.WriteLine("pluginpath relapath: " + fs.PluginDirectory().RelativePath());
        Console.WriteLine(" --------------------------------------- ");
        var tempDir = fs.PluginDirectory().AppendDirectory("mydirectory");
        var tempDir2 = fs.PluginDirectory().AppendDirectory("mydirectory2");
        fs.CreateDirectory(tempDir2);
        Console.WriteLine("mydirectory rawpath: " + tempDir.RawPath());
        Console.WriteLine("mydirectory relapath: " + tempDir.RelativePath());
        Console.WriteLine($"mydirectory exist: {tempDir.Exists()}");
        fs.CreateDirectory(tempDir);
        Console.WriteLine($"mydirectory exist: {tempDir.Exists()}");


        foreach(FileSystem.IrnDirectory dir in fs.ListDirectories(fs.PluginDirectory()))
        {
            Console.WriteLine(dir.RawPath());
            Console.WriteLine(dir.RelativePath());
            Console.WriteLine($"{dir.Exists()}");
            Console.WriteLine(" ***** ");
        }
        var tempFile = fs.PluginDirectory().AppendFile("testfile.txt");
        var alreadyExist = fs.Createfile(tempFile);
        Console.WriteLine($"File already exist: {alreadyExist}, exist: {tempFile.Exists()}");


    }
}


public class DemoMetadata : IPluginMetadata
{
    public string PluginId { get => "demo_plugin"; }
    public bool FullAccess { get => false; }
}