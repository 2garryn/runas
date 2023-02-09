namespace FileSystem;
public interface IrnFileSystem {
    public IrnDirectory RootDirectory();
    public IrnDirectory CreateDirectory(IrnDirectory parentDirectory, string relativePath);
     public IrnDirectory CreateDirectory(IrnDirectory parentDirectory);
    public IEnumerable<IrnDirectory> ListDirectories(IrnDirectory directory);
    public IEnumerable<IrnFile> ListFiles(IrnDirectory directory);
    public IrnDirectory AppendDirectory(IrnDirectory directory, string relativePath);
    public IrnFile AppendFile(IrnDirectory directory, string fileName);
    public IrnFile Createfile(IrnFile file);
}