using University.Services.Lessons.Contracts.Dtos;

namespace University.Services.Lessons.Contracts;

public interface LessonService
{
    Task<int> Add(AddLessonDto dto);
    Task Update(UpdateLessonDto dto, int id);
    Task Delete(int id);
}