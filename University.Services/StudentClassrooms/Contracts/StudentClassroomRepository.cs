using University.Entities;

namespace University.Services.StudentClassrooms.Contracts;

public interface StudentClassroomRepository
{
    Task Add(StudentClassroom studentClassroom);

    Task<StudentClassroom?> FindById(int id);

    void Delete(StudentClassroom studentClassroom);
}