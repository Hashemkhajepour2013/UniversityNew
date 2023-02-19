using University.Services.Professors.Contracts.Dtos;

namespace University.TestTools.Professors;

public sealed class CreateProfessorDtoBuilder
{
    private readonly AddProfessorDto _dto;

    public CreateProfessorDtoBuilder()
    {
        _dto = new AddProfessorDto
        {
            FirstName = "dummy-F",
            LastName = "dummy-L",
            Mobile = "dummy-M",
            PersonnelId = "dummy-Id"
        };
    }

    public CreateProfessorDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public CreateProfessorDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public CreateProfessorDtoBuilder WithMobile(string mobile)
    {
        _dto.Mobile = mobile;
        return this;
    }
    
    public CreateProfessorDtoBuilder WithPersonnel(string personnelId)
    {
        _dto.PersonnelId = personnelId;
        return this;
    }

    public AddProfessorDto Build()
    {
        return _dto;
    }
}