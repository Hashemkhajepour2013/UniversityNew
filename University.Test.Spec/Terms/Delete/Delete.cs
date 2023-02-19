using FluentAssertions;
using University.Entities;
using University.Persistence.EF;
using University.Services.Terms.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Terms;
using Xunit;

namespace University.Test.Spec.Terms.Delete;

public sealed class Delete : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly TermService _sut;
    private Term _term;
    
    public Delete(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TermServiceFactory.CreateService(ref _context);
    }

    [Given(" ترمی با عنوان ٰ نیمه دوم ٰ و حداکثر تعداد واحد ٰ 20 ٰ واحد وجود دارد.")]
    private void Given()
    {
        _term = new TermBuilder()
            .WithTitle("نیمه دوم")
            .WithUnitCount(20)
            .Build();
        _context.Save(_term);
    }

    [When("ترمی با عنوان ٰ نیمه دوم ٰ و حداکثر تعداد واحد ٰ 20 ٰ واحد را حذف میکنم.")]
    private async Task When()
    {
        await _sut.Delete(_term.Id);
    }

    [Then("نباید ترمی با عنوان ٰ نیمه دوم ٰ وحداکثر تعداد واحد ٰ 20 ٰ واحد وجود داشته باشد.")]
    private void Then()
    {
        _context.Terms.Should().BeNullOrEmpty();
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