namespace Commands;


public interface IrnContext
{
    public Task SendStringAsync(string content);
    public Task SendDataAsync(object data);
}