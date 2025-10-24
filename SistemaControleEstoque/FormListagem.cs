using SistemaControleEstoque.DAO;
using SistemaControleEstoque.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaControleEstoque
{
    public partial class FormListagem : Form
    {
        public FormListagem(string nivelAcesso)
        {
            InitializeComponent();
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var produtos = dao.ObterTodos();

            dgvProdutos.DataSource = null;
            dgvProdutos.Columns.Clear();
            dgvProdutos.Rows.Clear();
            dgvProdutos.AutoGenerateColumns = false;

            // Coluna de Imagem
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Foto";
            imgCol.HeaderText = "Foto";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imgCol.Width = 100;
            dgvProdutos.Columns.Add(imgCol);

            // Colunas de Texto
            dgvProdutos.Columns.Add("Id", "ID");
            dgvProdutos.Columns.Add("Nome", "Nome");
            dgvProdutos.Columns.Add("Quantidade", "Qtd");
            dgvProdutos.Columns.Add("Preco", "Preço Venda");
            dgvProdutos.Columns.Add("Localizacao", "Localização");
            dgvProdutos.Columns.Add("Vencimento", "Vencimento");

            dgvProdutos.Columns["Id"].Visible = false;

            // Popula o grid com os dados
            foreach (var p in produtos)
            {
                Image foto = null;
                if (p.Foto != null)
                {
                    using (MemoryStream ms = new MemoryStream(p.Foto))
                    {
                        foto = Image.FromStream(ms);
                    }
                }

                string vencimento = p.DataVencimento.HasValue ? p.DataVencimento.Value.ToShortDateString() : "N/A";

                dgvProdutos.Rows.Add(foto, p.Id, p.Nome, p.Quantidade, p.Preco.ToString("C2"), p.LocalizacaoEstoque, vencimento);
            }
        }


        private Produto GetSelecionado()
        {
            if (dgvProdutos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells["Id"].Value);

                ProdutoDAO dao = new ProdutoDAO();
                return dao.ObterTodos().Find(p => p.Id == id);
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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}