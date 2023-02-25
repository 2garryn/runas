namespace FsImplementation;
using Plugin;
using LiteDB;




public class AttrStorage
{
    private LiteDatabase _db;
    public AttrStorage(LiteDatabase db)
    {
        _db = db;
        var col = _db.GetCollection<AttrDto>("files");
        col.EnsureIndex((dto) => dto.Path, true);
        var col2 = _db.GetCollection<AttrDto>("directories");
        col.EnsureIndex((dto) => dto.Path, true);
    }
    public void SetAttrs(IFsDirectory directory, FsAttr[] attrs) => DoSetAttrs(directory.RelativePath(), "files", attrs);
    public void SetAttrs(IFsFile file, FsAttr[] attrs) => DoSetAttrs(file.RelativePath(), "directories", attrs);

    private void DoSetAttrs(string path, string collection, FsAttr[] attrs)
    {
        var dtos = attrs.Select((a) =>
            new AttrDto
            {
                Label = a.Label,
                AttrID = a.ID,
                Value = a.Value,
                Path = path,
                Id = $"{a.ID}_{path}"
            }
        );
        var col = _db.GetCollection<AttrDto>(collection);
        foreach (AttrDto attr in dtos)
        {
            col.Upsert(attr);
        }
    }
    public IEnumerable<FsAttr> GetAllAttrs(IFsDirectory directory) => DoGetAllAttrs(directory.RelativePath(), "directories");
    public IEnumerable<FsAttr> GetAllAttrs(IFsFile file) => DoGetAllAttrs(file.RelativePath(), "files");

    private IEnumerable<FsAttr> DoGetAllAttrs(string path, string collection)
    {
        var col = _db.GetCollection<AttrDto>(collection);
        var attrs = col.Find((dto) => dto.Path == path);
        return attrs.Select((attr) =>
        new FsAttr
        {
            Label = attr.Label,
            ID = attr.AttrID,
            Value = attr.Value
        });
    }
}

public record AttrDto
{
    public required string Id { init; get; }
    public required string Label { init; get; }
    public required string AttrID { init; get; }
    public required string Value { init; get; }
    public required string Path { init; get; }
}