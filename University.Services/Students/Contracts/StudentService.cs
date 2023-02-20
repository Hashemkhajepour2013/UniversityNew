using University.Services.Infrastructure;
using University.Services.Students.Contracts.Dtos;

namespace University.Services.Students.Contracts;

public interface StudentService : Service
{
    Task<int> Add(AddStudentDto dto);
    Task Update(UpdateStudentDto dto, int id);
    Task Delete(int id);
}