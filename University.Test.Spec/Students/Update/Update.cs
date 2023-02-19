using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Persistence.EF;
using University.Services.Students.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Students;
using Xunit;

namespace University.Test.Spec.Students.Update;

public sealed class Update : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly StudentService _sut;
    private Student _student;
    
    public Update(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.CreateService(ref _context);
    }

    [Given(" دانشجویی با نام ٰ محمد ٰ و نام خانوادگی ٰ حسینی ٰ و شماره همراه" +
           " ٰ 09164935625 ٰو شماره دانشجویی 123 در فهرست دانشجویان وجود دارد.")]
    private void Given()
    {
        _student = new StudentBuilder()
            .WithFirstName("محمد")
            .WithLastName("حسینی")
            .WithMobile("09164935625")
            .WithStudentNumber("123")
            .Build();
        _context.Save(_student);
    }

    [When("دانشجویی با نام ٰ محمد ٰ و نام خانوادگی ٰ حسینی ٰ و" +
          " شماره همراه ٰ 09164935625 ٰ‌و شماره دانشجویی 123" +
          " را به دانشجو با نام ٰ حسین ٰ و نام خانوادگی ٰ‌ حسینی ٰ و شماره دانشجویی 456 ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new UpdateStudentDtoBuilder()
            .WithFirstName("حسین")
            .WithLastName("حسینی")
            .WithStudentNumber("456")
            .Build();
        await _sut.Update(dto, _student.Id);
    }

    [Then("باید دانشجویی با نام ٰ‌حسین ٰ و نام خانوادگی ٰ‌حسینی ٰ و شماره همراه" +
          " ٰ09174935625ٰ ٰو شماره دانشجویی 456 وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Students.SingleAsync();
        expected.FirstName.Should().Be("حسین");
        expected.LastName.Should().Be("حسینی");
        expected.Mobile.Should().Be("09164935625");
        expected.StudentNumber.Should().Be("456");
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