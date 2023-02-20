using University.Persistence.EF;
using University.Services.Infrastructure;
using University.TestTools;

namespace University.Tests.Unit.Infrastructure;

public abstract class BusinessUnitTest
{
    private readonly EFDataContext _context;
    
    public BusinessUnitTest()
    {
        var db = new EFInMemoryDatabase();
        _context = db.CreateDataContext<EFDataContext>();
    }

    public EFDataContext DbContext()
    {
        return _context;
    }
    
    protected void Save<T>(T entity) where T : class, new()
    {
        _context.Save(entity);
    }

    protected void Save<T>(params T[] entities) where T : class, new()
    {
        foreach (var entity in entities)
            _context.SaveRange(entity);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}