using University.Entities;
using University.Services.Infrastructure;

namespace University.Services.Lessons.Contracts;

public interface LessonRepository : Service
{
    Task Add(Lesson lesson);

    Task<Lesson?> FindById(int id);

    void Delete(Lesson lesson);

    Task<bool> IsExistLesson(int id);
}