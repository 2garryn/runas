namespace DocumentDb;
using LiteDB;

public interface IrnDocumentDb
{
    public ILiteCollection<T> GetCollection<T>(string name, BsonAutoId autoId = BsonAutoId.ObjectId);
}