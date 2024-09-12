using System.Data;

namespace DAL.Sessions.Interfaces;

/// <summary>
/// Represents a session with the relational database (SQL ONLY)
/// </summary>
internal interface IDBSession : IDisposable
{
    /// <summary>
    /// Represents the connection to the database
    /// </summary>
    IDbConnection Connection { get; }

    /// <summary>
    /// Represents the current transaction scoped to the database
    /// </summary>
    IDbTransaction? Transaction { get; set; }
}
