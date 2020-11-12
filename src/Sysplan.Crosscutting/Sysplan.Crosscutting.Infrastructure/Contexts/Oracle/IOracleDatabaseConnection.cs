using System.Data.OracleClient;

namespace Sysplan.Crosscutting.Infrastructure.Contexts
{
    public interface IOracleDatabaseConnection
    {
        OracleConnection GetConnection();
    }
}