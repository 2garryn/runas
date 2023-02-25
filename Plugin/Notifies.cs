namespace Plugin;
public record NotifyFileOpened : IFsNotify
{
    public required IFsFile File { get; init; }
}

public record NotifyFileClosed : IFsNotify
{
    public required IFsFile File { get; init; }
}

public record NotifyFileCreated : IFsNotify
{
    public required IFsFile File { get; init; }
}

public record NotifyFileRemoved : IFsNotify
{
    public required IFsFile File { get; init; }
}

public interface IFsNotify
{
    public IFsFile File { get; init; }
}

public interface IFsNotifySubscriber
{
    public void Notify(IFsNotify notify);
}