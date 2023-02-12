using AsyncKeyedLock;

public class FileNotifierList
{
    private readonly AsyncKeyedLocker<string> _asyncKeyedLocker;
    private readonly IFsNotifyCatcher _catcher;
    public FileNotifierList(IFsNotifyCatcher catcher) 
    {
        _asyncKeyedLocker = new(o =>
        {
            o.PoolSize = 20;
            o.PoolInitialFill = 1;
        });
        _catcher = catcher;
    }

    public async Task Opened(FileSystem.IrnFile file)
    {
        _catcher.Notify(new NotifyOpened{File = file});
        var semaphore = _asyncKeyedLocker.GetOrAdd(file.RelativePath());
        await semaphore.SemaphoreSlim.WaitAsync();
    }

    public void Closed(FileSystem.IrnFile file)
    {
        if (_asyncKeyedLocker.Index.TryGetValue(file.RelativePath(), out var semaphore))
        {
            semaphore.Dispose();
        }
        _catcher.Notify(new NotifyClosed{File = file});
    }

    public bool IsBusy(FileSystem.IrnFile file) => _asyncKeyedLocker.IsInUse(file.RelativePath());

}