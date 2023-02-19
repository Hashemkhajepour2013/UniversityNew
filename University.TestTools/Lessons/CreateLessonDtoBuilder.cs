using University.Services.Lessons.Contracts.Dtos;

namespace University.TestTools.Lessons;

public sealed class CreateLessonDtoBuilder
{
    private readonly AddLessonDto _dto;

    public CreateLessonDtoBuilder()
    {
        _dto = new AddLessonDto
        {
            Title = "dummy",
            Coefficient = 1
        };
    }

    public CreateLessonDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public CreateLessonDtoBuilder WithCoefficient(byte coefficient)
    {
        _dto.Coefficient = coefficient;
        return this;
    }

    public AddLessonDto Build()
    {
        return _dto;
    }
}