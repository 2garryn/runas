namespace FileSystem;

public interface IrnFile {
    public string Path();
    public string RawPath();
    public bool Exists();
}