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

namespace SistemaControleEstoque
{
    public partial class FormSaida : Form
    {
        public FormSaida()
        {
            InitializeComponent();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            ProdutoDAO dao = new ProdutoDAO();
            cmbProduto.DataSource = dao.ObterTodos();
            cmbProduto.DisplayMember = "Nome"; // O que o usuário vê
            cmbProduto.ValueMember = "ID";   // O valor por trás
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbProduto.SelectedItem == null)
            {
                MessageBox.Show("Selecione um produto.");
                return;
            }

            var p = (Produto)cmbProduto.SelectedItem;
            int saida = (int)nudSaida.Value;

            if (saida <= 0) { MessageBox.Show("A quantidade de saída deve ser maior que zero."); return; }
            if (saida > p.Quantidade) { MessageBox.Show("Estoque insuficiente."); return; }

            try
            {
                MovimentacaoDAO dao = new MovimentacaoDAO();
                dao.RegistrarSaida(p.Id, saida, p.Preco);
                MessageBox.Show("Saída registrada com sucesso.");
                CarregarProdutos(); // Recarrega para atualizar a quantidade em estoque
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar saída: " + ex.Message);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}