using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Services.Documents.Exceptions;
using University.Services.Professors.Contracts;
using University.Services.Professors.Extensions;
using University.Tests.Unit.Infrastructure;
using University.TestTools.Professors;
using Xunit;

namespace University.Tests.Unit.Professors;

public sealed class ProfessorServiceTests : BusinessUnitTest
{
    private readonly ProfessorService _sut;
    public ProfessorServiceTests()
    {
        var efDataContext = DbContext();
        _sut = ProfessorServiceFactory.CreateService(ref efDataContext);
    }

    [Fact]
    private async Task Add_add_professor_properly()
    {
        var dto = new CreateProfessorDtoBuilder()
            .WithFirstName("Professor-F")
            .WithLastName("Professor-L")
            .WithMobile("dummy-M")
            .WithPersonnel("123")
            .Build();

        await _sut.Add(dto);

        var expected = await DbContext().Professors.SingleAsync();
        expected.FirstName.Should().Be(dto.FirstName);
        expected.LastName.Should().Be(dto.LastName);
        expected.Mobile.Should().Be(dto.Mobile);
        expected.PersonnelId.Should().Be(dto.PersonnelId);
    }

    [Fact]
    private async Task Add_add_throw_exception_when_mobile_duplicate()
    {
        var professor = new ProfessorBuilder()
            .WithFirstName("F-dummy")
            .WithLastName("L-dummy")
            .WithMobile("M-dummy")
            .WithPersonnel("123")
            .Build();
        Save(professor);
        var dto = new CreateProfessorDtoBuilder()
            .WithFirstName("F-dummy1")
            .WithLastName("L-dummy1")
            .WithMobile("M-dummy")
            .WithPersonnel("456")
            .Build();

        var expected = async () => await _sut.Add(dto);

        await expected.Should()
            .ThrowExactlyAsync<MobileIsDuplicateException>();
    }

    [Fact]
    private async Task Update_update_professor_properly()
    {
        var teacher = new ProfessorBuilder()
            .WithFirstName("F-dummy")
            .WithLastName("L-dummy")
            .WithMobile("M-dummy")
            .WithPersonnel("123")
            .Build();
        Save(teacher);
        var dto = new UpdateProfessorDtoBuilder()
            .WithFirstName("Update-F")
            .WithLastName("Update-L")
            .WithPersonnel("456")
            .Build();

        await _sut.Update(dto, teacher.Id);

        var expected = await DbContext().Professors.SingleAsync();
        expected.FirstName.Should().Be(dto.FirstName);
        expected.LastName.Should().Be(dto.LastName);
        expected.PersonnelId.Should().Be(dto.PersonnelId);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_professor_not_found(int professorId)
    {
        var dto = new UpdateProfessorDtoBuilder().Build();

        var expected = async () => await _sut.Update(dto, professorId);

        await expected.Should()
            .ThrowExactlyAsync<ProfessorNotFoundException>();
    }

    [Fact]
    private async Task Delete_delete_professor_properly()
    {
        var professor = new ProfessorBuilder().Build();
        Save(professor);
        
        await _sut.Delete(professor.Id);

        DbContext().Professors.Should().HaveCount(0);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Delete_delete_throw_exception_when_professor_not_found(int professorId)
    {
        var expected = async () => await _sut.Delete(professorId);

        await expected.Should()
            .ThrowExactlyAsync<ProfessorNotFoundException>();
    }
}