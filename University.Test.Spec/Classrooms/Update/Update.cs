using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Persistence.EF;
using University.Services.Classrooms.Contracts;
using University.Services.Classrooms.Contracts.Dtos;
using University.Services.Infrastructure;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Classrooms;
using University.TestTools.Lessons;
using University.TestTools.Professors;
using University.TestTools.Terms;
using Xunit;

namespace University.Test.Spec.Classrooms.Update;

public sealed class Update : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly ClassroomService _sut;
    private Lesson _lesson;
    private Classroom _classroom;
    private Term _term;
    private Professor _professor;
    private UpdateClassroomDto _dto;
    
    public Update(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ClassroomServiceFactory.CreateService(ref _context);
    }

    [Given(" ترمی با عنوان ٰ نیمه اول ٰ و حداکثر تعداد واحد ٰ 20 ٰ وجود دارد.")]
    [And(" استادی با نام ٰ مریم ٰ و نام خانوادگی ٰ رضایی ٰ‌ و " +
         "شماره همراه ٰ‌ 9164932626 ‌ٰ‌ و شماره پرسنلی 456 در فهرست استادان وجود دارد.")]
    private void Given()
    {
        _professor = new ProfessorBuilder()
            .WithFirstName("مریم")
            .WithLastName("رضایی")
            .WithMobile("9164932626")
            .WithPersonnel("456").Build();
        _context.Save(_professor);  
        _term = new TermBuilder()
            .WithTitle("نیمه اول")
            .WithUnitCount(20)
            .Build();
        _context.Save(_term);
    }

    [And("درسی با عنوان ٰ‌فیزیک ٰ و ضریب ٰ 3 ٰ وجود دارد.")]
    [And("کلاسی با حداکثر ظرفیت ٰ20 ٰ‌نفر و با  درس با عنوان  ٰ ریاضی ٰ‌ و تاریخ برگزاری" +
         " ٰ 1401/11/05 ٰ و ساعت شروع برگزاری ٰ 10:00 ٰ و ساعت پایان" +
         " ٰ 12:00 ٰ برای ترم با عنوان ٰ نیمه اول ٰ وجود دارد.")]
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
        _lesson = new LessonBuilder()
            .WithTitle("ٰ‌فیزیک")
            .WithCoefficient(3)
            .Build();
        _context.Save(_lesson);
        var lesson = new LessonBuilder()
            .WithTitle("ریاضی")
            .WithCoefficient(3)
            .Build();
        _context.Save(lesson);
        _classroom = new ClassroomBuilder()
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now.AddHours(2))
            .WithCapacity(20)
            .WithProfessor(professor.Id)
            .WithLesson(lesson.Id)
            .WithTerm(_term.Id)
            .Build();
        _context.Save(_classroom);
    }

    [When("کلاسی با حداکثر ظرفیت ٰ20 ٰ‌نفر و با درس با عنوان  ٰ ریاضی ٰ‌ و " +
          "استاد با شماره پرسنلی 123 و تاریخ برگزاری ٰ 1401/11/05 ٰو  " +
          "با ساعت شروع برگزاری ٰ 10:00 ٰ و ساعت پایان ٰ 12:00 ٰ را به کلاس با حداکثر ظرفیت " +
          "ٰ 10 ٰ نفر و با درس با عنوان  ٰ فیزیک ٰ و استاد با شماره پرسنلی 456 و تاریخ برگزاری ٰ 1401/11/10 ٰو با" +
          " ساعت شروع برگزاری ٰ 12:00 ٰ و ساعت پایان ٰ 14:00 ٰ  ویرایش میکنم.")]
    private async Task When()
    {
        _dto = new UpdateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now.AddHours(1))
            .WithEndDate(DateTime.Now.AddHours(3))
            .WithCapacity(20)
            .WithTerm(_term.Id)
            .WithProfessor(_professor.Id)
            .WithLesson(_lesson.Id)
            .Build();
        await _sut.Update(_dto, _classroom.Id);
    }

    [Then(" باید یک کلاس با حداکثر ظرفیت ٰ10 ٰ‌نفر و با استاد با نام ٰ‌  مریم ٰ‌ " +
          "و نام خانوادگی ٰ‌ رضایی  ٰ‌و شماره پرسنلی 456 و با درس با عنوان  ٰ فیزیک ٰ‌ و تاریخ برگزاری ٰ 1401/11/10 " +
          "ٰو با ساعت شروع برگزاری ٰ 12:00 ٰ و ساعت پایان ٰ 14:00 ٰ وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Classrooms.SingleAsync();
        expected.StartDate.Should().Be(_dto.StartDate);
        expected.EndDate.Should().Be(_dto.EndDate);
        expected.Capacity.Should().Be(20);
        expected.TermId.Should().Be(_term.Id);
        expected.LessonId.Should().Be(_lesson.Id);
        expected.ProfessorId.Should().Be(_professor.Id);
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