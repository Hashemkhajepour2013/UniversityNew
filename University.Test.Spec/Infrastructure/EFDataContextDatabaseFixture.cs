using University.Persistence.EF;
using Xunit;

namespace University.Test.Spec.Infrastructure;

[Collection(nameof(ConfigurationFixture))]
public abstract class EFDataContextDatabaseFixture : DatabaseFixture
{
    readonly ConfigurationFixture _configuration;

    public EFDataContextDatabaseFixture(
        ConfigurationFixture configuration)
    {
        _configuration = configuration;
    }
    public EFDataContext CreateDataContext()
    {
        return new EFDataContext(
            _configuration.Value.ConnectionString);
    }
}