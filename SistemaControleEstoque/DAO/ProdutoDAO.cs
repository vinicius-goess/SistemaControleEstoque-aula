using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using SistemaControleEstoque.Model;

namespace SistemaControleEstoque.DAO
{
    public class ProdutoDAO
    {
        public void Inserir(Produto p, int idCategoria)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO produto (nome_produto, descricao, quantidade, preco_custo, preco_venda, 
                                        estoque_minimo, fk_categoria_idcategoria, foto, localizacao_estoque, data_vencimento)
                       VALUES (@nome, @descricao, @qtd, @custo, @venda, @min, @cat, @foto, @local, @venc)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", p.Nome);
                cmd.Parameters.AddWithValue("@descricao", p.Descricao);
                cmd.Parameters.AddWithValue("@qtd", p.Quantidade);
                cmd.Parameters.AddWithValue("@custo", p.PrecoCusto);
                cmd.Parameters.AddWithValue("@venda", p.Preco);
                cmd.Parameters.AddWithValue("@min", p.EstoqueMinimo);
                cmd.Parameters.AddWithValue("@cat", idCategoria);
                cmd.Parameters.AddWithValue("@foto", p.Foto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@local", p.LocalizacaoEstoque ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@venc", p.DataVencimento ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Produto> ObterTodos()
        {
            var lista = new List<Produto>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT p.idproduto, p.nome_produto, p.descricao, p.quantidade, p.preco_custo, p.preco_venda,
                         p.estoque_minimo, c.nome as categoria_nome,
                         p.foto, p.localizacao_estoque, p.data_cadastro, p.data_vencimento
                       FROM produto p
                       JOIN categoria c ON p.fk_categoria_idcategoria = c.idcategoria";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Produto
                        {
                            Id = reader.GetInt32("idproduto"),
                            Nome = reader.GetString("nome_produto"),
                            Descricao = reader.GetString("descricao"),
                            Quantidade = reader.GetInt32("quantidade"),
                            PrecoCusto = reader.GetDecimal("preco_custo"),
                            Preco = reader.GetDecimal("preco_venda"),
                            EstoqueMinimo = reader.GetInt32("estoque_minimo"),
                            Categoria = reader.GetString("categoria_nome"),
                            Foto = reader.IsDBNull(reader.GetOrdinal("foto")) ? null : (byte[])reader["foto"],
                            LocalizacaoEstoque = reader.IsDBNull(reader.GetOrdinal("localizacao_estoque")) ? null : reader.GetString("localizacao_estoque"),
                            DataCadastro = reader.GetDateTime("data_cadastro"),
                            DataVencimento = reader.IsDBNull(reader.GetOrdinal("data_vencimento")) ? null : (DateTime?)reader.GetDateTime("data_vencimento")
                        });
                    }
                }
            }
            return lista;
        }

        public void Atualizar(Produto p, int idCategoria)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE produto SET nome_produto=@nome, descricao=@descricao, quantidade=@qtd,
                         preco_custo=@custo, preco_venda=@venda, estoque_minimo=@min, 
                         fk_categoria_idcategoria=@cat, foto=@foto, localizacao_estoque=@local, 
                         data_vencimento=@venc 
                       WHERE idproduto=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", p.Nome);
                cmd.Parameters.AddWithValue("@descricao", p.Descricao);
                cmd.Parameters.AddWithValue("@qtd", p.Quantidade);
                cmd.Parameters.AddWithValue("@custo", p.PrecoCusto);
                cmd.Parameters.AddWithValue("@venda", p.Preco);
                cmd.Parameters.AddWithValue("@min", p.EstoqueMinimo);
                cmd.Parameters.AddWithValue("@cat", idCategoria);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@foto", p.Foto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@local", p.LocalizacaoEstoque ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@venc", p.DataVencimento ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                // Primeiro, exclua movimentações relacionadas para evitar erro de chave estrangeira
                string sqlMov = "DELETE FROM movimentacao WHERE fk_produto_idproduto=@id";
                MySqlCommand cmdMov = new MySqlCommand(sqlMov, conn);
                cmdMov.Parameters.AddWithValue("@id", id);
                cmdMov.ExecuteNonQuery();

                // Agora, exclua o produto
                string sqlProd = "DELETE FROM produto WHERE idproduto=@id";
                MySqlCommand cmdProd = new MySqlCommand(sqlProd, conn);
                cmdProd.Parameters.AddWithValue("@id", id);
                cmdProd.ExecuteNonQuery();
            }
        }
    }
}