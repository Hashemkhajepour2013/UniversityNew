using FluentAssertions;
using University.Entities;
using University.Persistence.EF;
using University.Services.Students.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Students;
using Xunit;

namespace University.Test.Spec.Students.Delete;

public class Delete : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly StudentService _sut;
    private Student _student;
    
    public Delete(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.CreateService(ref _context);
    }

    [Given(" دانشجویی با نام ٰ محمد ٰ و نام خانوادگی ٰ حسینی ٰ و شماره همراه" +
           " ٰ 09164935625 ٰوشماره دانشجویی 123 در فهرست دانشجویان وجود دارد.")]
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

    [When(" دانشجویی با نام ٰ محمد ٰ‌ و نام خانوادگی ٰ حسینی ٰ " +
          "و شماره همراه ٰ‌ 09164935625 ٰ‌ و شماره دانشجویی 123 را حذف میکنم.")]
    private async Task When()
    {
        await _sut.Delete(_student.Id);
    }

    [Then(" نباید هیچ دانشجویی با نام ٰ‌ محمد ٰ‌ و نام خانوادگی ٰ‌ حسینی ٰ " +
          "و شماره همراه ٰ 09164935625 ٰو شماره دانشجویی 123 وجود داشته باشد.")]
    private void Then()
    {
        _context.Students.Should().BeNullOrEmpty();
    }
    
    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When() .Wait(),
            _ => Then());
    }
}