namespace FsImplementation;

using Plugin;
using System.IO;
using System;
using Plugin;
public struct DirectoryImpl : FileSystem.IrnDirectory
{
    private readonly string _relPath;
    private readonly string _rawPath;
    private FileLocker _notifierList;
    public DirectoryImpl(string relPath, string rawPath, FileLocker notifierList) 
    {
        _relPath = relPath ?? throw new ArgumentNullException(nameof(relPath));
        _rawPath = rawPath ?? throw new ArgumentNullException(nameof(rawPath));
        _notifierList = notifierList;
    }

    public bool Exists() => Directory.Exists(_rawPath);
    public string RelativePath() => _relPath;
    public string RawPath() => _rawPath;

    public FileSystem.IrnDirectory AppendDirectory(string relativePath)
    {
        var relPath = Path.Join(_relPath, relativePath);
        var rawPath = Path.Join(_rawPath, relativePath);
        return new DirectoryImpl(relPath, rawPath, _notifierList);
    }

    public FileSystem.IrnFile AppendFile(string filename)
    {
        var relPath = Path.Join(_relPath, filename);
        var rawPath = Path.Join(_rawPath, filename);
        return new FileImpl(relPath, rawPath, _notifierList);
    }
}