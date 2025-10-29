using MySqlConnector;
using SistemaControleEstoque.Model;
using SistemaControleEstoque.Util;
using System;
using System.Collections.Generic;

namespace SistemaControleEstoque.DAO
{
    public class FornecedorDAO
    {
        public void Inserir(Fornecedor f)
        {
            try
            {
                // Bloco using tradicional para .NET Framework 4.7
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO fornecedor (razao_social, nome_fantasia, cnpj, email) 
                                   VALUES (@razao, @fantasia, @cnpj, @email)";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@razao", f.RazaoSocial);
                        cmd.Parameters.AddWithValue("@fantasia", f.NomeFantasia);
                        cmd.Parameters.AddWithValue("@cnpj", f.CNPJ);
                        cmd.Parameters.AddWithValue("@email", f.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Erro ao inserir fornecedor", f.NomeFantasia);
                throw;
            }
        }

        public List<Fornecedor> ObterTodos()
        {
            var lista = new List<Fornecedor>();
            try
            {
                // Blocos using aninhados
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM fornecedor";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Fornecedor
                                {
                                    IdFornecedor = reader.GetInt32("idfornecedor"),
                                    RazaoSocial = reader.GetString("razao_social"),
                                    NomeFantasia = reader.GetString("nome_fantasia"),
                                    CNPJ = reader.GetString("cnpj"),
                                    Email = reader.GetString("email")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Erro ao obter lista de fornecedores");
                throw;
            }
            return lista;
        }

        public Fornecedor ObterPorId(int id)
        {
            try
            {
                // Blocos using aninhados
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM fornecedor WHERE idfornecedor = @id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Fornecedor
                                {
                                    IdFornecedor = reader.GetInt32("idfornecedor"),
                                    RazaoSocial = reader.GetString("razao_social"),
                                    NomeFantasia = reader.GetString("nome_fantasia"),
                                    CNPJ = reader.GetString("cnpj"),
                                    Email = reader.GetString("email")
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Erro ao obter fornecedor por id", id.ToString());
                throw;
            }
        }

        public void Atualizar(Fornecedor f)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE fornecedor SET razao_social=@razao, nome_fantasia=@fantasia, cnpj=@cnpj, email=@email 
                                   WHERE idfornecedor=@id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@razao", f.RazaoSocial);
                        cmd.Parameters.AddWithValue("@fantasia", f.NomeFantasia);
                        cmd.Parameters.AddWithValue("@cnpj", f.CNPJ);
                        cmd.Parameters.AddWithValue("@email", f.Email);
                        cmd.Parameters.AddWithValue("@id", f.IdFornecedor);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Erro ao atualizar fornecedor", f.NomeFantasia);
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM fornecedor WHERE idfornecedor=@id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Erro ao excluir fornecedor", id.ToString());
                throw;
            }
        }
    }
}