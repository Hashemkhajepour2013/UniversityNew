using FluentAssertions;
using University.Entities;
using University.Persistence.EF;
using University.Services.Classrooms.Contracts;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Classrooms;
using University.TestTools.Lessons;
using University.TestTools.Professors;
using University.TestTools.Terms;
using Xunit;

namespace University.Test.Spec.Classrooms.Delete;

public sealed class Delete : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly ClassroomService _sut;
    private Term _term;
    private Classroom _classroom;
    
    public Delete(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ClassroomServiceFactory.CreateService(ref _context);
    }

    [Given(" ترمی با عنوان ٰ نیمه اول ٰ و با حداکثر تعداد واحد ٰ 20 ٰ واحد وجود دارد.")]
    private void Given()
    {
        _term = new TermBuilder()
            .WithTitle("نیمه اول")
            .WithUnitCount(20).Build();
        _context.Save(_term);
    }

    [And("کلاسی با حداکثر ظرفیت ٰ20 ٰ‌نفر  و با درس با عنوان  ٰ ریاضی ٰ‌ و تاریخ برگزاری " +
         "ٰ 1401/11/05 ٰ و با ساعت شروع برگزاری ٰ 10:00 ٰ و ساعت پایان " +
         "ٰ 12:00 ٰ و برای ترمی با عنوان ٰ نیمه اول ٰ وجود دارد.")]
    [And(" استادی با نام ٰ رضا ٰ و نام خانوادگی ٰ محمدی ٰ‌ و " +
         "شماره همراه ٰ‌ 09164932539 ‌ٰ‌ و شماره پرسنلی 123 در فهرست استادان وجود دارد.")]
    private void And()
    {
        var professor = new ProfessorBuilder()
            .WithFirstName("رضا")
            .WithLastName("محمدی")
            .WithMobile("9164932539")
            .WithPersonnel("123").Build();
        _context.Save(professor);
        var lesson = new LessonBuilder()
            .WithTitle("ریاضی")
            .WithCoefficient(2).Build();
        _context.Save(lesson);
        _classroom = new ClassroomBuilder()
            .WithTerm(_term.Id)
            .WithLesson(lesson.Id)
            .WithCapacity(20)
            .WithProfessor(professor.Id)
            .WithStartDate(DateTime.Now.AddHours(1))
            .WithEndDate(DateTime.Now.AddHours(3))
            .Build();
        _context.Save(_classroom);
    }

    [When(" کلاس با حداکثر ظرفیت ٰ 20 ٰ نفر و با درس با عنوان  ٰ ریاضی ٰ " +
          "و استاد با شماره پرسنلی 123  و تاریخ برگزاری" +
          " ٰ 1401/11/05 ٰ و با ساعت شروع برگزاری ٰ10:00 ٰ و ساعت پایان" +
          " ٰ 12:00 ٰ از ترم با عنوان ٰ نیمه اول ٰ حذف میکنیم.")]
    private async Task When()
    {
        await _sut.Delete(_classroom.Id);
    }

    [Then(" نباید هیچ کلاسی با درس با عنوان  ٰ ریاضی ٰ برای ترمی با عنوان ٰ نیمه اول ٰ  وجود داشته باشد.")]
    private async Task Then()
    {
        _context.Classrooms.Should().BeNullOrEmpty();
    }
    
    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => And(),
            _ => When().Wait(),
            _ => Then().Wait());
    }
}