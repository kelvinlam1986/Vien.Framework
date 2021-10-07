using System.Configuration;

namespace MinhLam.Framework.Data
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
                    connectionString = ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;
                }

                return connectionString;
            }
        }
    }
}
