using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace SistemaControleEstoque.DAO
{
    public static class Database
    {
        // ATENÇÃO: Ajuste a senha (Pwd) se a sua for diferente.
        public static string ConnectionString = "Server=localhost;Database=bd_sistema_estoque;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
