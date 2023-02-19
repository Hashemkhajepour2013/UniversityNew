namespace University.Entities;

public sealed class StudentClassroom
{
    public int Id { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; }
}