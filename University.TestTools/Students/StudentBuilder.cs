using University.Entities;

namespace University.TestTools.Students;

public sealed class StudentBuilder
{
    private readonly Student _student;

    public StudentBuilder()
    {
        _student = new Student()
        {
            FirstName = "dummy-F",
            LastName = "dummy-L",
            Mobile = "dummy-M",
            StudentNumber = "dummy-num"
        };
    }

    public StudentBuilder WithFirstName(string firstName)
    {
        _student.FirstName = firstName;
        return this;
    }

    public StudentBuilder WithLastName(string lastName)
    {
        _student.LastName = lastName;
        return this;
    }

    public StudentBuilder WithMobile(string mobile)
    {
        _student.Mobile = mobile;
        return this;
    }
    
    public StudentBuilder WithStudentNumber(string studentNumber)
    {
        _student.StudentNumber = studentNumber;
        return this;
    }

    public Student Build()
    {
        return _student;
    }
}