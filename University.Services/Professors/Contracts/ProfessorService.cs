using University.Services.Infrastructure;
using University.Services.Professors.Contracts.Dtos;

namespace University.Services.Professors.Contracts;

public interface ProfessorService : Service
{
    Task<int> Add(AddProfessorDto dto);
    Task Update(UpdateProfessorDto dto, int id);
    Task Delete(int id);
}