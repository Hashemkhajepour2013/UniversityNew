using University.Services.Professors.Contracts.Dtos;

namespace University.TestTools.Professors;

public sealed class UpdateProfessorDtoBuilder
{
    private UpdateProfessorDto _dto;

    public UpdateProfessorDtoBuilder()
    {
        _dto = new UpdateProfessorDto
        {
            FirstName = "dummy-F",
            LastName = "dummy-L"
        };
    }

    public UpdateProfessorDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public UpdateProfessorDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }
    
    public UpdateProfessorDtoBuilder WithPersonnel(string personnelId)
    {
        _dto.PersonnelId = personnelId;
        return this;
    }

    public UpdateProfessorDto Build()
    {
        return _dto;
    }
}