using DAL.Sessions.Implementations;
using DAL.Sessions.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

/// <summary>
/// Represents the type of the database
/// </summary>
public enum DBType
{
    MariaDB,
    SQLServer,
    PostgreSQL,
    Oracle
}

/// <summary>
/// Represents the options for the DAL
/// </summary>
public class DALOptions
{
    //Here you can add your custom options

    /// <summary>
    /// Type of the database
    /// </summary>
    public DBType DBType { get; set; } = DBType.MariaDB;

    /// <summary>
    /// Connection string to the database
    /// </summary>
    public string? DBConnectionString { get; set; }
}

public static class DALExtension
{
    public static IServiceCollection AddDAL(this IServiceCollection services, Action<DALOptions>? configure = null)
    {
        DALOptions options = new();
        configure?.Invoke(options); // Invoke the configuration method if not null


        
        // Miss configuration critical error ! (Forgot to specify the connection string)
        if (string.IsNullOrEmpty(options.DBConnectionString))
        {
            throw new SystemException("Please specify a connection string for the DAL configuration !");
        }



        //Register your services here
        services.AddScoped<IDBSession>((services) =>
        {
            switch (options.DBType)
            {
                case DBType.MariaDB:
                    return new DBSessionMariaDB(options.DBConnectionString);
                case DBType.SQLServer:
                //    return new DBSessionSqlServer(options.DBConnectionString);
                case DBType.PostgreSQL:
                //   return new DBSessionPostgreSQL(options.DBConnectionString);
                case DBType.Oracle:
                //  return new DBSessionOracle(options.DBConnectionString);
                default:
                    throw new NotImplementedException();


            }
        });

        //NOTE: GetRequiredService is used to get the service in the IOC that is required by the UOW (DBSession)
        services.AddTransient<IUOW, UOW>((services) => new UOW(services.GetRequiredService<IDBSession>(), options.DBType));

        return services;
    }
}