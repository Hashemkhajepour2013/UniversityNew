using University.Persistence.EF;
using University.Persistence.EF.Classrooms;
using University.Persistence.EF.Professors;
using University.Persistence.EF.StudentClassrooms;
using University.Persistence.EF.Students;
using University.Services.StudentClassrooms;
using University.Services.StudentClassrooms.Contracts;

namespace University.TestTools.StudentClassrooms;

public static class StudentClassroomServiceFactory
{
    public static StudentClassroomService CreateService(ref EFDataContext context)
    {
        var repository = new EFStudentClassroomRepository(context);
        var studentRepository = new EFStudentRepository(context);
        var classroomRepository = new EFClassroomRepository(context);
        var professorRepository = new EFProfessorRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        
        return new StudentClassroomAppService(
            repository,
            studentRepository,
            professorRepository,
            classroomRepository,
            unitOfWork);
    }
}