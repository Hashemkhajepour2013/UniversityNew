using University.Entities;
using University.Services.Terms.Contracts;
using University.Services.Terms.Contracts.Dtos;
using University.Services.Terms.Extensions;

namespace University.Services.Terms;

public sealed class TermAppService : TermService
{
    private readonly TermRepository _repository;
    private readonly UnitOfWork _unitOfWork;

    public TermAppService(
        TermRepository repository, 
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Add(AddTermDto dto)
    {
        var term = PrototypingOfTerm(dto);

        await _repository.Add(term);
        
        await _unitOfWork.Complete();
        
        return term.Id;
    }

    public async Task Update(UpdateTermDto dto, int id)
    {
        var term = await StopIfTermNotFound(id);

        EditTerm(dto, term);
        
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var term = await StopIfTermNotFound(id);
        
        _repository.Delete(term);

        await _unitOfWork.Complete();
    }

    private static Term PrototypingOfTerm(AddTermDto dto)
    {
        var term = new Term
        {
            Title = dto.Title,
            UnitCount = dto.UnitCount
        };
        return term;
    }

    private static void EditTerm(UpdateTermDto dto, Term? term)
    {
        term.Title = dto.Title;
        term.UnitCount = dto.UnitCount;
    }

    private async Task<Term> StopIfTermNotFound(int id)
    {
        var term = await _repository.FindById(id);
        if (term == null)
        {
            throw new TermNotFoundException();
        }

        return term;
    }
}