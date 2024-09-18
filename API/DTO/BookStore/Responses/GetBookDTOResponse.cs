using Domain.Entities;

namespace API.DTO.BookStore.Responses;

public class GetBookDTOResponse
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public string ISBN { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is GetBookDTOResponse response &&
               Id == response.Id &&
               Title == response.Title &&
               Description == response.Description &&
               ISBN == response.ISBN;
    }
}

