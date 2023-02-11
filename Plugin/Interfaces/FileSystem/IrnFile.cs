namespace FileSystem;
using System.IO;

public interface IrnFile {
    public string RelativePath();
    public string RawPath();
    public bool Exists();
    public Task<FileStream> CreateFileStream(FileMode fileMode);
    public Task<StreamReader> CreateStreamReader();
    public Task<StreamWriter> CreateStreamWriter();
    public bool IsBusy();
    public Task WaitFree();
}