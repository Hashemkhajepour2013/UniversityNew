using FluentAssertions;
using University.Entities;
using University.Persistence.EF;
using University.Services.Infrastructure;
using University.Services.Lessons.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Lessons;
using Xunit;

namespace University.Test.Spec.Lessons.Delete;

public sealed class Delete : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly LessonService _sut;
    private Lesson _lesson;
    public Delete(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = LessonServiceFactory.CreateService(ref _context);
    }

    [Given("درسی با عنوان ٰ ریاضی ٰ و ضریب ٰ2 ٰ در فهرست دروس وجود دارد.")]
    private void Given()
    {
        _lesson = new LessonBuilder()
            .WithTitle("ریاضی")
            .WithCoefficient(2)
            .Build();
        _context.Save(_lesson);
    }

    [When(" درسی با عنوان ٰ ریاضی ٰ و ضریب ٰ 2 ٰ را حذف میکنم.")]
    private async Task When()
    {
        await _sut.Delete(_lesson.Id);
    }

    [Then(" ‌نباید درسی با عنوان ٰ ریاضی ٰ و ضریب ٰ 2 ٰ در فهرست دروس وجود داشته باشد.")]
    private void Then()
    {
        _context.Lessons.Should().BeNullOrEmpty();
    }
    
    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When().Wait(),
            _ => Then());
    }
}