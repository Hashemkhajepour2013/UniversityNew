namespace University.Entities;

public sealed class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string StudentNumber { get; set; }

    public List<StudentClassroom> StudentClassrooms { get; set; } = new();
}