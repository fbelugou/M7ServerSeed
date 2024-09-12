using DAL.Repositories.Interfaces;
using Domain.Entities;

namespace DAL.Repositories.Implementations.SqlServer;
internal class BookRepositorySQLServer : IBookRepository
{
    public Task<Book> CreateAsync(Book entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> ModifyAsync(Book entity)
    {
        throw new NotImplementedException();
    }
}
