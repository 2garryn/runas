namespace FileSystem;
public record NotifyFileOpened : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public record NotifyFileClosed : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public record NotifyFileCreated : IFsNotify
{
    public required FileSystem.IrnFile File { get; init; }
}

public record NotifyFileRemoved : IFsNotify
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