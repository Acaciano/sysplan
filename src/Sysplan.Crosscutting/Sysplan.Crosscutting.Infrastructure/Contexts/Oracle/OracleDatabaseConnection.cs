using Microsoft.Extensions.Configuration;
using System.Data.OracleClient;

namespace Sysplan.Crosscutting.Infrastructure.Contexts
{
    public class OracleDatabaseConnection : IOracleDatabaseConnection
    {
        private readonly IConfiguration _configuration;

        public OracleDatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OracleConnection GetConnection()
        {
            var config = new OracleDatabaseConfig();
            _configuration.Bind("OracleHC", config);

            OracleConnection conn = new OracleConnection(config.ConnectionString);

            return conn;
        }
    }
}