using API.DTO.BookStore.Responses;
using System.Net.Http.Json;
using TestsIntegrations.Fixtures;

namespace TestsIntegrations;
public class BookStoreControllerTests : AbstractIntegrationTest
{
    public BookStoreControllerTests(APIWebApplicationFactory fixture) : base(fixture)
    {
    }


    [Fact]
    public async Task GetBook_IdValid_OkBookResponseDTO()
    {
        //Arrange (SendTOServer:RequestDTO, expected:ResponseDTO)
        
        //Valeurs de ma base de données test
        GetBookDTOResponse expected = new()
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            ISBN = "ISBN"
        };

        int idBook = expected.Id;
        //Act
        GetBookDTOResponse? result = await _client.GetFromJsonAsync<GetBookDTOResponse>($"/api/bookStore/books/{idBook}");

        //Assert
        Assert.Equal(expected, result);
    }
}
