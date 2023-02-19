using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Persistence.EF;
using University.Services.Students.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools.Students;
using Xunit;

namespace University.Test.Spec.Students.Add;

public sealed class Add : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly StudentService _sut;
    
    public Add(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.CreateService(ref _context);
    }

    [Given("هیچ دانشجویی در فهرست دانشجویان وجود ندارد.")]
    private void Given()
    {
    }

    [When("یک دانشجو با نام ٰ محمد ٰ و نام خانوادگی ٰ " +
          "حسینی ٰ و شماره همراه ٰ 09164935625 ٰ و شماره دانشجویی 123 ثبت میکنم.")]
    private async Task When()
    {
        var dto = new AddStudentDtoBuilder()
            .WithFirstName("محمد")
            .WithLastName("حسینی")
            .WithMobile("09164935625")
            .WithStudentNumber("123")
            .Build();
        await _sut.Add(dto);
    }

    [Then("باید دانشجویی با نام ٰ محمد ٰ و نام خانوادگی ٰ حسینی ٰ و شماره همراه " +
          "ٰ 09164935625 ٰ  و شماره دانشجویی 123 در فهرست دانشجویان  وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Students.SingleAsync();
        expected.FirstName.Should().Be("محمد");
        expected.LastName.Should().Be("حسینی");
        expected.Mobile.Should().Be("09164935625");
        expected.StudentNumber.Should().Be("123");
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