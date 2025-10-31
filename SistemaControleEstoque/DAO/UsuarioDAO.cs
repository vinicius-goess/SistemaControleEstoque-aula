using System;
using System.Collections.Generic;
using MySqlConnector;
using SistemaControleEstoque.Util; // É necessário incluir o namespace onde está a classe Seguranca e Logger (se for usar)

namespace SistemaControleEstoque.DAO
{
    public class UsuarioDAO
    {
        // Retorna nível de acesso como string
        public string ValidarLogin(string login, string senha)
        {
            // MUDANÇA: Usando o bloco 'using' tradicional do .NET Framework 4.7
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                // MUDANÇA: O SQL agora seleciona o hash da senha (coluna 'senha') e o 'nivel'
                string sql = "SELECT senha, nivel FROM usuario WHERE login=@login LIMIT 1";

                // MUDANÇA: Usando o bloco 'using' tradicional
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);

                    // MUDANÇA: Não adicionamos mais a senha digitada como parâmetro no SQL,
                    // pois a comparação será feita no C# com o hash.

                    // MUDANÇA: Precisamos usar ExecuteReader() para ler o hash e o nível
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Lê o hash da senha armazenado (índice 0)
                            string storedHash = reader.IsDBNull(0) ? null : reader.GetString(0);

                            // Lê o nível de acesso (índice 1)
                            string nivel = reader.IsDBNull(1) ? null : reader.GetString(1);

                            // MUDANÇA: Chama Seguranca.VerificarSenha para comparar a senha digitada
                            // com o hash armazenado.
                            if (!string.IsNullOrEmpty(storedHash) && Seguranca.VerificarSenha(senha, storedHash))
                            {
                                return nivel; // Autenticado, retorna o nível
                            }
                        }
                    }
                }
            }
            // Se a conexão falhou, o usuário não foi encontrado ou a senha não corresponde ao hash
            return null;
        }

        public void CadastrarUsuario(string nome, string login, string senha, string nivel)
        {
            // O ideal é que a senha seja hasheada antes de ser passada aqui,
            // ou que você chame Seguranca.CriarHash(senha) antes de inserir no banco.
            // Para manter o código igual, assumo que você cuidará do hash antes de chamar este DAO,
            // ou que a camada superior fará isso.
            // Se a classe Seguranca estiver sendo usada, seria bom garantir o hash aqui:
            // string senhaHash = Seguranca.CriarHash(senha);

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

                // Se você não está hasheando na camada de serviço, faça o hash aqui antes de inserir.
                // Exemplo, assumindo que Seguranca.CriarHash exista:
                // string senhaHash = Seguranca.CriarHash(senha);
                // No cmd.Parameters.AddWithValue("@senha", senhaHash);

                string sql = "INSERT INTO usuario (nome, login, senha, nivel) VALUES (@nome, @login, @senha, @nivel)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha); // *VERIFICAR SE ESTÁ HASHEADA ANTES*
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