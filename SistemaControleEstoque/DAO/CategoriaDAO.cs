using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace SistemaControleEstoque.DAO
{
    public class CategoriaDAO
    {
        public List<string> ObterCategorias()
        {
            var lista = new List<string>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT nome FROM categoria ORDER BY nome";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(reader.GetString("nome"));
                    }
                }
            }
            return lista;
        }
    }
}