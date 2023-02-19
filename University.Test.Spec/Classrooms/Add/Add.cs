using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Persistence.EF;
using University.Services.Classrooms.Contracts;
using University.Services.Classrooms.Contracts.Dtos;
using University.Test.Spec.Infrastructure;
using University.TestTools;
using University.TestTools.Classrooms;
using University.TestTools.Lessons;
using University.TestTools.Professors;
using University.TestTools.Terms;
using Xunit;

namespace University.Test.Spec.Classrooms.Add;

public sealed class  Add : EFDataContextDatabaseFixture
{
    private readonly EFDataContext _context;
    private readonly ClassroomService _sut;
    private Term _term;
    private Lesson _lesson;
    private Professor _professor;
    private AddClassroomDto _dto;
    
    public Add(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = ClassroomServiceFactory.CreateService(ref _context);
    }

    [Given(" هیچ کلاسی در فهرست کلاس‌ها وجود ندارد.")]
    [And("درسی با عنوان ٰ ریاضی ٰ و ضریب ٰ2 ٰ وجود دارد.")]
    [And(" استادی با نام ٰ رضا ٰ و نام خانوادگی ٰ محمدی ٰ‌ و " +
           "شماره همراه ٰ‌ 09164932539 ‌ٰ‌ و شماره پرسنلی 123 در فهرست استادان وجود دارد.")]
    [And(" ترمی با عنوان ٰ نیمه اول ٰ و حداکثر تعداد واحد ٰ 20 ٰ واحد وجود دارد.")]
    private void Given()
    {
        _term = new TermBuilder()
            .WithTitle("نیمه اول")
            .WithUnitCount(20)
            .Build();
        _context.Save(_term);
        _professor = new ProfessorBuilder()
            .WithFirstName("رضا")
            .WithLastName("محمدی")
            .WithMobile("9164932539")
            .WithPersonnel("123").Build();
        _context.Save(_professor);
        _lesson = new LessonBuilder()
            .WithTitle("ریاضی")
            .WithCoefficient(2)
            .Build();
        _context.Save(_lesson);
    }

    [When("یک کلاس با حداکثر ظرفیت ٰ 20 ٰ نفر و با درس با عنوان  ٰ ریاضی ٰ و " +
          "استاد با شماره پرسنلی 123 و تاریخ برگزاری ٰ 1401/11/05 ٰ" +
          "و ساعت شروع برگزاری ٰ 10:00 ٰ و ساعت پایان   ٰ12:00 ٰ برای ترم با عنوان ٰ نیمه اول ٰ   ثبت میکنم.")]
    private async Task When()
    {
        _dto = new CreateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now.AddHours(1))
            .WithEndDate(DateTime.Now.AddHours(3))
            .WithCapacity(20)
            .WithTerm(_term.Id)
            .WithLesson(_lesson.Id)
            .WithProfessor(_professor.Id)
            .Build();
        await _sut.Add(_dto);
    }

    [Then("باید یک کلاس با حداکثر ظرفیت ٰ20 ٰ‌نفر  و با درس با عنوان  ٰ ریاضی ٰ‌ " +
          "و استاد با شماره پرسنلی 123 و تاریخ برگزاری ٰ 1401/11/05 ٰ و" +
          " ساعت شروع برگزاری ٰ 10:00 ٰ و ساعت پایان  ٰ 12:00 ٰبرای ترمی با عنوان ٰ نیمه اول ٰ  وجود داشته باشد.")]
    private async Task Then()
    {
        var expected = await _context.Classrooms
            .SingleAsync();
        expected.Capacity.Should().Be(20);
        expected.StartDate.Should().Be(_dto.StartDate);
        expected.EndDate.Should().Be(_dto.EndDate);
        expected.TermId.Should().Be(_term.Id);
        expected.ProfessorId.Should().Be(_professor.Id);
        expected.LessonId.Should().Be(_lesson.Id);
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