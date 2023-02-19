using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Classrooms.Contracts;
using University.Services.Classrooms.Extensions;
using University.Services.Lessons.Extensions;
using University.Services.Professors.Extensions;
using University.Services.Terms.Extensions;
using University.Tests.Unit.Infrastructure;
using University.TestTools.Classrooms;
using University.TestTools.Lessons;
using University.TestTools.Professors;
using University.TestTools.Terms;
using Xunit;

namespace University.Tests.Unit.Classrooms;

public sealed class ClassroomServiceTest : BusinessUnitTest
{
    private readonly ClassroomService _sut;
    
    public ClassroomServiceTest()
    {
        var efDataContext = DbContext();
        _sut = ClassroomServiceFactory.CreateService(ref efDataContext);
    }
    
    [Fact]
    private async Task Add_add_classroom_properly()
    {
        var term = new TermBuilder().Build();
        Save(term);
        var lesson = new LessonBuilder().Build();
        Save(lesson);
        var professor = new ProfessorBuilder().Build();
        Save(professor);
        var dto = new CreateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(20)
            .WithLesson(lesson.Id)
            .WithProfessor(professor.Id)
            .WithTerm(term.Id)
            .Build();

        await _sut.Add(dto);

        var expected = await DbContext().Classrooms.SingleAsync();
        expected.StartDate.Should().Be(dto.StartDate);
        expected.EndDate.Should().Be(dto.EndDate);
        expected.Capacity.Should().Be(dto.Capacity);
        expected.TermId.Should().Be(term.Id);
        expected.LessonId.Should().Be(lesson.Id);
        expected.ProfessorId.Should().Be(professor.Id);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Add_add_throw_exception_when_term_not_found(int termId)
    {
        var lesson = new LessonBuilder().Build();
        Save(lesson);
        var professor = new ProfessorBuilder().Build();
        Save(professor);
        var dto = new CreateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(20)
            .WithLesson(lesson.Id)
            .WithProfessor(professor.Id)
            .WithTerm(termId)
            .Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<TermNotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Add_add_throw_exception_when_lesson_not_found(int lessonId)
    {
        var term = new TermBuilder().Build();
        Save(term);
        var professor = new ProfessorBuilder().Build();
        Save(professor);
        Save(term);
        var dto = new CreateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(20)
            .WithLesson(lessonId)
            .WithProfessor(professor.Id)
            .WithTerm(term.Id)
            .Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<LessonNotFoundException>();
    }
    
    [Theory]
    [InlineData(-1)]
    private async Task Add_add_throw_exception_when_professor_not_found(int professorId)
    {
        var term = new TermBuilder().Build();
        Save(term);
        var lesson = new LessonBuilder().Build();
        Save(lesson);
        Save(term);
        var dto = new CreateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(20)
            .WithLesson(lesson.Id)
            .WithProfessor(professorId)
            .WithTerm(term.Id)
            .Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<LessonNotFoundException>();
    }


    [Fact]
    private async Task Add_add_throw_exception_when_start_date_expired()
    {
        var term = new TermBuilder().Build();
        var lesson = new LessonBuilder().Build();
        var dto = new CreateClassroomDtoBuilder()
            .WithStartDate(new DateTime(2022, 12, 01))
            .WithEndDate(DateTime.Now)
            .WithCapacity(20)
            .WithLesson(lesson.Id)
            .WithTerm(term.Id)
            .Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<StartDateExpiredException>();
    }

    [Fact]
    private async Task Add_add_throw_exception_when_end_date_smaller_start_date()
    {
        var term = new TermBuilder().Build();
        var lesson = new LessonBuilder().Build();
        var dto = new CreateClassroomDtoBuilder()
            .WithStartDate(DateTime.Now.AddDays(1))
            .WithEndDate(DateTime.Now)
            .WithCapacity(20)
            .WithLesson(lesson.Id)
            .WithTerm(term.Id)
            .Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<EndDateExpiredException>();
    }


    [Fact]
    private async Task Update_update_classroom_properly()
    {
        var term1 = CreateTerm("dummy1",18);
        var term2 = CreateTerm("dummy2", 20);
        var professor1 = CreateProfessor("mobile", "123");
        var professor2 = CreateProfessor("mobile1", "456");
        var lesson1 = CreateLesson("lesson1", 2);
        var lesson2 = CreateLesson("lesson2", 2);
        var classroom = new ClassroomBuilder()
            .WithTerm(term1.Id)
            .WithLesson(lesson1.Id)
            .WithProfessor(professor1.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(term2.Id)
            .WithLesson(lesson2.Id)
            .WithProfessor(professor2.Id)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

        await _sut.Update(dto, classroom.Id);

        var expected = await DbContext().Classrooms.SingleAsync();
        expected.TermId.Should().Be(term2.Id);
        expected.LessonId.Should().Be(lesson2.Id);
        expected.ProfessorId.Should().Be(professor2.Id);
        expected.StartDate.Should().Be(dto.StartDate);
        expected.EndDate.Should().Be(dto.EndDate);
        expected.Capacity.Should().Be(dto.Capacity);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_class_not_found(int classroomId)
    {
        var term = CreateTerm("dummy1",18);
        var professor = CreateProfessor("mobile", "123");
        var lesson = CreateLesson("lesson1", 2);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(term.Id)
            .WithLesson(lesson.Id)
            .WithProfessor(professor.Id)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

        var expected = async () => await _sut.Update(dto, classroomId);

        await expected.Should()
            .ThrowExactlyAsync<ClassroomNotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_lesson_not_found(int lessonId)
    {
        var term1 = CreateTerm("dummy1",18);
        var term2 = CreateTerm("dummy2", 20);
        var professor1 = CreateProfessor("mobile", "123");
        var professor2 = CreateProfessor("mobile1", "456");
        var lesson1 = CreateLesson("lesson1", 2);
        var classroom = new ClassroomBuilder()
            .WithTerm(term1.Id)
            .WithLesson(lesson1.Id)
            .WithProfessor(professor1.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(term2.Id)
            .WithLesson(lessonId)
            .WithProfessor(professor2.Id)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

        var expected = async () => await _sut.Update(dto, classroom.Id);

        await expected.Should()
            .ThrowExactlyAsync<LessonNotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_term_not_found(int termId)
    {
        var term1 = CreateTerm("dummy1",18);
        var professor1 = CreateProfessor("mobile", "123");
        var professor2 = CreateProfessor("mobile1", "456");
        var lesson1 = CreateLesson("lesson1", 2);
        var lesson2 = CreateLesson("lesson2", 2);
        var classroom = new ClassroomBuilder()
            .WithTerm(term1.Id)
            .WithLesson(lesson1.Id)
            .WithProfessor(professor1.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(termId)
            .WithProfessor(professor2.Id)
            .WithLesson(lesson2.Id)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

       var expected = async () => await _sut.Update(dto, classroom.Id);

       await expected.Should()
           .ThrowExactlyAsync<TermNotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_professor_not_found(int professorId)
    {
        var term1 = CreateTerm("dummy1",18);
        var term2 = CreateTerm("dummy2",20);
        var professor1 = CreateProfessor("mobile", "123");
        var lesson1 = CreateLesson("lesson1", 2);
        var lesson2 = CreateLesson("lesson2", 2);
        var classroom = new ClassroomBuilder()
            .WithTerm(term1.Id)
            .WithLesson(lesson1.Id)
            .WithProfessor(professor1.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(term2.Id)
            .WithProfessor(professorId)
            .WithLesson(lesson2.Id)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

        var expected = async () => await _sut.Update(dto, classroom.Id);

        await expected.Should()
            .ThrowExactlyAsync<ProfessorNotFoundException>();
    }
    
    [Fact]
    private async Task Update_update_throw_exception_when_start_date_expired()
    {
        var term1 = CreateTerm("dummy1",18);
        var term2 = CreateTerm("dummy2", 20);
        var professor1 = CreateProfessor("mobile", "123");
        var professor2 = CreateProfessor("mobile1", "456");
        var lesson1 = CreateLesson("lesson1", 2);
        var lesson2 = CreateLesson("lesson2", 2);
        var classroom = new ClassroomBuilder()
            .WithTerm(term1.Id)
            .WithLesson(lesson1.Id)
            .WithProfessor(professor1.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(term2.Id)
            .WithLesson(lesson2.Id)
            .WithProfessor(professor2.Id)
            .WithStartDate(new DateTime(2022, 12,01))
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

        var expected = async () => await _sut.Update(dto, classroom.Id);

        await expected.Should()
            .ThrowExactlyAsync<StartDateExpiredException>();
    }

    [Fact]
    private async Task Update_update_throw_exception_when_end_date_smaller_start_date()
    {
        var term1 = CreateTerm("dummy1",18);
        var term2 = CreateTerm("dummy2", 20);
        var professor1 = CreateProfessor("mobile", "123");
        var professor2 = CreateProfessor("mobile1", "456");
        var lesson1 = CreateLesson("lesson1", 2);
        var lesson2 = CreateLesson("lesson2", 2);
        var classroom = new ClassroomBuilder()
            .WithTerm(term1.Id)
            .WithLesson(lesson1.Id)
            .WithProfessor(professor1.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);
        var dto = new UpdateClassroomDtoBuilder()
            .WithTerm(term2.Id)
            .WithLesson(lesson2.Id)
            .WithProfessor(professor2.Id)
            .WithStartDate(DateTime.Now.AddHours(1))
            .WithEndDate(DateTime.Now)
            .WithCapacity(50)
            .Build();

        var expected = async () => await _sut.Update(dto, classroom.Id);

        await expected.Should()
            .ThrowExactlyAsync<EndDateExpiredException>();
    }

    [Fact]
    private async Task Delete_delete_classroom_properly()
    {
        var term = CreateTerm("dummy1",18);
        var lesson = CreateLesson("lesson1", 2);
        var professor = CreateProfessor("mobile", "123");
        var classroom = new ClassroomBuilder()
            .WithTerm(term.Id)
            .WithLesson(lesson.Id)
            .WithProfessor(professor.Id)
            .WithCapacity(20)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now)
            .Build(); 
        Save(classroom);

        await _sut.Delete(classroom.Id);

        DbContext().Classrooms.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Delete_delete_throw_exception_when_classroom_not_found(int classroomId)
    {
        var expected = async () => await _sut.Delete(classroomId);

        await expected.Should()
            .ThrowExactlyAsync<ClassroomNotFoundException>();
    }
    
    private Lesson CreateLesson(string title, byte coefficient)
    {
        var lesson = new LessonBuilder()
            .WithTitle(title)
            .WithCoefficient(coefficient)
            .Build();
        Save(lesson);
        return lesson;
    }
    
    private Term CreateTerm(string title, byte unitCount)
    {
        var term = new TermBuilder()
            .WithTitle(title)
            .WithUnitCount(unitCount)
            .Build();
        Save(term);
        return term;
    }
    
    private Professor CreateProfessor(string mobile, string PersonnelId)
    {
        var professor = new ProfessorBuilder()
            .WithFirstName("dummy-name")
            .WithLastName("dummy-lastname")
            .WithMobile(mobile)
            .WithPersonnel(PersonnelId)
            .Build();
        Save(professor);
        return professor;
    }
}