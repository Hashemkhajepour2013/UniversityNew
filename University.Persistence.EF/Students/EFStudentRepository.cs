using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Students.Contracts;

namespace University.Persistence.EF.Students;

public sealed class EFStudentRepository : StudentRepository
{
    private readonly EFDataContext _context;

    public EFStudentRepository(EFDataContext context)
    {
        _context = context;
    }

    public async Task Add(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public async Task<bool> MobileIsExist(string mobile)
    {
        return await _context.Students.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<Student?> FindById(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public void Delete(Student student)
    {
        _context.Students.Remove(student);
    }

    public async Task<bool> IsExistStudent(int id)
    {
        return await _context.Students.AnyAsync(_ => _.Id == id);
    }
}