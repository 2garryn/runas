namespace DocumentDb;
using LiteDB;

public interface IrbDocumentDb 
{
    public ILiteCollection<T> GetCollection<T>(string name, BsonAutoId autoId = BsonAutoId.ObjectId);
}