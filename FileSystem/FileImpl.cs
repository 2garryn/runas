using Plugin;
using System.IO;


public class FileImpl : FileSystem.IrnFile
{
    public readonly string _relPath;
    public readonly string _rawPath;
    private Action<FileSystem.IrnFile> _notifyUpdated;
    internal FileImpl(string relPath, string rawPath, Action<FileSystem.IrnFile> notifyUpdated)
    {
        _relPath = relPath;
        _rawPath = rawPath;
        _notifyUpdated = notifyUpdated;
    }


    public bool Exists() => File.Exists(RawPath());
    public void NotifyUpdated() => _notifyUpdated(this);
    public string RelativePath() => _relPath;
    public string RawPath() => _rawPath;
    public FileStream CreateFileStream(FileMode fileMode) => new FileStream(_rawPath, fileMode);
    public StreamReader CreateStreamReader() => new StreamReader(_rawPath);
    public StreamWriter CreateStreamWriter() => new StreamWriter(_rawPath);
    public TextReader CreateTextReader() => File.OpenText(_rawPath);
    public TextWriter CreateTextWriter() => File.CreateText(_rawPath);
} 