using University.Services.Classrooms.Contracts.Dtos;

namespace University.Services.Classrooms.Contracts;

public interface ClassroomService
{
    Task<int> Add(AddClassroomDto dto);
    Task Update(UpdateClassroomDto dto, int id);
    Task Delete(int id);
}