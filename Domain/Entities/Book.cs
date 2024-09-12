namespace Domain.Entities;

/// <summary>
/// Represents a book.
/// </summary>
public class Book : Entity
{
    /// <summary>
    /// Title of the book.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Description of the book.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// ISBN of the book.
    /// </summary>
    public string ISBN { get; set; }

}
