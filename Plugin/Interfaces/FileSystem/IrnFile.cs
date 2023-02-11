namespace FileSystem;
using System.IO;

public interface IrnFile {
    public string RelativePath();
    public string RawPath();
    public bool Exists();
    public void NotifyUpdated();
    public FileStream CreateFileStream(FileMode fileMode);
    public StreamReader CreateStreamReader();
    public StreamWriter CreateStreamWriter();
    public TextReader CreateTextReader();
    public TextWriter CreateTextWriter();
}