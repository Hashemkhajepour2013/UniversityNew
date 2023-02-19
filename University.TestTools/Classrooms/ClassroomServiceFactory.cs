using University.Persistence.EF;
using University.Persistence.EF.Classrooms;
using University.Persistence.EF.Lessons;
using University.Persistence.EF.Professors;
using University.Persistence.EF.Terms;
using University.Services.Classrooms;
using University.Services.Classrooms.Contracts;

namespace University.TestTools.Classrooms;

public static class ClassroomServiceFactory
{
    public static ClassroomService CreateService(ref EFDataContext context)
    {
        var repository = new EFClassroomRepository(context);
        var professorRepository = new EFProfessorRepository(context);
        var lessonRepository = new EFLessonRepository(context);
        var termRepository = new EFTermRepository(context);
        
        var unitOfWork = new EFUnitOfWork(context);
        
        return new ClassroomAppService(
            repository,
            lessonRepository,
            termRepository,
            professorRepository,
            unitOfWork);
    }
}