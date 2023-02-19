using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Classrooms.Contracts;

namespace University.Persistence.EF.Classrooms;

public sealed class EFClassroomRepository : ClassroomRepository
{
    private readonly EFDataContext _context;

    public EFClassroomRepository(EFDataContext context)
    {
        _context = context;
    }

    public async Task Add(Classroom classroom)
    {
        await _context.Classrooms.AddAsync(classroom);
    }

    public async Task<Classroom?> FindById(int id)
    {
        return await _context.Classrooms.FindAsync(id);
    }

    public void Delete(Classroom classroom)
    {
        _context.Classrooms.Remove(classroom);
    }
    
    public async Task<List<Classroom>> FindAllById(int id)
    {
        return await (from classroom in _context.Classrooms
            join studentClassroom in _context.StudentClassrooms
                on classroom.Id equals studentClassroom.ClassroomId
            where studentClassroom.ClassroomId == id
            select classroom).ToListAsync();
    }

    public async Task<List<Classroom>> FindAllClassroomByStudentId(int studentId, int id)
    {
        return await (from classroom in _context.Classrooms
            join studentClassroom in _context.StudentClassrooms
                on classroom.Id equals studentClassroom.ClassroomId
            where studentClassroom.StudentId == studentId &&
                  classroom.Id != id
            select classroom).ToListAsync();
    }
}