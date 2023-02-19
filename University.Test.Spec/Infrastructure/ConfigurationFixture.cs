using Microsoft.Extensions.Configuration;
using Xunit;

namespace University.Test.Spec.Infrastructure;

public class ConfigurationFixture
{
    public TestSettings Value { get; private set; }

    public ConfigurationFixture()
    {
        Value = GetSettings();
    }

    private TestSettings GetSettings()
    {
        var settings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, false)
            .AddEnvironmentVariables()
            .AddCommandLine(Environment.GetCommandLineArgs())
            .Build();
        
        var testSettings = new TestSettings();
        settings.Bind("PersistenceConfig", testSettings);
        return testSettings;
    }
    
    
}

public class TestSettings
{
    public string ConnectionString { get; set; }
}

[CollectionDefinition(nameof(ConfigurationFixture),
    DisableParallelization = false)]
public class
    ConfigurationCollectionFixture : ICollectionFixture<
        ConfigurationFixture>
{
}
