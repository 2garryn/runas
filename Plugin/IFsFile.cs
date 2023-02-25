namespace Plugin;
using System.IO;

public interface IFsFile
{
    public string RelativePath();
    public string RawPath();
    public bool Exists();
    public Task<FileStream> CreateFileStream(FileMode fileMode);
    public Task<StreamReader> CreateStreamReader();
    public Task<StreamWriter> CreateStreamWriter();
    public bool IsBusy();
    public void SetAttrs(params FsAttr[] attr);
    public IEnumerable<FsAttr> GetAllAttrs();
}