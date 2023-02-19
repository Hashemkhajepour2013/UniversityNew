using FluentAssertions;
using University.Entities;
using University.Persistence.EF;
using University.Services.Professors.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Professors;
using Xunit;

namespace University.Test.Spec.Professors.Delete;

public sealed class Delete : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly ProfessorService _sut;
    private Professor _professor;
    
    public Delete(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ProfessorServiceFactory.CreateService(ref _context);
    }

    [Given(" استادی با نام ٰ رضا ٰ و نام خانوادگی ٰ محمدی ٰ‌ و" +
           " شماره همراه ٰ‌ 09164932539 ‌ٰ‌ و شماره پرسنلی 123 در فهرست استادان وجود دارد.")]
    private void Given()
    {
        _professor = new ProfessorBuilder()
            .WithFirstName("رضا")
            .WithLastName("محمدی")
            .WithMobile("09164932539")
            .WithPersonnel("123")
            .Build();
        _context.Save(_professor);
    }

    [When("استادی با نام ٰ رضا ٰ و نام خانوادگی ٰ محمدی ٰ " +
          "و شماره همراه ٰ 09164932539 ٰ و شماره پرسنلی 123 را حذف میکنم.")]
    private async Task When()
    {
        await _sut.Delete(_professor.Id);
    } 

    [Then("نباید هیچ استادی با نام ٰ‌ رضا ٰ‌ و نام خانوادگی ٰ محمدی ٰ‌ و" +
          " شماره همراه ٰ 09164932539 ٰو شماره پرسنلی 123  در فهرست استادان وجود داشته باشد.")]
    private void Then()
    {
        _context.Professors.Should().BeNullOrEmpty();
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