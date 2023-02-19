using University.Persistence.EF;
using University.Persistence.EF.Professors;
using University.Services.Professors;
using University.Services.Professors.Contracts;

namespace University.TestTools.Professors;

public static class ProfessorServiceFactory
{
    public static ProfessorService CreateService(
        ref EFDataContext context)
    {
        var repository = new EFProfessorRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        return new ProfessorAppService(repository, unitOfWork);
    }
}