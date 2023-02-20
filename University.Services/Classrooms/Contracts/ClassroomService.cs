using University.Services.Classrooms.Contracts.Dtos;
using University.Services.Infrastructure;

namespace University.Services.Classrooms.Contracts;

public interface ClassroomService : Service
{
    Task<int> Add(AddClassroomDto dto);
    Task Update(UpdateClassroomDto dto, int id);
    Task Delete(int id);
}