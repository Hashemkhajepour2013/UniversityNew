using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Lessons.Contracts;

namespace University.Persistence.EF.Lessons;

public sealed class EFLessonRepository : LessonRepository
{
    private readonly EFDataContext _context;

    public EFLessonRepository(EFDataContext context)
    {
        _context = context;
    }

    public async Task Add(Lesson lesson)
    {
        await _context.Lessons.AddAsync(lesson);
    }

    public async Task<Lesson?> FindById(int id)
    {
        return await _context.Lessons.FindAsync(id);
    }

    public void Delete(Lesson lesson)
    {
        _context.Lessons.Remove(lesson);
    }

    public async Task<bool> IsExistLesson(int id)
    {
        return await _context.Lessons.AnyAsync(_ => _.Id == id);
    }
}