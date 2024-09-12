using API.DTO.BookStore.Requests;
using API.DTO.BookStore.Responses;
using BLL.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BookStoreController : APIBaseController
{
    private readonly IBookStoreService _bookStoreService;

    public BookStoreController(IBookStoreService bookStoreService)
    {
        _bookStoreService = bookStoreService;
    }


    // NOTE : THESE METHODS ARE IMPLEMENTED FOR EXEMPLES
    //
    // Normally, these methods should be implemented with 3 steps:
    // 1. Verify the RequestDTO of client application if needed with the corresponding Validator<T>() ! [FLuentValidation] 
    // 2. Call the LOGIC layer (BLL) to execute the business operations
    // 3. Return the good ResponseDTO to the client application (Exception, Result, etc.)

    [Route("books")]
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        // I) Verify the request

        //  Nothing here !

        // II) Call the BLL for the business operations

        IEnumerable<Book> books = await _bookStoreService.RetreiveBooksAsync();

        // III) Return the DTO response 
        GetAllBooksDTOResponse response = new()
        {
            Books = books.Select(book => new GetAllBooksDTOResponse.GetAllBookItemResponse
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN
            })
        };
        return Ok(response); // 200 OK
    }

    [Route("books/{idBook}")]
    [HttpGet]
    public async Task<IActionResult> GetBook(int idBook)
    {
        // I) Verify the request

        //  Nothing here !

        // II) Call the BLL for the business operations

        Book book = await _bookStoreService.RetreiveBookAsync(idBook);

        // III) Return the DTO response 
        GetBookDTOResponse response = new()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN
        };
        return Ok(response); // 200 OK
    }

    [Route("books")]
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDTORequest request, IValidator<CreateBookDTORequest> validator)
    {
        // I) Verify the request
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            ValidationProblemDetails problemDetails = new(validationResult.ToDictionary())
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1" //RFC link 400 Bad Request
            };

            return BadRequest(problemDetails); // 400 Bad Request
        }

        // II) Call the BLL for the business operations
        Book bookToCreate = new()
        {
            Title = request.Title,
            Description = request.Description,
            ISBN = request.ISBN
        };

        Book bookCreated = await _bookStoreService.CreateBookAsync(bookToCreate);

        // III) Return the DTO response 
        CreateBookDTOResponse response = new()
        {
            Id = bookCreated.Id,
            Title = bookCreated.Title,
            Description = bookCreated.Description,
            ISBN = bookCreated.ISBN
        };
        return CreatedAtAction(nameof(GetBook), new { idBook = bookCreated.Id }, response); // 201 Created
    }

    [Route("books/{idBook}")]
    [HttpPut]
    public async Task<IActionResult> ModifyBook([FromRoute] int idBook, [FromBody] ModifyBookDTORequest request, IValidator<ModifyBookDTORequest> validator)
    {
        // I) Verify the request
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            ValidationProblemDetails problemDetails = new(validationResult.ToDictionary())
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1" //RFC link 400 Bad Request
            };
            return BadRequest(problemDetails); // 400 Bad Request
        }
        // II) Call the BLL for the business operations
        Book bookToModify = new()
        {
            Id = idBook,
            Title = request.Title,
            Description = request.Description,
            ISBN = request.ISBN
        };
       
        Book bookModified = await _bookStoreService.ModifyBookAsync(bookToModify);
        // III) Return the DTO response 
        ModifyBookDTOResponse response = new()
        {
            Id = bookModified.Id,
            Title = bookModified.Title,
            Description = bookModified.Description,
            ISBN = bookModified.ISBN
        };
        return Ok(response); // 200 OK
  
        /////////////////////////////////////////////
    }


    [Route("books/{idBook}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteBook(int idBook)
    {
        // I) Verify the request
        //  Nothing here !
        // II) Call the BLL for the business operations
        await _bookStoreService.DeleteBookAsync(idBook);
        return NoContent(); // 204 No Content

    }

}
