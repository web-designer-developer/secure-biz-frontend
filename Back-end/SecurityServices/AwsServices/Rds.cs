using MySqlConnector;
using Newtonsoft.Json.Linq;
using SecurityServices.Common;

namespace SecurityServices.AwsServices
{
    public static class Rds
    {
        public static string GetConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = EnvironmentHelper.RDS_HOST,
                Port = 3306,
                UserID = EnvironmentHelper.RDS_USERNAME,
                Password = EnvironmentHelper.RDS_PASSWORD,
                Database = EnvironmentHelper.RDS_DATABASE,
                ConnectionTimeout = 30,
                SslMode = MySqlSslMode.Required
            };

            return builder.ConnectionString;
        }

    }
}
