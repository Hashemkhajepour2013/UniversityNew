using Microsoft.Extensions.DependencyInjection;
using University.Persistence.EF.Classrooms;
using University.Persistence.EF.Lessons;
using University.Persistence.EF.Professors;
using University.Persistence.EF.StudentClassrooms;
using University.Persistence.EF.Students;
using University.Persistence.EF.Terms;
using University.Services.Classrooms;
using University.Services.Classrooms.Contracts;
using University.Services.Infrastructure;
using University.Services.Lessons;
using University.Services.Lessons.Contracts;
using University.Services.Professors;
using University.Services.Professors.Contracts;
using University.Services.StudentClassrooms;
using University.Services.StudentClassrooms.Contracts;
using University.Services.Students;
using University.Services.Students.Contracts;
using University.Services.Terms;
using University.Services.Terms.Contracts;

namespace University.Persistence.EF.Infrastructure;

public class DependencyInjection : Service
{
    public static void RegisterServices(IServiceCollection service)
    {
        service.AddScoped<ClassroomService, ClassroomAppService>();
        service.AddScoped<ClassroomRepository, EFClassroomRepository>();

        service.AddScoped<LessonService, LessonAppService>();
        service.AddScoped<LessonRepository, EFLessonRepository>();
        
        service.AddScoped<ProfessorService, ProfessorAppService>();
        service.AddScoped<ProfessorRepository, EFProfessorRepository>();
        
        service.AddScoped<StudentClassroomService, StudentClassroomAppService>();
        service.AddScoped<StudentClassroomRepository, EFStudentClassroomRepository>();
        
        service.AddScoped<StudentService, StudentAppService>();
        service.AddScoped<StudentRepository, EFStudentRepository>();
        
        service.AddScoped<TermService, TermAppService>();
        service.AddScoped<TermRepository, EFTermRepository>();
    }
}