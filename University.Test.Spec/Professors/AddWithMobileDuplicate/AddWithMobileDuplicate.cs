using FluentAssertions;
using University.Persistence.EF;
using University.Services.Documents.Exceptions;
using University.Services.Professors.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Professors;
using Xunit;

namespace University.Test.Spec.Professors.AddWithMobileDuplicate;

public sealed class AddWithMobileDuplicate : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly ProfessorService _sut;
    private Func<Task> _expected;
    
    public AddWithMobileDuplicate(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ProfessorServiceFactory.CreateService(ref _context);
    }

    [Given(" استادی با نام ٰ رضا ٰ و نام خانوادگی ٰ محمدی ٰ‌ و" +
           " شماره همراه ٰ‌ 09164932539 ‌ٰ‌ و شماره پرسنلی 123 در فهرست استادان وجود دارد.")]
    private void Given()
    {
        var professor = new ProfessorBuilder()
            .WithFirstName("رضا")
            .WithLastName("محمدی")
            .WithMobile("09164932539")
            .WithPersonnel("123")
            .Build();
        _context.Save(professor);
    }

    [When(" یک استاد با نام ٰ آرمان ٰ و نام خانوادگی ٰ احمدی " +
          "ٰ و شماره همراه ٰ 09164932539 ٰ و شماره پرسنلی 456 ثبت میکنم.")]
    private void When()
    {
        var dto = new CreateProfessorDtoBuilder()
            .WithFirstName("آرمان")
            .WithLastName("احمدی")
            .WithMobile("09164932539")
            .WithPersonnel("456")
            .Build();
        _expected = async () => await _sut.Add(dto);
    }

    [Then(" باید یک خطا با عنوان ٰ شماره همراه وارد شده قبلاً ثبت شده است ٰ رخ دهد.")]
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
            _ => When(),
            _ => Then().Wait());
    }
}