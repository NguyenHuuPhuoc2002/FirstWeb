using Microsoft.Data.SqlClient;


namespace FirstWeb
{
    public class SqlConnectionSingleton
    {
        private static SqlConnection _instance;
        private static readonly object _lock = new object();

        private SqlConnectionSingleton() { }

        public static SqlConnection GetInstance(IConfiguration configuration)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlConnection(configuration.GetConnectionString("DB"));
                        _instance.Open();
                    }
                }
            }
            return _instance;
        }

        public static void CloseConnection()
        {
            if (_instance != null)
            {
                lock (_lock)
                {
                    if (_instance != null && _instance.State == System.Data.ConnectionState.Open)
                    {
                        _instance.Close();
                        _instance = null;
                    }
                }
            }
        }
    }
}
