using University.Services.Terms.Contracts.Dtos;

namespace University.Services.Terms.Contracts;

public interface TermService
{
    Task<int> Add(AddTermDto dto);

    Task Update(UpdateTermDto dto, int id);

    Task Delete(int id);
}