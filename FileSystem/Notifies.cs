using Plugin;


public record NotifyOpened : IFsNotify
{
    public required FileSystem.IrnFile File {get; init;}
}

public record NotifyClosed : IFsNotify
{
    public required FileSystem.IrnFile File {get; init;}
}


public interface IFsNotify
{
    public FileSystem.IrnFile File {get; init;}
}

public interface IFsNotifyCatcher
{
    public void Notify(IFsNotify notify);
}