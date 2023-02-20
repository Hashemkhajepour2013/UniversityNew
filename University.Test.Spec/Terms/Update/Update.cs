using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Persistence.EF;
using University.Services.Infrastructure;
using University.Services.Terms.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Terms;
using Xunit;

namespace University.Test.Spec.Terms.Update;

public sealed class Update : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly TermService _sut;
    private Term _term;
    
    public Update(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TermServiceFactory.CreateService(ref _context);
    }

    [Given("ترمی با عنوان ٰ نیمه دوم ٰ و حداکثر تعداد واحد ٰ 20 ٰ واحد وجود دارد.")]
    private void Given()
    {
        _term = new TermBuilder()
            .WithTitle("نیمه دوم")
            .WithUnitCount(20)
            .Build();
        _context.Save(_term);
    }

    [When("ترمی با عنوان ٰ نیمه دوم ٰ و حداکثر تعداد واحد ٰ 20 ٰ " +
          "واحد  را به ترمی با عنوان ٰ نیمه اول ٰ‌ و حداکثر " +
          "تعداد واحد ٰ 18 ٰ واحد ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new UpdateTermDtoBuilder()
            .WithTitle("نیمه اول")
            .WithUnitCount(18)
            .Build();
        await _sut.Update(dto, _term.Id);
    }

    [Then("باید یک ترم با عنوان ٰ نیمه اول ٰ و حداکثر" +
          " تعداد واحد ٰ 18 ٰ واحد وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Terms.SingleAsync();
        expected.Title.Should().Be("نیمه اول");
        expected.UnitCount.Should().Be(18);
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