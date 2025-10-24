using System;
using System.Collections.Generic;
using MySqlConnector;
//using SistemaControleEstoque.Util;

namespace SistemaControleEstoque.DAO
{
    public class UsuarioDAO
    {
        // Retorna nível de acesso como string
        public string ValidarLogin(string login, string senha)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT nivel FROM usuario WHERE login=@login AND senha=@senha";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                    return null;
                }
            }
        }

        public void CadastrarUsuario(string nome, string login, string senha, string nivel)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sqlCheck = "SELECT COUNT(*) FROM usuario WHERE login=@login";
                using (MySqlCommand cmdCheck = new MySqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@login", login);
                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (exists > 0)
                    {
                        //Logger.LogError("Erro no cadastro de usuário", "Usuário tentou criar um login que já está em uso.");
                        throw new Exception("Este login já está em uso.");
                    }
                }

                string sql = "INSERT INTO usuario (nome, login, senha, nivel) VALUES (@nome, @login, @senha, @nivel)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@nivel", nivel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> ObterNiveis()
        {
            List<string> lista = new List<string>();
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string schema = conn.Database;
                string sqlEnum = "SELECT COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS " +
                                 "WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='usuario' AND COLUMN_NAME='nivel'";

                using (MySqlCommand cmdEnum = new MySqlCommand(sqlEnum, conn))
                {
                    cmdEnum.Parameters.AddWithValue("@schema", schema);
                    object enumResult = cmdEnum.ExecuteScalar();
                    if (enumResult != null && enumResult != DBNull.Value)
                    {
                        string text = enumResult.ToString();
                        int start = text.IndexOf('(');
                        int end = text.LastIndexOf(')');
                        if (start >= 0 && end > start)
                        {
                            string inner = text.Substring(start + 1, end - start - 1);
                            string[] parts = inner.Split(',');
                            foreach (string p in parts)
                            {
                                string cleaned = p.Trim().Trim('\'').Trim();
                                if (!string.IsNullOrEmpty(cleaned) && !lista.Contains(cleaned))
                                    lista.Add(cleaned);
                            }
                        }
                    }
                }

                string sql = "SELECT DISTINCT nivel FROM usuario";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string val = reader.IsDBNull(0) ? null : reader.GetString(0);
                        if (!string.IsNullOrEmpty(val) && !lista.Contains(val))
                            lista.Add(val);
                    }
                }
            }

            lista.Sort(StringComparer.OrdinalIgnoreCase);
            return lista;
        }
    }
}