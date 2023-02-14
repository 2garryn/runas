namespace DocumentDb;

using LiteDB;


public class DocumentDbFactory
{
    private readonly LiteDatabase _db;

    public DocumentDbFactory(string filename)
    {
        _db = new LiteDatabase(filename);
    }

    public DocumentDbImpl New(string prefix)
    {
        return new DocumentDbImpl(prefix, _db);
    }


}