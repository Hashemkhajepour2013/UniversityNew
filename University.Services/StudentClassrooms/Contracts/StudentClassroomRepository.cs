using University.Entities;
using University.Services.Infrastructure;

namespace University.Services.StudentClassrooms.Contracts;

public interface StudentClassroomRepository : Service
{
    Task Add(StudentClassroom studentClassroom);

    Task<StudentClassroom?> FindById(int id);

    void Delete(StudentClassroom studentClassroom);
}