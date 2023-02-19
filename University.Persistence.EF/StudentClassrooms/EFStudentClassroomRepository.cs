using University.Entities;
using University.Services.StudentClassrooms.Contracts;

namespace University.Persistence.EF.StudentClassrooms;

public sealed class EFStudentClassroomRepository : StudentClassroomRepository
{
    private readonly EFDataContext _context;

    public EFStudentClassroomRepository(EFDataContext context)
    {
        _context = context;
    }

    public async Task Add(StudentClassroom studentClassroom)
    {
        await _context.StudentClassrooms.AddAsync(studentClassroom);
    }

    public async Task<StudentClassroom?> FindById(int id)
    {
        return await _context.StudentClassrooms.FindAsync(id);
    }

    public void Delete(StudentClassroom studentClassroom)
    {
        _context.StudentClassrooms.Remove(studentClassroom);
    }
}