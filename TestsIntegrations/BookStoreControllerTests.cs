using API.DTO.BookStore.Requests;
using API.DTO.BookStore.Responses;
using Domain.Entities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
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
        await LogIn("admin", "admin");
        //Act
        GetBookDTOResponse? result = await _client.GetFromJsonAsync<GetBookDTOResponse>($"/api/bookStore/books/{idBook}");

        //Clean 
        LogOut();

        //Assert
        Assert.Equal(expected, result);

    }



    [Fact]
    public async Task GetBook_IdInvalid_OkBookResponseDTO()
    {
        //Arrange
        int id =999999;
        //Act
        Func<Task> action =  async () => await _client.GetFromJsonAsync<GetBookDTOResponse>($"/api/bookStore/books/{id}");

        //Assert
        var exeption = await Assert.ThrowsAsync<HttpRequestException>(action);


        Assert.Equal(HttpStatusCode.NotFound, exeption.StatusCode);
    }


    [Fact]
    public async Task CreateBook_CreateBookDTORequestValid_OKCreateBookDTOResponse()
    {
        //Arrange
        CreateBookDTORequest request = new()
        {
            Title = "Title",
            Description = "Description",
            ISBN = "ISBN"
        };


        //Act
        //Envoi et on sérialise la request
        HttpResponseMessage rep = await _client.PostAsJsonAsync("/api/bookStore/books", request);

        //Assert
        Assert.Equal(HttpStatusCode.Created, rep.StatusCode); //201 Created

        //Deserialize the response
        CreateBookDTOResponse? content = await rep.Content.ReadFromJsonAsync<CreateBookDTOResponse>();

        Assert.NotNull(content);

        Assert.True(content.Id > 0);
        Assert.Equal(request.Title, content.Title);
        Assert.Equal(request.Description, content.Description);
        Assert.Equal(request.ISBN, content.ISBN);
    }
}
