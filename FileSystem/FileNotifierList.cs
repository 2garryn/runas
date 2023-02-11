using Plugin;

public class FileNotifierList
{
    public async Task Opened(FileSystem.IrnFile file)
    {
        Console.WriteLine($"File Opened: {file.RawPath()}");
    }

    public void Closed(FileSystem.IrnFile file)
    {
        Console.WriteLine($"File Closed: {file.RawPath()}");
    }

    public bool IsBusy(FileSystem.IrnFile file)
    {
        return false;
    }

    public async Task WaitFree() 
    {
        await Task.Delay(1000);
    }
}