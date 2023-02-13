namespace FileSystem;
public record NotifyOpened : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public record NotifyClosed : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public record NotifyCreated : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public record NotifyRemoved : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public interface IFsNotify
{
    public FileSystem.IrnFile File { get; init; }
}

public interface IFsNotifySubscriber
{
    public void Notify(IFsNotify notify);
}