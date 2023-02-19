using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Services.Lessons.Contracts;
using University.Services.Lessons.Extensions;
using University.Tests.Unit.Infrastructure;
using University.TestTools.Lessons;
using Xunit;

namespace University.Tests.Unit.Lessons;

public sealed class LessonServiceTests : BusinessUnitTest
{ 
    private readonly LessonService _sut;

    public LessonServiceTests()
    {
        var context = DbContext();
        _sut = LessonServiceFactory.CreateService(ref context);    
    }
    
    [Fact]
    private async Task Add_add_lesson_properly()
    {
        var dto = new CreateLessonDtoBuilder()
            .WithTitle("dummy-T")
            .WithCoefficient(1).Build();
        
        await _sut.Add(dto); 

        var expected = await DbContext().Lessons.SingleAsync();
        expected.Title.Should().Be(dto.Title);
        expected.Coefficient.Should().Be(dto.Coefficient);
    }

    [Fact]
    private async Task Update_update_lesson_properly()
    {
        var lesson = new LessonBuilder()
            .WithTitle("dummy-T")
            .WithCoefficient(1)
            .Build();
        Save(lesson);
        var dto = new EditLessonDtoBuilder()
            .WithTitle("dummy-E")
            .WithCoefficient(2)
            .Build();

        await _sut.Update(dto, lesson.Id);

        var expected = await DbContext().Lessons.SingleAsync();
        expected.Id.Should().Be(lesson.Id);
        expected.Title.Should().Be(dto.Title);
        expected.Coefficient.Should().Be(dto.Coefficient);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_lesson_not_found(int lessonId)
    {
        var dto = new EditLessonDtoBuilder().Build();

        var expected = async () => await _sut.Update(dto, lessonId);

        await expected.Should().ThrowExactlyAsync<LessonNotFoundException>();
    }

    [Fact]
    private async Task Delete_delete_lesson_properly()
    {
        var lesson = new LessonBuilder()
            .WithTitle("lesson-T")
            .WithCoefficient(2)
            .Build();

        await _sut.Delete(lesson.Id);

        DbContext().Lessons.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Delete_delete_throw_exception_when_lesson_not_found(int lessonId)
    {
        var expected = async () => await _sut.Delete(lessonId);

        await expected.Should().ThrowExactlyAsync<LessonNotFoundException>();
    }
}