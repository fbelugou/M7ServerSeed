using API.DTO.Account.Rerquests;
using API.DTO.Account.Responses;
using Domain.Exceptions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

    public void LogOut()
    {
        _client.DefaultRequestHeaders.Authorization = null;
    }

    public async Task LogIn(string username, string password)
    {
        // Requete HTTP
        var response = await _client.PostAsJsonAsync("/api/account", new LogInDTORequest()
        {
            password = password,
            username = username
        });

        if(response.IsSuccessStatusCode) {
            var logInResponseDTO = await response.Content.ReadFromJsonAsync<LogInDTOResponse>();
        
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", logInResponseDTO.access_token);
        }
        else
        {
            throw new LogInException(username);
        }
    }

}
