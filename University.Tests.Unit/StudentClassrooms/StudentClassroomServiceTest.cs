using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Entities;
using University.Services.Classrooms.Extensions;
using University.Services.StudentClassrooms.Contracts;
using University.Services.StudentClassrooms.Extensions;
using University.Services.Students.Extensions;
using University.Tests.Unit.Infrastructure;
using University.TestTools.Classrooms;
using University.TestTools.Lessons;
using University.TestTools.StudentClassrooms;
using University.TestTools.Students;
using University.TestTools.Terms;
using Xunit;

namespace University.Tests.Unit.StudentClassrooms;

public sealed class StudentClassroomServiceTest : BusinessUnitTest
{
    private readonly StudentClassroomService _sut;
    public StudentClassroomServiceTest()
    {
        var efDataContext = DbContext();
        _sut = StudentClassroomServiceFactory.CreateService(ref efDataContext);
    }
    
    [Fact]
    private async Task AddClass_add_student_to_class_properly()
    {
        var term = CreateTerm();
        var lesson = CreateLesson();
        var student = CreateStudent();
        var classroom = CreateClassroom(term, lesson);
        var dto = new AddStudentClassroomDtoBuilder()
            .WithClassroom(classroom.Id)
            .WithStudent(student.Id).Build();

        await _sut.Add(dto);

        var expected = await DbContext().StudentClassrooms
            .Include(_ => _.Student)
            .SingleAsync();
        expected.Student.FirstName.Should().Be(student.FirstName);
        expected.Student.LastName.Should().Be(student.LastName);
        expected.ClassroomId.Should().Be(classroom.Id);
        expected.StudentId.Should().Be(student.Id);
    }

    [Fact]
    private async Task AddClass_add_student_to_class_throw_exception_when_time_interference()
    {
        var term = CreateTerm();
        var lesson = CreateLesson();
        var student = CreateStudent();
        var classroom = CreateClassroom(term, lesson); 
        CreateClassroom(term, lesson);
        var dto = new AddStudentClassroomDtoBuilder()
            .WithClassroom(classroom.Id)
            .WithStudent(student.Id).Build();

         var expected = async () => await _sut.Add(dto);

         await expected.Should()
             .ThrowExactlyAsync<ClassroomTimeInterferenceException>();
    }

    [Fact]
    private async Task AddClass_add_student_to_class_throw_exception_when_completed_capacity()
    {
        var term = CreateTerm();
        var lesson = CreateLesson();
        var student1 = CreateStudent();
        var student2 = CreateStudent();
        var student3 = CreateStudent();
        var classroom = CreateClassroom(term, lesson);
        await CreateStudentClassroom(student1, classroom);
        await CreateStudentClassroom(student2, classroom);
        var dto = new AddStudentClassroomDtoBuilder()
            .WithClassroom(classroom.Id)
            .WithStudent(student3.Id).Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<ClassroomCompletedCapacityException>();
    }

    [Theory]
    [InlineData(-1)]
    private async Task AddClass_add_student_to_class_throw_exception_when_student_not_found(int studentId)
    {
        var term = CreateTerm();
        var lesson = CreateLesson();
        var classroom = CreateClassroom(term, lesson);
        var dto = new AddStudentClassroomDtoBuilder()
            .WithStudent(studentId)
            .WithClassroom(classroom.Id).Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<StudentNotFoundException>();
    }

    [Theory]
    [InlineData(-1)]
    private async Task AddClass_add_student_to_class_throw_exception_when_class_not_found(int classroomId)
    {
        var student = CreateStudent();
        var dto = new AddStudentClassroomDtoBuilder()
            .WithClassroom(classroomId)
            .WithStudent(student.Id).Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<ClassroomNotFoundException>();
    }

    [Fact]
    private async Task Delete_delete_student_form_class()
    {
        var term = CreateTerm();
        var lesson = CreateLesson();
        var student = CreateStudent();
        var classroom = CreateClassroom(term, lesson);
        var studentClassroom =await CreateStudentClassroom(student, classroom);

        await _sut.Delete(studentClassroom.Id);
        
        DbContext().StudentClassrooms.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Delete_delete_throw_exception_when_student_class_room_not_found(
        int studentClassroomId)
    {
        var expected = async () => await _sut.Delete(studentClassroomId);

        await expected.Should()
            .ThrowExactlyAsync<StudentClassroomNotFoundException>();
    }

 

    private async Task<StudentClassroom> CreateStudentClassroom(Student student, Classroom classroom)
    {
        var studentClassroom = new StudentClassroomBuilder()
            .WithStudent(student.Id)
            .WithClassroom(classroom.Id).Build();
        Save(studentClassroom);
        return studentClassroom;
    }

    private Classroom CreateClassroom(Term term, Lesson lesson)
    {
        var classroom = new ClassroomBuilder()
            .WithTerm(term.Id)
            .WithLesson(lesson.Id)
            .WithCapacity(2)
            .WithStartDate(DateTime.Now)
            .WithEndDate(DateTime.Now).Build();
        Save(classroom);
        return classroom;
    }

    private Student CreateStudent()
    {
        var student = new StudentBuilder()
            .WithFirstName("dummy-F")
            .WithLastName("dummy-L")
            .WithMobile("dummy-m").Build();
        Save(student);
        return student;
    }

    private Lesson CreateLesson()
    {
        var lesson = new LessonBuilder()
            .WithTitle("lesson-T")
            .WithCoefficient(3).Build();
        Save(lesson);
        return lesson;
    }

    private Term CreateTerm()
    {
        var term = new TermBuilder()
            .WithTitle("term-T")
            .WithUnitCount(20).Build();
        Save(term);
        return term;
    }
}