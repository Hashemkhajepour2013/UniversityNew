using University.Entities;
namespace University.TestTools.Professors;

public sealed class ProfessorBuilder
{
    private readonly Professor _professor;
    public ProfessorBuilder()
    {
        _professor = new Professor();
    }

    public ProfessorBuilder WithFirstName(string firstName)
    {
        _professor.FirstName = firstName;
        return this;
    }

    public ProfessorBuilder WithLastName(string lastName)
    {
        _professor.LastName = lastName;
        return this;
    }

    public ProfessorBuilder WithMobile(string mobile)
    {
        _professor.Mobile = mobile;
        return this;
    }

    public ProfessorBuilder WithPersonnel(string personnelId)
    {
        _professor.PersonnelId = personnelId;
        return this;
    }
    
    public Professor Build()
    {
        return _professor;
    }
}