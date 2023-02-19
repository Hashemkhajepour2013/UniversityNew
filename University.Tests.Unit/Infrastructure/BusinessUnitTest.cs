using University.Persistence.EF;
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
    
    public void Save<T>(T entity)
    {
        if (entity != null)
            _context.Manipulate(_ => _.Add(entity));
    }
}