using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Terms.Contracts;

namespace University.Persistence.EF.Terms;

public sealed class EFTermRepository : TermRepository
{
    private readonly EFDataContext _context;

    public EFTermRepository(EFDataContext context)
    {
        _context = context;
    }

    public async Task Add(Term term)
    {
        await _context.AddAsync(term);
    }

    public async Task<Term?> FindById(int id)
    {
        return await _context.Terms.FindAsync(id);
    }

    public void Delete(Term term)
    {
        _context.Terms.Remove(term);
    }

    public async Task<bool> IsExistTerm(int id)
    {
        return await _context.Terms.AnyAsync(_ => _.Id == id);
    }
}