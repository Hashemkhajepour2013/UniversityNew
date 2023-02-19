using University.Services.Students.Contracts.Dtos;

namespace University.TestTools.Students;

public class UpdateStudentDtoBuilder
{
    private readonly UpdateStudentDto _dto;

    public UpdateStudentDtoBuilder()
    {
        _dto = new UpdateStudentDto
        {
            FirstName = "dummy-F",
            LastName = "dummy-L",
        };
    }

    public UpdateStudentDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public UpdateStudentDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public UpdateStudentDtoBuilder WithStudentNumber(string studentNumber)
    {
        _dto.StudentNumber = studentNumber;
        return this;
    }
    
    public UpdateStudentDto Build()
    {
        return _dto;
    }
}