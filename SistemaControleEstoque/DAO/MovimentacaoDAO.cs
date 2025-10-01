using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace SistemaControleEstoque.DAO
{
    public class MovimentacaoDAO
    {
        public void RegistrarSaida(int idProduto, int quantidade, decimal preco)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var tx = conn.BeginTransaction(); // Usamos transação para garantir consistência

                try
                {
                    // 1. Atualiza o estoque do produto (diminui a quantidade)
                    string sqlEstoque = "UPDATE produto SET quantidade = quantidade - @qtd WHERE idproduto=@id";
                    var cmd1 = new MySqlCommand(sqlEstoque, conn, tx);
                    cmd1.Parameters.AddWithValue("@qtd", quantidade);
                    cmd1.Parameters.AddWithValue("@id", idProduto);
                    cmd1.ExecuteNonQuery();

                    // 2. Insere o registro na tabela de movimentação
                    string sqlMov = @"INSERT INTO movimentacao (tipo, quantidade, preco, data_movimentacao, fk_produto_idproduto) 
                                    VALUES ('Saida', @qtd, @preco, NOW(), @id)";
                    var cmd2 = new MySqlCommand(sqlMov, conn, tx);
                    cmd2.Parameters.AddWithValue("@qtd", quantidade);
                    cmd2.Parameters.AddWithValue("@preco", preco);
                    cmd2.Parameters.AddWithValue("@id", idProduto);
                    cmd2.ExecuteNonQuery();

                    tx.Commit(); // Confirma as duas operações
                }
                catch
                {
                    tx.Rollback(); // Desfaz tudo se der erro em uma das operações
                    throw; // Propaga o erro
                }
            }
        }
    }
}