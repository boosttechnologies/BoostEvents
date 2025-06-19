using System.Data;
using System.Data.SqlClient;

namespace BoostEvents.Web.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class SqlConnectionFactory(IConfiguration config) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        connection.Open();
        return connection;
    }
}