using University.Entities;

namespace University.Services.Terms.Contracts;

public interface TermRepository
{
    Task Add(Term term);

    Task<Term?> FindById(int id);

    void Delete(Term term);

    Task<bool> IsExistTerm(int id);
}