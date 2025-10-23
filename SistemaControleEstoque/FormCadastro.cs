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
            txtDescricao.Text = produtoEditando.Descricao;
            nudQuantidade.Value = produtoEditando.Quantidade;
            txtPreco.Text = produtoEditando.Preco.ToString("F2");
            cmbCategoria.SelectedItem = produtoEditando.Categoria;
            txtLocalizacao.Text = produtoEditando.LocalizacaoEstoque;

            if (produtoEditando.DataVencimento.HasValue)
            {
                chkSemVencimento.Checked = false;
                dtpVencimento.Value = produtoEditando.DataVencimento.Value;
            }
            else
            {
                chkSemVencimento.Checked = true;
            }

            if (produtoEditando.Foto != null)
            {
                fotoBytes = produtoEditando.Foto;
                using (MemoryStream ms = new MemoryStream(fotoBytes))
                {
                    picFotoProduto.Image = Image.FromStream(ms);
                }
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text) || cmbCategoria.SelectedItem == null)
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
                Descricao = txtDescricao.Text.Trim(),
                Quantidade = (int)nudQuantidade.Value,
                Preco = Convert.ToDecimal(txtPreco.Text),
                Categoria = cmbCategoria.SelectedItem.ToString(),
                Foto = this.fotoBytes,
                LocalizacaoEstoque = txtLocalizacao.Text.Trim(),
                DataVencimento = chkSemVencimento.Checked ? (DateTime?)null : dtpVencimento.Value
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
            txtDescricao.Clear();
            nudQuantidade.Value = 1;
            txtPreco.Clear();
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSemVencimento_CheckedChanged(object sender, EventArgs e)
        {
            dtpVencimento.Enabled = !chkSemVencimento.Checked;
        }


        private byte[] fotoBytes;
        private void btnSelecionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fotoBytes = File.ReadAllBytes(ofd.FileName);
                    using (MemoryStream ms = new MemoryStream(fotoBytes))
                    {
                        picFotoProduto.Image = Image.FromStream(ms);
                    }
                }
            }

        }

        private void btnRemoverFoto_Click(object sender, EventArgs e)
        {
            picFotoProduto.Image = null;
            fotoBytes = null;

        }

    }
}
