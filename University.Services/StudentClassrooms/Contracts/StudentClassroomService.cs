using University.Services.Infrastructure;
using University.Services.StudentClassrooms.Contracts.Dtos;

namespace University.Services.StudentClassrooms.Contracts;

public interface StudentClassroomService : Service
{
    Task<int> Add(AddStudentClassroomDto dto);
    Task Delete(int professorId);
}