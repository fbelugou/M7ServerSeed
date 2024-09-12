using DAL.Sessions.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.Sessions.Implementations;

/// <summary>
/// Represent a session with a MariaDB database
/// </summary>
internal class DBSessionMariaDB : IDBSession
{
    public IDbConnection Connection { get; private set; }

    public IDbTransaction? Transaction { get; set; }

    public DBSessionMariaDB(string connectionString)
    {
        
        Connection = new MySqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}
