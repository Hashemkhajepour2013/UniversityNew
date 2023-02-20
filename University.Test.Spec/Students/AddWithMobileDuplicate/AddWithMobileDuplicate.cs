using FluentAssertions;
using University.Persistence.EF;
using University.Services.Documents.Exceptions;
using University.Services.Infrastructure;
using University.Services.Students.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Students;
using Xunit;

namespace University.Test.Spec.Students.AddWithMobileDuplicate;

public class AddWithMobileDuplicate : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly StudentService _sut;
    private Func<Task> _expected;

    public AddWithMobileDuplicate(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.CreateService(ref _context);
    }

    [Given("دانشجویی با نام ٰ محمد ٰ و نام خانوادگی ٰ حسینی ٰ و شماره همراه " +
           "ٰ 09164935625 ٰ و شماره دانشجویی 123 در فهرست دانشجویان وجود دارد.")]
    private void Given()
    {
        var student = new StudentBuilder()
            .WithFirstName("محمد")
            .WithLastName("حسینی")
            .WithMobile("09164935625")
            .WithStudentNumber("123")
            .Build();
        _context.Save(student);
    }

    [When(" یک دانشجو با نام ٰ علی ٰ و نام خانوادگی ٰ‌ محمدی ٰ‌ " +
          "و شماره همراه ٰ 09164935625 ٰو شماره دانشجویی 456 ثبت میکنم.")]
    private async Task When()
    {
        var dto = new AddStudentDtoBuilder()
            .WithFirstName("علی")
            .WithLastName("محمدی")
            .WithMobile("09164935625")
            .WithStudentNumber("456")
            .Build();
        _expected = async () => await _sut.Add(dto);
    }

    [Then("باید یک خطا با عنوان ٰ‌ شماره همراه وارد شد قبلاً ثبت شده است ٰ‌ رخ دهد.")]
    private async Task Then()
    {
        await _expected.Should()
            .ThrowExactlyAsync<MobileIsDuplicateException>();
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