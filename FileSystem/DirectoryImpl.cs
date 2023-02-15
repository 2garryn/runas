namespace FsImplementation;

using Plugin;
using System.IO;
using System;
using FileSystem;
using System.Collections.Generic;

public struct DirectoryImpl : FileSystem.IrnDirectory
{
    private readonly string _relPath;
    private readonly string _rawPath;
    private FileLocker _notifierList;
    private AttrStorage _attrStorage;

    public DirectoryImpl(string relPath, string rawPath, FileLocker notifierList, AttrStorage attrStorage)
    {
        _relPath = relPath ?? throw new ArgumentNullException(nameof(relPath));
        _rawPath = rawPath ?? throw new ArgumentNullException(nameof(rawPath));
        _notifierList = notifierList;
        _attrStorage = attrStorage;
    }

    public bool Exists() => Directory.Exists(_rawPath);
    public string RelativePath() => _relPath;
    public string RawPath() => _rawPath;

    public FileSystem.IrnDirectory AppendDirectory(string relativePath)
    {
        var relPath = Path.Join(_relPath, relativePath);
        var rawPath = Path.Join(_rawPath, relativePath);
        return new DirectoryImpl(relPath, rawPath, _notifierList, _attrStorage);
    }

    public FileSystem.IrnFile AppendFile(string filename)
    {
        var relPath = Path.Join(_relPath, filename);
        var rawPath = Path.Join(_rawPath, filename);
        return new FileImpl(relPath, rawPath, _notifierList, _attrStorage);
    }

    public void SetAttrs(params Attr[] attr) => _attrStorage.SetAttrs(this, attr);

    public IEnumerable<Attr> GetAllAttrs() => _attrStorage.GetAllAttrs(this);
}