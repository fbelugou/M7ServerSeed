using Domain.Entities;

namespace BLL.Services.Interfaces;

/// <summary>
/// Service for the BookStore.
/// </summary>
public interface IBookStoreService
{
    #region Books operations
    /// <summary>
    /// Get all books in the store.
    /// </summary>
    /// <returns>
    ///   Enumerable of books.
    /// </returns>
    public Task<IEnumerable<Book>> RetreiveBooksAsync();

    /// <summary>
    /// Get a book by its unique identifier.
    /// </summary>
    /// <param name="idBook">
    ///  Unique identifier of the book.
    /// </param>
    /// <returns>
    ///     The book with the given identifier.
    /// </returns>
    /// <exception cref="Domain.Exceptions.NotFoundEntityException{Book}" >
    ///  Thrown when the book with the given identifier is not found.
    /// </exception>
    public Task<Book> RetreiveBookAsync(int idBook);

    /// <summary>
    /// Create a new book in the store.
    /// </summary>
    /// <param name="book">
    /// Book to create.
    /// </param>
    /// <returns>
    ///  Book created.
    /// </returns>
    public Task<Book> CreateBookAsync(Book book);

    /// <summary>
    /// Modify a book in the store.
    /// </summary>
    /// <param name="book">
    /// Book with the new values.
    /// </param>
    /// <returns>
    ///    Book modified.
    /// </returns>
    /// <exception cref="Domain.Exceptions.NotFoundEntityException{Book}">
    ///  Book with the given identifier is not found.
    /// </exception>
    public Task<Book> ModifyBookAsync(Book book);

    /// <summary>
    /// Archive a book in the store.
    /// </summary>
    /// <param name="idBook">
    ///     Unique identifier of the book to archive.
    /// </param>
    public Task DeleteBookAsync(int idBook);
    #endregion
}
