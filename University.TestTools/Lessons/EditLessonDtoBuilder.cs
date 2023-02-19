using University.Services.Lessons.Contracts.Dtos;

namespace University.TestTools.Lessons;

public sealed class EditLessonDtoBuilder
{
    private readonly UpdateLessonDto _dto;

    public EditLessonDtoBuilder()
    {
        _dto = new UpdateLessonDto
        {
            Title = "dummy",
            Coefficient = 1
        };
    }

    public EditLessonDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public EditLessonDtoBuilder WithCoefficient(byte coefficient)
    {
        _dto.Coefficient = coefficient;
        return this;
    }

    public UpdateLessonDto Build()
    {
        return _dto;
    }
}