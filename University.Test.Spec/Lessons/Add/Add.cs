using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Persistence.EF;
using University.Services.Lessons.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools.Lessons;
using Xunit;

namespace University.Test.Spec.Lessons.Add;

public sealed class Add : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly LessonService _sut;
    
    public Add(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = LessonServiceFactory.CreateService(ref _context);
    }

    [Given("هیچ درسی در فهرست دروس وجود ندارد.")]
    private void Given()
    {
    }

    [When(" یک درس با عنوان ٰ ریاضی ٰ و با ضریب ٰ 2 ٰ ثبت میکنم.")]
    private async Task When()
    {
        var dto = new CreateLessonDtoBuilder()
            .WithTitle("ریاضی")
            .WithCoefficient(2)
            .Build(); 
        await _sut.Add(dto);
    }

    [Then(" باید یک درس با عنوان ٰ ریاضی ٰ و با " +
          "ضریب ٰ2 ٰ در فهرست دروس وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Lessons.SingleAsync();
        expected.Title.Should().Be("ریاضی");
        expected.Coefficient.Should().Be(2);
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