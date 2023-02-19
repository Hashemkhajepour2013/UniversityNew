using System.Linq.Expressions;
using University.Services.Infrastructure;

namespace University.Test.Spec
{
    public static class Runner
    {
        public static void RunScenario(
            params Expression<Action<object>>[] steps)
        {
            var textContext = new
            {
                //...
            };
            steps.Select(_ => _.Compile())
                .ForEach(_ => _.Invoke(textContext));
        }
    }

    public class Feature : Attribute
    {
        public string? Title { get; set; }
        public string InOrderTo { get; set; }
        public string AsA { get; set; }
        public string IWantTo { get; set; }

        public Feature(string? title = null)
        {
            Title = title;
        }
    }

    public class Scenario : Attribute
    {
        public string Title { get; set; }

        public Scenario(string title)
        {
            Title = title;
        }
    }

    public class Story : Attribute
    {
        public string Title { get; set; }
        public string InOrderTo { get; set; }
        public string AsA { get; set; }
        public string IWantTo { get; set; }

        public Story(string title)
        {
            Title = title;
        }
    }

    public class Given : Attribute
    {
        public string Description { get; set; }

        public Given(string description)
        {
            Description = description;
        }
    }

    public class When : Attribute
    {
        public string Description { get; set; }

        public When(string description)
        {
            Description = description;
        }
    }

    public class Then : Attribute
    {
        public string Description { get; set; }

        public Then(string description)
        {
            Description = description;
        }
    }

    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = true,
        Inherited = true)]
    public class And : Attribute
    {
        public And(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}