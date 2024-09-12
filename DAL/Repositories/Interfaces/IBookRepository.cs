using DAL.Repositories.Interfaces.Generics;
using Domain.Entities;

namespace DAL.Repositories.Interfaces;

/// <summary>
/// Allow to interact with books in the database.
/// </summary>
public interface IBookRepository : IGenericReadRepository<int, Book>, IGenericWriteRepository<int, Book>
{
    // Here you can add specific methods for manipuling books in the database.
}
