namespace FsImplementation;
using Plugin;
using System.Collections.Concurrent;
using System.Threading;

public class FileLocker
{
    private ConcurrentDictionary<string, SemaphoreSlim> _dict;
    private Notificator _notificator;
    public FileLocker(Notificator notificator)
    {
        _dict = new ConcurrentDictionary<string, SemaphoreSlim>();
        _notificator = notificator;
    }

    public async Task Opened(FileSystem.IrnFile file)
    {
        _notificator.Notify(new FileSystem.NotifyFileOpened { File = file });
        var semaphore = _dict.GetOrAdd(file.RelativePath(), (_relativePath) => new SemaphoreSlim(1));
        await semaphore.WaitAsync();
    }

    public void Closed(FileSystem.IrnFile file)
    {
        SemaphoreSlim? semaphore;
        var removed = _dict.TryRemove(file.RelativePath(), out semaphore);
        if (removed)
        {
            semaphore?.Release();
            _notificator.Notify(new FileSystem.NotifyFileClosed { File = file });
        }
    }
    public bool IsBusy(FileSystem.IrnFile file) => _dict.ContainsKey(file.RelativePath());

}