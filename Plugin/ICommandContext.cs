namespace Plugin;


public interface ICommandContext
{
    public Task SendStringAsync(string content);
    public Task SendDataAsync(object data);
    public Stream OpenResponseStream();
}