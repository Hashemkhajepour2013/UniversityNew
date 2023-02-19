using University.Services.Terms.Contracts.Dtos;

namespace University.TestTools.Terms;

public sealed class UpdateTermDtoBuilder
{
    private UpdateTermDto _dto;

    public UpdateTermDtoBuilder()
    {
        _dto = new UpdateTermDto
        {
            Title = "dummy",
            UnitCount = 20
        };
    }

    public UpdateTermDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public UpdateTermDtoBuilder WithUnitCount(byte unitCount)
    {
        _dto.UnitCount = unitCount;
        return this;
    }

    public UpdateTermDto Build()
    {
        return _dto;
    }
}