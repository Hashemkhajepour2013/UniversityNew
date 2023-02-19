using University.Entities;

namespace University.TestTools.Terms;

public sealed class TermBuilder
{
    private Term _term;

    public TermBuilder()
    {
        _term = new Term
        {
            Title = "dummy",
            UnitCount = 20
        };
    }

    public TermBuilder WithTitle(string title)
    {
        _term.Title = title;
        return this;
    }

    public TermBuilder WithUnitCount(byte unitCount)
    {
        _term.UnitCount = unitCount;
        return this;
    }

    public Term Build()
    {
        return _term;
    }
}