namespace FileSystem;

public interface IrnDirectory {
    public bool Exists();
    public string Path();
    public string Name();
    public string RawPath();
}