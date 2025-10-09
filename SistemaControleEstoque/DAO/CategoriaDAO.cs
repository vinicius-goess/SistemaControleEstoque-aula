using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using SistemaControleEstoque.Model;

namespace SistemaControleEstoque.DAO
{
    public class CategoriaDAO
    {
        // CORREÇÃO: Especificar o tipo de retorno como List<Categoria>
        public List<Categoria> ObterTodas()
        {
            // CORREÇÃO: Especificar o tipo da lista como List<Categoria>
            var lista = new List<Categoria>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT idcategoria, nome FROM categoria ORDER BY nome";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var c = new Categoria();
                            c.Id = Convert.ToInt32(reader["idcategoria"]);
                            c.Nome = reader["nome"].ToString();
                            lista.Add(c);
                        }
                    }
                }
            }
            return lista;
        }

        // Compatibilidade: retorna apenas nomes (se alguma parte do código ainda usar)
        public List<string> ObterCategorias()
        {
            // Esta linha agora funcionará corretamente após a correção acima.
            return ObterTodas().Select(c => c.Nome).ToList();
        }

        public void Inserir(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da categoria não pode ser vazio.", "nome");

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string sqlCheck = "SELECT COUNT(*) FROM categoria WHERE LOWER(nome) = LOWER(@nome)";
                using (var cmdCheck = new MySqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@nome", nome.Trim());
                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (exists > 0)
                        throw new InvalidOperationException("Categoria já existe.");
                }

                string sql = "INSERT INTO categoria (nome) VALUES (@nome)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Categoria c)
        {
            if (c == null)
                throw new ArgumentNullException("c");
            if (string.IsNullOrWhiteSpace(c.Nome))
                throw new ArgumentException("Nome da categoria não pode ser vazio.", "c.Nome");

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string sqlCheck = "SELECT COUNT(*) FROM categoria WHERE LOWER(nome) = LOWER(@nome) AND idcategoria <> @id";
                using (var cmdCheck = new MySqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@nome", c.Nome.Trim());
                    cmdCheck.Parameters.AddWithValue("@id", c.Id);
                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (exists > 0)
                        throw new InvalidOperationException("Outra categoria com o mesmo nome já existe.");
                }

                string sql = "UPDATE categoria SET nome = @nome WHERE idcategoria = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", c.Nome.Trim());
                    cmd.Parameters.AddWithValue("@id", c.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                // Verifica vinculação com produtos
                string sqlCheck = "SELECT COUNT(*) FROM produto WHERE fk_categoria_idcategoria = @id";
                using (var cmdCheck = new MySqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@id", id);
                    int vinculados = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (vinculados > 0)
                        throw new InvalidOperationException("Não é possível excluir: existem produtos vinculados a esta categoria.");
                }

                string sql = "DELETE FROM categoria WHERE idcategoria = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}