using University.Entities;
using University.Services.Infrastructure;

namespace University.Services.Terms.Contracts;

public interface TermRepository : Service
{
    Task Add(Term term);

    Task<Term?> FindById(int id);

    void Delete(Term term);

    Task<bool> IsExistTerm(int id);
}