using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Persistence.EF;
using University.Services.Professors.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools.Professors;
using Xunit;

namespace University.Test.Spec.Professors.Add;

public sealed class Add : EFDataContextDatabaseFixture
{
    private readonly ProfessorService _sut;
    private readonly EFDataContext _context;
    
    public Add(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ProfessorServiceFactory.CreateService(ref _context);
    }
    
    [Given(" هیچ استادی در فهرست استادان وجود ندارد.")]
    private void Given()
    {
    }
    
    [When("یک استاد با نام ٰ رضا ٰ‌ و نام خانوادگی" +
          " ٰ محمدی ٰ‌ و شماره همراه ٰ‌ 09164932539 ٰ‌ و شماره پرسنلی 123 ثبت میکنم.")]
    private async Task When()
    {
        var dto = new CreateProfessorDtoBuilder()
            .WithFirstName("رضا")
            .WithLastName("محمدی")
            .WithMobile("09164932539")
            .WithPersonnel("123")
            .Build(); 
        await _sut.Add(dto);
    }

    [Then(" باید استادی با نام ٰ‌رضا ٰ و نام خانوادگی ٰ محمدی ٰ و شماره همراه " +
          "ٰ‌ 09164932539 ٰ‌و شماره پرسنلی 123 در فهرست استادان وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Professors.SingleAsync();
        expected.FirstName.Should().Be("رضا");
        expected.LastName.Should().Be("محمدی");
        expected.Mobile.Should().Be("09164932539");
        expected.PersonnelId.Should().Be("123");
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