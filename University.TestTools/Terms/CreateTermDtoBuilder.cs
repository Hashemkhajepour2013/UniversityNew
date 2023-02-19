using University.Services.Terms.Contracts.Dtos;

namespace University.TestTools.Terms;

public sealed class CreateTermDtoBuilder
{
    private AddTermDto _dto;

    public CreateTermDtoBuilder()
    {
        _dto = new AddTermDto
        {
            Title = "dummy",
            UnitCount = 20
        };
    }

    public CreateTermDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public CreateTermDtoBuilder WithUnitCount(byte unitCount)
    {
        _dto.UnitCount = unitCount;
        return this;
    }

    public AddTermDto Build()
    {
        return _dto;
    }
}