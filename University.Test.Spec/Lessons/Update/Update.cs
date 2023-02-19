using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Persistence.EF;
using University.Services.Lessons.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Lessons;
using Xunit;

namespace University.Test.Spec.Lessons.Update;

public sealed class Update : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly LessonService _sut;
    private Lesson _lesson;
    
    public Update(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = LessonServiceFactory.CreateService(ref _context);
    }

    [Given("درسی با عنوان ٰ ریاضی ٰ و با ضریب ٰ2 ٰ در فهرست دروس وجود دارد.")]
    private void Given()
    {
        _lesson = new LessonBuilder()
            .WithTitle("ریاضی")
            .WithCoefficient(2)
            .Build();
        _context.Save(_lesson);
    }

    [When("درس با عنوان ٰ ریاضی ٰ و با ضریب ٰ 2 ٰ را به درس" +
          " با عنوان ٰ‌فیزیک ٰ و ضریب ٰ 4 ٰ  ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new EditLessonDtoBuilder()
            .WithTitle("ٰ‌فیزیک")
            .WithCoefficient(4)
            .Build();
        await _sut.Update(dto, _lesson.Id);
    }

    [Then("باید یک درس با عنوان ٰ فیزیک ٰ و ضریب ٰ4 ٰ" +
          " در فهرست دروس وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Lessons.SingleAsync();
        expected.Title.Should().Be("ٰ‌فیزیک");
        expected.Coefficient.Should().Be(4);
    }
      
    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When().Wait(),
            _ => Then().Wait());
    }
}