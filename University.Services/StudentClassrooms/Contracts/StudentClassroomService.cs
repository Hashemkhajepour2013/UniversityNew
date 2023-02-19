using University.Services.StudentClassrooms.Contracts.Dtos;

namespace University.Services.StudentClassrooms.Contracts;

public interface StudentClassroomService
{
    Task<int> Add(AddStudentClassroomDto dto);
    Task Delete(int professorId);
}