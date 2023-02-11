using Plugin;
using System.IO;
using System;
using Plugin;
public struct DirectoryImpl : FileSystem.IrnDirectory
{
    public readonly string _relPath;
    public readonly string _rawPath;
    private Action<FileSystem.IrnFile> _notifyUpdated;
    public DirectoryImpl(string relPath, string rawPath, Action<FileSystem.IrnFile> notifyUpdated) 
    {
        _relPath = relPath ?? throw new ArgumentNullException(nameof(relPath));
        _rawPath = rawPath ?? throw new ArgumentNullException(nameof(rawPath));
        _notifyUpdated = notifyUpdated;
    }

    public bool Exists() => Directory.Exists(_rawPath);
    public string RelativePath() => _relPath;
    public string RawPath() => _rawPath;

    public FileSystem.IrnDirectory AppendDirectory(string relativePath)
    {
        var relPath = Path.Join(_relPath, relativePath);
        var rawPath = Path.Join(_rawPath, relativePath);
        return new DirectoryImpl(relPath, rawPath, _notifyUpdated);
    }

    public FileSystem.IrnFile AppendFile(string filename)
    {
        var relPath = Path.Join(_relPath, filename);
        var rawPath = Path.Join(_rawPath, filename);
        return new FileImpl(relPath, rawPath, _notifyUpdated);
    }
}