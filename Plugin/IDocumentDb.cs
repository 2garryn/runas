namespace Plugin;
using LiteDB;

public interface IDocumentDb
{
    public ILiteCollection<T> GetCollection<T>(string name, BsonAutoId autoId = BsonAutoId.ObjectId);
}