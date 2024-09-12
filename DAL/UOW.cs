using DAL.Repositories.Implementations.MariaDB;
using DAL.Repositories.Implementations.SqlServer;
using DAL.Repositories.Interfaces;
using DAL.Sessions.Interfaces;

namespace DAL;
internal class UOW : IUOW
{
    private readonly IDBSession _dbSession;
    private readonly DBType _dBType;
    private readonly Dictionary<Type, Type> _currentRepositories;


    //////////////////////////////////////////////////////////////////
    // Here you can add your repositories for each database type
    ////////////////////////////////////////////////////////////////
    private readonly Dictionary<Type, Type> RepositoriesMysql = new Dictionary<Type, Type>()
    {
        //MySQL | MariaDB   
        { typeof(IBookRepository), typeof(BookRepositoryMariaDB) },
    };

    private readonly Dictionary<Type, Type> RepositoriesSQLServer = new Dictionary<Type, Type>()
    {
        //SQL Server
        { typeof(IBookRepository), typeof(BookRepositorySQLServer) },
    };
    //////////////////////////////////////////////////////////////////////



    public UOW(IDBSession dBSession, DBType dBType)
    {
        _dbSession = dBSession;
        _dBType = dBType;
        _currentRepositories = _dBType switch
        {
            DBType.MariaDB => RepositoriesMysql,
            DBType.SQLServer => RepositoriesSQLServer,
            _ => throw new NotImplementedException()
        };
    }

#pragma warning disable CS8603 // Existence possible d'un retour de référence null Impossible or PREFER CRASH APPLICATION. 

    //Permet de créer une instance par reflexion avec la classe ACTIVATOR et de retourner une instance de l'interface
    public IBookRepository Books => Activator.CreateInstance(_currentRepositories[typeof(IBookRepository)], _dbSession) as IBookRepository;

    //... Add your repositories here


#pragma warning restore CS8603 // Existence possible d'un retour de référence null Impossible.

    public void BeginTransaction()
    {
        if (_dbSession.Transaction is null)
        {
            _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
        }
    }

    public void Commit()
    {
        _dbSession.Transaction?.Commit();
        _dbSession.Transaction = null;
    }

    public void RollBack()
    {
        _dbSession.Transaction?.Rollback();
        _dbSession.Transaction = null;
    }
}

