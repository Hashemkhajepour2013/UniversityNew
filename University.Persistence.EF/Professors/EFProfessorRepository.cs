using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Professors.Contracts;

namespace University.Persistence.EF.Professors;

public sealed class EFProfessorRepository : ProfessorRepository
{
    private readonly EFDataContext _context;

    public EFProfessorRepository(EFDataContext context)
    {
        _context = context;
    }

    public async Task Add(Professor professor)
    {
        await _context.Professors.AddAsync(professor);
    }

    public async Task<bool> MobileIsExist(string mobile)
    {
        return await _context.Professors.AnyAsync(_ => _.Mobile == mobile);
    }

    public async Task<Professor?> FindById(int id)
    {
        return await _context.Professors.FindAsync(id);
    }

    public void Delete(Professor professor)
    {
        _context.Professors.Remove(professor);
    }

    public async Task<bool> IsExistProfessor(int id)
    {
        return await _context.Professors.AnyAsync(_ => _.Id == id);
    }
}