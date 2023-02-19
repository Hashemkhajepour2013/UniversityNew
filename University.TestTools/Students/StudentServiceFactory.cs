using University.Persistence.EF;
using University.Persistence.EF.Students;
using University.Services.Students;
using University.Services.Students.Contracts;

namespace University.TestTools.Students;

public static class StudentServiceFactory
{
    public static StudentService CreateService(ref EFDataContext context)
    {
        var repository = new EFStudentRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        return new StudentAppService(repository, unitOfWork);
    }
}