using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using University.Services.Terms.Contracts;
using University.Services.Terms.Extensions;
using University.Tests.Unit.Infrastructure;
using University.TestTools.Terms;
using Xunit;

namespace University.Tests.Unit.Terms;

public sealed class TermServiceTest : BusinessUnitTest
{
    private readonly TermService _sut;

    public TermServiceTest(TermService sut)
    {
        var efDataContext = DbContext();
        _sut = TermServiceFactory.CreateService(ref efDataContext);
    }
    
    [Fact]
    private async Task Add_add_term_properly()
    {
        var dto = new CreateTermDtoBuilder()
            .WithTitle("dummy")
            .WithUnitCount(20)
            .Build();

        await _sut.Add(dto);

        var expected = await DbContext().Terms.SingleAsync();
        expected.Title.Should().Be(dto.Title);
        expected.UnitCount.Should().Be(dto.UnitCount);  
    }
    
    [Fact]
    private async Task Update_update_term_properly()
    {
        var term = new TermBuilder()
            .WithTitle("dummy-T")
            .WithUnitCount(20)
            .Build();
        Save(term);
        var dto = new UpdateTermDtoBuilder()
            .WithTitle("dummy-E")
            .WithUnitCount(18)
            .Build();

        await _sut.Update(dto, term.Id);

        var expected = await DbContext().Terms.SingleAsync();
        expected.Id.Should().Be(term.Id);
        expected.Title.Should().Be(term.Title);
        expected.UnitCount.Should().Be(term.UnitCount);
    }

    [Theory]
    [InlineData(-1)]
    private async Task Update_update_throw_exception_when_term_not_found(int termId)
    {
        var dto = new UpdateTermDtoBuilder().Build();

        var expected = async () => await _sut.Update(dto, termId);

        await expected.Should()
            .ThrowExactlyAsync<TermNotFoundException>();
    }
    
    [Fact]
    private async Task Delete_delete_term_properly()
    {
        var term = new TermBuilder().Build();

        await _sut.Delete(term.Id);

        DbContext().Terms.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(-1)]
    private async Task Delete_delete_throw_exception_when_term_not_found(int termId)
    {
        var expected = async () => await _sut.Delete(termId);

        await expected.Should()
            .ThrowExactlyAsync<TermNotFoundException>();
    }
}