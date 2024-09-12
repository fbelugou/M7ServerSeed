using BLL.Services.Interfaces;
using DAL;
using Domain.Entities;

namespace BLL.Services.Implementations;
internal class BookStoreService : IBookStoreService
{
    private readonly IUOW _db;

    public BookStoreService(IUOW db)
    {
        _db = db;    
    }
    //NOTE : THESE METHODS ARE IMPLEMENTED FOR EXEMPLES
    //
    // Normally, these methods should be implemented with 3 steps:
    // 1. Pre-Processing (e.g. Validation, Authorization, Verify data, etc.) Is possible to execute this action ?
    // 2. Execute the business operations (e.g. Call other services, Modify the database with UnitOfWork, etc.) Make the action !
    // 3. Maybe needed Post-Processing (e.g. Logging, Notifications, etc.) What to do after the action ?

    public Task<Book> CreateBookAsync(Book book)
    {
        return _db.Books.CreateAsync(book);
    }

    public Task DeleteBookAsync(int idBook)
    {
       return _db.Books.DeleteAsync(idBook);
    }

    public Task<Book> ModifyBookAsync(Book book)
    {
        return _db.Books.ModifyAsync(book);
    }

    public Task<Book> RetreiveBookAsync(int idBook)
    {
        return _db.Books.GetAsync(idBook);
    }

    public Task<IEnumerable<Book>> RetreiveBooksAsync()
    {
        return _db.Books.GetAllAsync();
    }
}
