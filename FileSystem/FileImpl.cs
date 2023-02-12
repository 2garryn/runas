namespace FsImplementation;
using Plugin;
using System.IO;

public class FileImpl : FileSystem.IrnFile
{
    public readonly string _relPath;
    public readonly string _rawPath;
    private FileLocker _notifierList;
    internal FileImpl(string relPath, string rawPath, FileLocker notifierList)
    {
        _relPath = relPath;
        _rawPath = rawPath;
        _notifierList = notifierList;
    }


    public bool Exists() => File.Exists(RawPath());
    public string RelativePath() => _relPath;
    public string RawPath() => _rawPath;
    public async Task<FileStream> CreateFileStream(FileMode fileMode)
    {
        await _notifierList.Opened(this);
        return new RnFileStream(this, fileMode);
    }
    public async Task<StreamReader> CreateStreamReader()
    {
        await _notifierList.Opened(this);
        return new RnStreamReader(this);
    }
    public async Task<StreamWriter> CreateStreamWriter()
    {
        await _notifierList.Opened(this);
        return new RnStreamWriter(this);
    }

    public bool IsBusy() => _notifierList.IsBusy(this);
    internal void Closed() => _notifierList.Closed(this);

}


public class RnFileStream : FileStream
{
    private FileImpl _fileImpl;
    public RnFileStream(FileImpl fimple, FileMode options) : base(fimple._rawPath, options)
    {
        _fileImpl = fimple;
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _fileImpl.Closed();
    }
    public override async System.Threading.Tasks.ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        _fileImpl.Closed();
    }
}

public class RnStreamReader : StreamReader
{
    private FileImpl _fileImpl;
    public RnStreamReader(FileImpl fileImpl) : base(fileImpl._rawPath)
    {
        _fileImpl = fileImpl;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _fileImpl.Closed();
    }
}

public class RnStreamWriter : StreamWriter
{
    private FileImpl _fileImpl;
    public RnStreamWriter(FileImpl fileImpl) : base(fileImpl._rawPath)
    {
        _fileImpl = fileImpl;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _fileImpl.Closed();
    }
}
