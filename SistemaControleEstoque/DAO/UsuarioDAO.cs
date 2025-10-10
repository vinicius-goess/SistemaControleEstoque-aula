using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows.Forms;

namespace SistemaControleEstoque.DAO
{
    public class UsuarioDAO
    {
        public string ValidarLogin(string login, string senha)
        {
            // Alterado de 'using var' para o bloco using tradicional para compatibilidade com .NET 4.7
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT nivel FROM usuario WHERE login=@login AND senha=@senha";

                // Bloco using tradicional
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }

            return null;
        }

        public void CadastrarUsuario(string nome, string login, string senha, string nivel)
        {
            // Bloco using tradicional
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string sqlCheck = "SELECT COUNT(*) FROM usuario WHERE login=@login";
                // Bloco using tradicional
                using (var cmdCheck = new MySqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@login", login);
                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (exists > 0)
                        throw new Exception("Este login já está em uso.");
                }

                string sql = "INSERT INTO usuario (nome, login, senha, nivel) VALUES (@nome, @login, @senha, @nivel)";
                // Bloco using tradicional
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@nivel", nivel);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    }
