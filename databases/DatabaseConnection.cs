using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuppSystem
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;

        private readonly SqlConnection _connection;

        private DatabaseConnection()
        {
            _connection = new SqlConnection(@"Data Source=LAPTOP-6PF13PHQ\SQLEXPRESS;Initial Catalog=rupp;Integrated Security=True");
        }
     
        public static DatabaseConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection();
                }
                return _instance;
            }
        }

        public SqlConnection GetConnection()
        {
            
            return _connection;
        }

       
    }
}
