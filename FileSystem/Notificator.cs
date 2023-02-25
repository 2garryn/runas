namespace FsImplementation;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using Plugin;

public class Notificator
{
    private SubscribersTree _tree;
    public Notificator()
    {
        _tree = new SubscribersTree();
    }
    public void Subscribe(IFsDirectory directory, IFsNotifySubscriber subscriber) => _tree.AddSubscriber(directory, subscriber);
    public void Unsubscribe(IFsDirectory directory, IFsNotifySubscriber subscriber) => _tree.RemoveSubscriber(directory, subscriber);
    public void Notify(IFsNotify notify) => _tree.Notify(notify);
}



public class SubscribersTree
{
    private SubscribersTreeElem _tree;
    private ReaderWriterLock _rwl = new ReaderWriterLock();
    public SubscribersTree()
    {
        _tree = new SubscribersTreeElem
        {
            DirName = "/",
            Subscribers = new HashSet<IFsNotifySubscriber>(),
            SubDirs = new Dictionary<string, SubscribersTreeElem>()
        };
    }
    public bool AddSubscriber(IFsDirectory directory, IFsNotifySubscriber subscriber)
    {
        if (!directory.Exists())
        {
            return false;
        }
        string[] directories = directory.RelativePath().Split(Path.DirectorySeparatorChar);
        _rwl.AcquireWriterLock(10000);
        var currentElem = _tree;
        foreach (string currentPathName in directories)
        {
            if (currentElem.SubDirs.ContainsKey(currentPathName))
            {
                currentElem = currentElem.SubDirs[currentPathName];
            }
            else
            {
                var newElem = new SubscribersTreeElem
                {
                    DirName = currentPathName,
                    Subscribers = new HashSet<IFsNotifySubscriber>(),
                    SubDirs = new Dictionary<string, SubscribersTreeElem>()
                };
                currentElem.SubDirs[currentPathName] = newElem;
                currentElem = newElem;
            }
        }
        currentElem.Subscribers.Add(subscriber);
        _rwl.ReleaseWriterLock();
        return true;
    }
    public void RemoveSubscriber(IFsDirectory directory, IFsNotifySubscriber subscriber)
    {

    }

    public void Notify(IFsNotify notify)
    {
        string[] directories = notify.File.RelativePath().Split(Path.DirectorySeparatorChar);
        var subscribers = new List<IFsNotifySubscriber>();
        _rwl.AcquireReaderLock(10000);
        var currentElem = _tree;
        foreach (string currentPathName in directories)
        {
            subscribers.AddRange(currentElem.Subscribers);
            if (currentElem.SubDirs.ContainsKey(currentPathName))
            {
                currentElem = currentElem.SubDirs[currentPathName];
            }
            else
            {
                break;
            }
        }
        foreach (IFsNotifySubscriber subscriber in subscribers)
        {
            subscriber.Notify(notify);
        }
        _rwl.ReleaseReaderLock();
    }
}


public class SubscribersTreeElem
{
    public required string DirName { get; init; }
    public required HashSet<IFsNotifySubscriber> Subscribers { get; init; }
    public required Dictionary<string, SubscribersTreeElem> SubDirs { get; init; }
}