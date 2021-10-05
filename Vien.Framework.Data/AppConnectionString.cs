using System.Configuration;

namespace Vien.Framework.Data
{
    public sealed class AppConnectionString
    {
        private static string connectionString = string.Empty;

        private AppConnectionString()
        {
        }

        public static string ConnectionString
        {
            get
            {
                if (connectionString == "")
                {
                    connectionString = ConfigurationManager.ConnectionStrings["VienConnectionString"].ConnectionString;
                }

                return connectionString;
            }
        }
    }
}
