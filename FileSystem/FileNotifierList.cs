using Plugin;
using System.Collections.Concurrent;
using System.Threading;

public class FileNotifierList
{
    private ConcurrentDictionary<string, SemaphoreSlim> _dict;
    private IFsNotifyCatcher _catcher;
    public FileNotifierList(IFsNotifyCatcher catcher) 
    {
        _dict = new ConcurrentDictionary<string, SemaphoreSlim>();
        _catcher = catcher;
    }

    public async Task Opened(FileSystem.IrnFile file)
    {
        _catcher.Notify(new NotifyOpened{File = file});
        var semaphore = _dict.GetOrAdd(file.RelativePath(), (_relativePath) => new SemaphoreSlim(1));
        await semaphore.WaitAsync();
    }

    public void Closed(FileSystem.IrnFile file)
    {
        SemaphoreSlim? semaphore;
        var removed = _dict.TryRemove(file.RelativePath(), out semaphore);
        semaphore?.Release();
        _catcher.Notify(new NotifyClosed{File = file});


    }

    public bool IsBusy(FileSystem.IrnFile file) => _dict.ContainsKey(file.RelativePath());

}