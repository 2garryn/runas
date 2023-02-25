namespace DocumentDb;

using LiteDB;
using Plugin;

public class DocumentDbImpl : IDocumentDb
{
    private string _prefix;
    private LiteDatabase _db;

    public DocumentDbImpl(string prefix, LiteDatabase db)
    {
        _prefix = prefix;
        _db = db;
    }

    public ILiteCollection<T> GetCollection<T>(string name, BsonAutoId autoId = BsonAutoId.ObjectId)
    {
        return _db.GetCollection<T>($"{_prefix}_{name}", autoId);
    }
}
