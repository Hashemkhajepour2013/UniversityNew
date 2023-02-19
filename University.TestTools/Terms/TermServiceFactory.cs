using University.Persistence.EF;
using University.Persistence.EF.Terms;
using University.Services.Terms;
using University.Services.Terms.Contracts;

namespace University.TestTools.Terms;

public static class TermServiceFactory
{
    public static TermService CreateService(ref EFDataContext context)
    {
        var repository = new EFTermRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        return new TermAppService(repository, unitOfWork);
    }
}