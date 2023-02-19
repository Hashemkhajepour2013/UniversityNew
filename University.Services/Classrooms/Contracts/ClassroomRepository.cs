using University.Entities;

namespace University.Services.Classrooms.Contracts;

public interface ClassroomRepository
{
    Task Add(Classroom classroom);

    Task<Classroom?> FindById(int id);

    void Delete(Classroom classroom);

    Task<List<Classroom>> FindAllById(int id);
    
    Task<List<Classroom>> FindAllClassroomByStudentId(int studentId, int id);
}