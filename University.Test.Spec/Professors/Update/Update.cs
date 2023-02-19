using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Persistence.EF;
using University.Services.Professors.Contracts;
using University.Services.Professors.Contracts.Dtos;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Professors;
using Xunit;

namespace University.Test.Spec.Professors.Update;

public sealed class Update : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly ProfessorService _sut;
    private Professor _professor;
    private UpdateProfessorDto _dto;
    
    public Update(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ProfessorServiceFactory.CreateService(ref _context);
    }

    [Given(" استادی با نام ٰ رضا ٰ و نام خانوادگی ٰ محمدی ٰ‌ و " +
           "شماره همراه ٰ‌ 09164932539 ‌ٰ‌ و شماره پرسنلی 123 در فهرست استادان وجود دارد.")]
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

    [When(" استادی با نام ٰ‌ رضا ٰ و نام خانوادگی ٰ محمدی ٰ و" +
          " شماره همراه ٰ 09164932539 ٰ و شماره پرسنلی 456 را به استاد با" +
          " نام ٰ محمد ٰ‌ و نام خانوادگی ٰ‌ ژیان پور ٰ ویرایش میکنم.")]
    private async Task When()
    {
        _dto = new UpdateProfessorDtoBuilder()
            .WithFirstName("محمد")
            .WithLastName("ژیان پور")
            .WithPersonnel("456")
            .Build();
       await _sut.Update(_dto, _professor.Id);
    }

    [Then("باید استادی با نام ٰ محمد ٰ‌و نام خانوادگی ٰ‌ ژیان پور ٰ‌ و " +
          "شماره همراه ٰ‌ 09164932539 ٰوشماره پرسنلی 456 در فهرست استادان وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Professors.SingleAsync();
        expected.FirstName.Should().Be("محمد");
        expected.LastName.Should().Be("ژیان پور");
        expected.Mobile.Should().Be("09164932539");
        expected.Mobile.Should().Be("456");
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