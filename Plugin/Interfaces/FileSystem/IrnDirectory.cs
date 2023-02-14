namespace FileSystem;

public interface IrnDirectory
{
    public bool Exists();
    public string RelativePath();
    public string RawPath();
    public IrnDirectory AppendDirectory(string relativePath);
    public IrnFile AppendFile(string fileName);
    public void SetOwnAttrs(Attr[] attr);
    public IEnumerable<Attr> GetOwnAttrs();
    public IEnumerable<Attr> GetAllAttrs();

}