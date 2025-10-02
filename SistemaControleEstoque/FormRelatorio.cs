using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaControleEstoque.DAO;
using MySqlConnector;

namespace SistemaControleEstoque
{
    public partial class FormRelatorio : Form
    {
        public FormRelatorio()
        {
            InitializeComponent();
            GerarRelatorio();
        }

        private void GerarRelatorio()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Relatório 1: Produtos com estoque baixo
                    string sqlBaixo = "SELECT nome_produto, quantidade, estoque_minimo, preco_venda FROM produto WHERE quantidade < estoque_minimo";
                    MySqlDataAdapter da = new MySqlDataAdapter(sqlBaixo, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRelatorio.DataSource = dt;

                    // Relatório 2: Valor total em estoque
                    string sqlTotal = "SELECT SUM(quantidade * preco_venda) AS total FROM produto";
                    MySqlCommand cmd = new MySqlCommand(sqlTotal, conn);
                    object result = cmd.ExecuteScalar();
                    decimal total = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;

                    lblValorTotal.Text = $"Valor total em estoque: {total:C2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar relatório: " + ex.Message);
            }
        }

        private void btnAtualizarRel_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }
    }
}
