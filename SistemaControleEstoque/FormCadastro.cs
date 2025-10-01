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
    public partial class FormCadastro : Form
    {
        private Produto produtoEditando;

        public FormCadastro()
        {
            InitializeComponent();
            PreencherCategorias();
        }

        // Construtor para EDIÇÃO
        public FormCadastro(Produto p) : this()
        {
            produtoEditando = p;
            CarregarProdutoParaEdicao();
        }

        private void PreencherCategorias()
        {
            CategoriaDAO dao = new CategoriaDAO();
            cmbCategoria.DataSource = dao.ObterCategorias();
        }

        private void CarregarProdutoParaEdicao()
        {
            txtNome.Text = produtoEditando.Nome;
            nudQuantidade.Value = produtoEditando.Quantidade;
            txtPreco.Text = produtoEditando.Preco.ToString("F2");
            cmbCategoria.SelectedItem = produtoEditando.Categoria;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.");
                return;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            var p = new Produto
            {
                Nome = txtNome.Text.Trim(),
                Quantidade = (int)nudQuantidade.Value,
                Preco = preco,
                Categoria = cmbCategoria.SelectedItem.ToString()
            };

            ProdutoDAO dao = new ProdutoDAO();
            // O ID da categoria é o índice + 1 (pois o ID no banco começa em 1)
            int idCategoria = cmbCategoria.SelectedIndex + 1;

            try
            {
                if (produtoEditando == null) // Inserindo novo
                {
                    dao.Inserir(p, idCategoria);
                    MessageBox.Show("Produto salvo com sucesso!");
                    LimparCampos();
                }
                else // Atualizando existente
                {
                    p.Id = produtoEditando.Id;
                    dao.Atualizar(p, idCategoria);
                    MessageBox.Show("Produto atualizado!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            nudQuantidade.Value = 1;
            txtPreco.Clear();
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }
}
