using DAL.Repositories.Interfaces;
using DAL.Sessions.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Exceptions;

namespace DAL.Repositories.Implementations.MariaDB;
internal class BookRepositoryMariaDB : IBookRepository
{
    private readonly IDBSession _session;

    public BookRepositoryMariaDB(IDBSession dBSession)
    {
        _session = dBSession;
    }

    public async Task<Book> CreateAsync(Book entity)
    {
        string query = @"
            INSERT INTO Book(Title, Description, ISBN)
            VALUES(@Title, @Description, @ISBN); Select LAST_INSERT_ID()";

        int lastId = await _session.Connection.ExecuteScalarAsync<int>(query,
          new
          {
              entity.Title,
              entity.Description,
              entity.ISBN
          }, _session.Transaction
        );

        entity.Id = lastId;

        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        string query = "DELETE FROM Book WHERE Id = @Id";

        int numLine = await _session.Connection.ExecuteAsync(query, new { Id = id }, _session.Transaction);

        if (numLine == 0)
        {
            throw new NotFoundEntityException(nameof(Book) ,id);
        }
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        string query = "SELECT * FROM Book";

        return _session.Connection.QueryAsync<Book>(query, transaction: _session.Transaction);
    }

    public async Task<Book> GetAsync(int id)
    {
        string query = "SELECT * FROM Book WHERE Id = @Id";

        Book? book = await _session.Connection.QueryFirstOrDefaultAsync<Book>(query, new { Id = id }, _session.Transaction);

        if (book is null)
        {
            throw new NotFoundEntityException(nameof(Book), id);
        }

        return book;
    }

    public async Task<Book> ModifyAsync(Book entity)
    {
        string query = @"
            UPDATE Book
            SET Title = @Title, Description = @Description, ISBN = @ISBN
            WHERE Id = @Id";

        int numLine = await _session.Connection.ExecuteAsync(query, new
        {
            entity.Title,
            entity.Description,
            entity.ISBN,
        }, _session.Transaction);

        if (numLine == 0)
        {
            throw new NotFoundEntityException(nameof(Book), entity.Id);
        }

        return entity;
    }
}
