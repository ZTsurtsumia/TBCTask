using System.Data;

namespace PersonDirectory.Application.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
