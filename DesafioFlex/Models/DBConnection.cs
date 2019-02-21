using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace DesafioFlex.Models
{
    public class DBConnection
    {
        private DBConnection()
        {
        }
        
        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                // ToDo: Modificar para os dados aplicaveis
                string server = "localhost";
                string user = "root";
                string pass = "senha";
                string connstring = string.Format("Server={0}; database=flex; UID={1}; password={2}", server, user, pass);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }
            else if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}