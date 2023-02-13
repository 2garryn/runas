using AsyncKeyedLock;
namespace FsImplementation;
using Plugin;
using System.Collections.Concurrent;
using System.Threading;

public class FileLocker
{
    private Notificator _notificator;    
    private readonly AsyncKeyedLocker<string> _asyncKeyedLocker;

    public FileLocker(Notificator notificator)
    {
        _asyncKeyedLocker = new(o =>
        {
            o.PoolSize = 20;
            o.PoolInitialFill = 1;
        });
        _notificator = notificator;
    }

    public async Task Opened(FileSystem.IrnFile file)
    {
        _notificator.Notify(new FileSystem.NotifyOpened { File = file });
        var semaphore = _asyncKeyedLocker.GetOrAdd(file.RelativePath());
        await semaphore.SemaphoreSlim.WaitAsync();
    }

    public void Closed(FileSystem.IrnFile file)
    {
        if (_asyncKeyedLocker.Index.TryGetValue(file.RelativePath(), out var semaphore))
        {
            semaphore.Dispose();
        }
        _notificator.Notify(new FileSystem.NotifyClosed { File = file });
    }

    public bool IsBusy(FileSystem.IrnFile file) => _asyncKeyedLocker.IsInUse(file.RelativePath());

}