using API.Controllers;
using API.DTO.BookStore.Responses;
using BLL.Services.Interfaces;
using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace TestsUnitaires;


public class BookStoreControllerTests
{
   
    [Fact]
    public async Task GetBook_GoodId_GetBookDTOResponse200()
    {
        //Arrange
        int id = 1;
        IBookStoreService bookStoreService = Mock.Of<IBookStoreService>();
        var book =  new Domain.Entities.Book()
            {
                Id= 1,
                Title = "Title",
                Description = "Description",
                ISBN = "ISBN"
            };


        Mock.Get(bookStoreService)
            .Setup( (instance) => instance.RetreiveBookAsync(1))
            .ReturnsAsync(book);
        
        BookStoreController controller = new BookStoreController(bookStoreService);
        
        //Act
        IActionResult result = await controller.GetBook(id);

        //Assert
        result.Should().BeOfType<OkObjectResult>().And.NotBeNull(); 


        //Assert.IsType<OkObjectResult>(result); //200
        var okObjectResult = result as OkObjectResult;

        var expectedDTO = new GetBookDTOResponse()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN
        };


        okObjectResult.Value.Should().BeEquivalentTo(expectedDTO);

//        Assert.Equal(expectedDTO, okObjectResult.Value); // Check if good DTO inside response
    }



    [Fact]
    public async Task GetBook_BadId_NotFound()
    {
        //Arrange
        int id = 1;
        IBookStoreService bookStoreService = Mock.Of<IBookStoreService>();

        Mock.Get(bookStoreService)
            .Setup( (instance) => instance.RetreiveBookAsync(1))
            .ThrowsAsync(new NotFoundEntityException("Book", 1));

        BookStoreController controller = new BookStoreController(bookStoreService);
        
        //Act
        var action = () => controller.GetBook(id);

        //Assert
        await Assert.ThrowsAsync<NotFoundEntityException>(action); //404

    }

}