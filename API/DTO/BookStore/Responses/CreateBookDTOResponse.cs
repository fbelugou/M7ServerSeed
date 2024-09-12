namespace API.DTO.BookStore.Responses;

public class CreateBookDTOResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
}
