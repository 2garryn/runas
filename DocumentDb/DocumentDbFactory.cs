namespace DocumentDb;

using LiteDB;


public class DocumentDbFactory
{
    private readonly LiteDatabase _db;

    public DocumentDbFactory(LiteDatabase db)
    {
        _db = db;
    }

    public DocumentDbImpl New(string prefix)
    {
        return new DocumentDbImpl(prefix, _db);
    }


}