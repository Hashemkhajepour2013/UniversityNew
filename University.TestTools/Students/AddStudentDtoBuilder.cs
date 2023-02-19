using University.Services.Students.Contracts.Dtos;

namespace University.TestTools.Students;

public sealed class AddStudentDtoBuilder
{
    private readonly AddStudentDto _dto;

    public AddStudentDtoBuilder()
    {
        _dto = new AddStudentDto
        {
            FirstName = "dummy-F",
            LastName = "dummy-L",
            Mobile = "dummy-M",
            StudentNumber = "dummy-num"
        };
    }

    public AddStudentDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public AddStudentDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public AddStudentDtoBuilder WithMobile(string mobile)
    {
        _dto.Mobile = mobile;
        return this;
    }

    public AddStudentDtoBuilder WithStudentNumber(string studentNumber)
    {
        _dto.StudentNumber = studentNumber;
        return this;
    }
    
    public AddStudentDto Build()
    {
        return _dto;
    }
}