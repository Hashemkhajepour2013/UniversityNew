using University.Entities;
using University.Services.Infrastructure;

namespace University.Services.Professors.Contracts;

public interface ProfessorRepository : Service
{
    Task Add(Professor professor);
    
    Task<bool> MobileIsExist(string mobile);

    Task<Professor?> FindById(int id);

    void Delete(Professor professor);

    Task<bool> IsExistProfessor(int id);
}