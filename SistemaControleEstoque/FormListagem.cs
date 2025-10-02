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
    public partial class FormListagem : Form
    {
        public FormListagem()
        {
            InitializeComponent();
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            ProdutoDAO dao = new ProdutoDAO();
            dgvProdutos.DataSource = dao.ObterTodos();
            dgvProdutos.Columns["ID"].Visible = false;
        }

        private Produto GetSelecionado()
        {
            if (dgvProdutos.SelectedRows.Count > 0)
            {
                return dgvProdutos.SelectedRows[0].DataBoundItem as Produto;
            }
            return null;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var p = GetSelecionado();
            if (p == null) { MessageBox.Show("Selecione um produto."); return; }

            using (var f = new FormCadastro(p))
            {
                f.ShowDialog();
            }
            AtualizarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var p = GetSelecionado();
            if (p == null) { MessageBox.Show("Selecione um produto."); return; }

            if (MessageBox.Show($"Tem certeza que deseja excluir {p.Nome}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProdutoDAO dao = new ProdutoDAO();
                dao.Excluir(p.Id);
                AtualizarGrid();
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
        }
    }
}