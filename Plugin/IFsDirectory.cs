namespace Plugin;

public interface IFsDirectory
{
    public bool Exists();
    public string RelativePath();
    public string RawPath();
    public IFsDirectory AppendDirectory(string relativePath);
    public IFsFile AppendFile(string fileName);
    public void SetAttrs(params FsAttr[] attr);
    public IEnumerable<FsAttr> GetAllAttrs();

}