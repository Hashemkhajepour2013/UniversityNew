using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Persistence.EF;
using University.Services.Terms.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools.Terms;
using Xunit;

namespace University.Test.Spec.Terms.Add;

public sealed class Add : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly TermService _sut;
    
    public Add(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TermServiceFactory.CreateService(ref _context);
    }

    [Given("هیچ ترمی در فهرست ترم ها وجود ندارد.")]
    private void Given()
    {
    }

    [When("یک ترم با عنوان ٰ نیمه دوم  ٰ " +
          "و حداکثر تعداد واحد ٰ 20 ٰ واحد را ثبت میکنم.")]
    private async Task When()
    {
        var dto = new CreateTermDtoBuilder()
            .WithTitle("نیمه دوم")
            .WithUnitCount(20)
            .Build();
        await _sut.Add(dto);
    }

    [Then("باید یک ترم با عنوان ٰ نیمه دوم ٰ " +
          "و حداکثر تعداد واحد ٰ 20 ٰ واحد وجود داشته باشد.")]
    private async Task Then()
    {
        var expected =await _context.Terms.SingleAsync();
        expected.Title.Should().Be("نیمه دوم");
        expected.UnitCount.Should().Be(20);
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