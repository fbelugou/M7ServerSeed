using Domain.Entities;

namespace API.DTO.BookStore.Responses;

public class GetAllBooksDTOResponse
{
    public class GetAllBookItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
    }
    public IEnumerable<GetAllBookItemResponse> Books { get; set; }
}

