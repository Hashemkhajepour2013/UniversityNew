using University.Entities;
using University.Services.Infrastructure;

namespace University.Services.Classrooms.Contracts;

public interface ClassroomRepository : Service
{
    Task Add(Classroom classroom);

    Task<Classroom?> FindById(int id);

    void Delete(Classroom classroom);

    Task<List<Classroom>> FindAllById(int id);
    
    Task<List<Classroom>> FindAllClassroomByStudentId(int studentId, int id);
}