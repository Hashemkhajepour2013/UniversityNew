using University.Entities;

namespace University.TestTools.Lessons;

public sealed class LessonBuilder
{
    private readonly Lesson _lesson;

    public LessonBuilder()
    {
        _lesson = new Lesson
        {
            Title = "dummy",
            Coefficient = 1
        };
    }

    public LessonBuilder WithTitle(string title)
    {
        _lesson.Title = title;
        return this;
    }

    public LessonBuilder WithCoefficient(byte coefficient)
    {
        _lesson.Coefficient = coefficient;
        return this;
    }

    public Lesson Build()
    {
        return _lesson;
    }
}