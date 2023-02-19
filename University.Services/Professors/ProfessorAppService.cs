using University.Entities;
using University.Services.Documents.Exceptions;
using University.Services.Professors.Contracts;
using University.Services.Professors.Contracts.Dtos;
using University.Services.Professors.Extensions;

namespace University.Services.Professors;

public sealed class ProfessorAppService : ProfessorService
{
    private readonly ProfessorRepository _repository;
    private readonly UnitOfWork _unitOfWork;

    public ProfessorAppService(
        ProfessorRepository repository, 
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Add(AddProfessorDto dto)
    {
        await StopIfMobileIsDuplicate(dto.Mobile);
        
        var professor = PrototypingOfProfessor(dto); 
        
        await _repository.Add(professor);
        
        await _unitOfWork.Complete();
        
        return professor.Id;
    }

    public async Task Update(UpdateProfessorDto dto, int id)
    {
        var professor = await StopIfProfessorNotFound(id);
        
        EditProfessor(dto, professor);
        
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var professor = await StopIfProfessorNotFound(id);
        
        _repository.Delete(professor);
        
        await _unitOfWork.Complete();
    }
    
    private static Professor PrototypingOfProfessor(AddProfessorDto dto)
    {
        var professor = new Professor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Mobile = dto.Mobile,
            PersonnelId = dto.PersonnelId
        };
        return professor;
    }
    
    private async Task StopIfMobileIsDuplicate(string mobile)
    {
        var mobileIsExist = await _repository.MobileIsExist(mobile);
        if (mobileIsExist)
        {
            throw new MobileIsDuplicateException();
        }
    }
    
    private async Task<Professor> StopIfProfessorNotFound(int id)
    {
        var professor = await _repository.FindById(id);
        if (professor == null)
        {
            throw new ProfessorNotFoundException();
        }

        return professor;
    }
    
    private static void EditProfessor(UpdateProfessorDto dto, Professor professor)
    {
        professor.FirstName = dto.FirstName;
        professor.LastName = dto.LastName;
        professor.PersonnelId = dto.PersonnelId;
    }
}