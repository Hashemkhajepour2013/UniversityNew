using University.Services.Infrastructure;
using University.Services.Lessons.Contracts.Dtos;

namespace University.Services.Lessons.Contracts;

public interface LessonService : Service
{
    Task<int> Add(AddLessonDto dto);
    Task Update(UpdateLessonDto dto, int id);
    Task Delete(int id);
}