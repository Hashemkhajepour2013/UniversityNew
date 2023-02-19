using University.Persistence.EF;
using University.Persistence.EF.Lessons;
using University.Services.Lessons;
using University.Services.Lessons.Contracts;

namespace University.TestTools.Lessons;

public static class LessonServiceFactory
{
    public static LessonService CreateService(ref EFDataContext context)
    {
        var repository = new EFLessonRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        return new LessonAppService(repository, unitOfWork);
    }
}