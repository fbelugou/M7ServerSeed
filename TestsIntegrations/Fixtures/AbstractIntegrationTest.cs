[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace TestsIntegrations.Fixtures;
public abstract class AbstractIntegrationTest : IClassFixture<APIWebApplicationFactory>
{
    protected HttpClient _client;

    protected AbstractIntegrationTest(APIWebApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
    }

    // Note : Utilities methods 
    // exemple : Login, Logout, PopulateDatabase, CleanDatabase, etc.

}
