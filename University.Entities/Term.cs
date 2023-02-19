namespace University.Entities;

public sealed class Term
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte UnitCount { get; set; }
    public List<Classroom> Classrooms { get; set; } = new();
}