using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Services.Documents.Exceptions;
using University.Services.Students.Contracts;
using University.Services.Students.Extensions;
using University.Tests.Unit.Infrastructure;
using University.TestTools.Students;
using Xunit;

namespace University.Tests.Unit.Students;

public sealed class StudentServiceTests : BusinessUnitTest
{
    private readonly StudentService _sut;

    public StudentServiceTests()
    {
        var efDataContext = DbContext();
        _sut = StudentServiceFactory.CreateService(ref efDataContext);
    }
    
    [Fact]
    private async Task Add_add_student_properly()
    {
        var dto = new AddStudentDtoBuilder()
            .WithFirstName("dummy-F")
            .WithLastName("dummy-L")
            .WithMobile("dummy-M")
            .WithStudentNumber("123")
            .Build();

        await _sut.Add(dto);

        var expected = await DbContext().Students.SingleAsync();
        expected.FirstName.Should().Be(dto.FirstName);
        expected.LastName.Should().Be(dto.LastName);
        expected.Mobile.Should().Be(dto.Mobile);
        expected.StudentNumber.Should().Be(dto.StudentNumber);
    }

    [Fact]
    private async Task Add_add_throw_exception_when_mobile_is_duplicate()
    {
        var student = new StudentBuilder()
            .WithFirstName("Dummy-F")
            .WithLastName("Dummy-L")
            .WithMobile("Dummy-M")
            .WithStudentNumber("123")
            .Build();
        Save(student);
        var dto = new AddStudentDtoBuilder()
            .WithFirstName("dummy-F")
            .WithLastName("dummy-L")
            .WithMobile("Dummy-M")
            .WithStudentNumber("456")
            .Build();
        
        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<MobileIsDuplicateException>();
    }
    
    [Fact]
    private async Task Update_update_student_properly()
    {
        var student = new StudentBuilder()
            .WithFirstName("Dummy-F")
            .WithLastName("Dummy-L")
            .WithMobile("Dummy-M")
            .WithStudentNumber("123")
            .Build();
        Save(student);
        var dto = new UpdateStudentDtoBuilder()
            .WithFirstName("Dummy-F-U")
            .WithLastName("Dummy-L-U")
            .WithStudentNumber("456")
            .Build();

        await _sut.Update(dto, student.Id);

        var expected = await DbContext().Students.SingleAsync();
        expected.FirstName.Should().Be(dto.FirstName);
        expected.LastName.Should().Be(dto.LastName);
        expected.StudentNumber.Should().Be(dto.StudentNumber);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_student_not_found(
        int studentId)
    {
        var dto = new UpdateStudentDtoBuilder().Build();

        var expected = async () => await _sut.Update(dto, studentId);

        await expected.Should()
            .ThrowExactlyAsync<StudentNotFoundException>();
    }

    [Fact]
    private async Task Delete_delete_student_properly()
    {
        var student = new StudentBuilder()
            .WithFirstName("dummy-F")
            .WithLastName("dummy-L")
            .WithMobile("dummy-M")
            .WithStudentNumber("123")
            .Build();
        Save(student);

        await _sut.Delete(student.Id);

        DbContext().Students.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Delete_delete_throw_exception_when_student_not_found(int id)
    {
        var expected = async () => await _sut.Delete(id);

        await expected.Should().ThrowExactlyAsync<StudentNotFoundException>();
    }
}