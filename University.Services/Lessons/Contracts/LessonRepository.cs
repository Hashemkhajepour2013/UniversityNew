using University.Entities;

namespace University.Services.Lessons.Contracts;

public interface LessonRepository
{
    Task Add(Lesson lesson);

    Task<Lesson?> FindById(int id);

    void Delete(Lesson lesson);

    Task<bool> IsExistLesson(int id);
}