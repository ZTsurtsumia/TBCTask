using Microsoft.Data.SqlClient;
using PersonDirectory.Application.Data;
using System.Data;

namespace PersonDirectory.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}
