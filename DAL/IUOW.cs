using DAL.Repositories.Interfaces;

namespace DAL;
/// <summary>
/// Unit of work to interact with the database
/// </summary>
public interface IUOW
{
    #region Repositories
    IBookRepository Books { get; }
    #endregion

    /// <summary>
    /// Start a transaction with the database
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Cancel transaction and restaure previous state of the database
    /// </summary>
    void RollBack();

    /// <summary>
    /// Confirm the transaction with the database
    /// </summary>
    void Commit();
}
