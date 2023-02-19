using University.Entities;

namespace University.TestTools.Classrooms;

public sealed class ClassroomBuilder
{
    private Classroom _classroom;

    public ClassroomBuilder()
    {
        _classroom = new Classroom
        {

        };
    }

    public ClassroomBuilder WithStartDate(DateTime startDate)
    {
        _classroom.StartDate = startDate;
        return this;
    }

    public ClassroomBuilder WithEndDate(DateTime endDate)
    {
        _classroom.EndDate = endDate;
        return this;
    }

    public ClassroomBuilder WithCapacity(byte capacity)
    {
        _classroom.Capacity = capacity;
        return this;
    }

    public ClassroomBuilder WithTerm(int termId)
    {
        _classroom.TermId = termId;
        return this;
    }

    public ClassroomBuilder WithLesson(int lessonId)
    {
        _classroom.LessonId = lessonId;
        return this;
    }
    
    public ClassroomBuilder WithProfessor(int professorId)
    {
        _classroom.ProfessorId = professorId;
        return this;
    }

    public Classroom Build()
    {
        return _classroom;
    }
}