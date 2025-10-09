using SistemaControleEstoque.DAO;
using SistemaControleEstoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SistemaControleEstoque
{
    public partial class FormCategorias : Form
    {
        private int editingId = 0;

        public FormCategorias()
        {
            InitializeComponent();
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            var dao = new CategoriaDAO();
            var lista = dao.ObterTodas();
            dgvCategorias.DataSource = lista;
            if (dgvCategorias.Columns.Contains("Id"))
                dgvCategorias.Columns["Id"].Visible = false;
            dgvCategorias.ClearSelection();
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            editingId = 0;
            btnSalvar.Text = "Inserir";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome da categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dao = new CategoriaDAO();
                if (editingId == 0)
                {
                    dao.Inserir(nome);
                    MessageBox.Show("Categoria inserida.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dao.Atualizar(new Categoria { Id = editingId, Nome = nome });
                    MessageBox.Show("Categoria atualizada.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CarregarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            // Primeiro, verificamos se realmente existe uma linha selecionada
            if (dgvCategorias.SelectedRows.Count > 0)
            {
                // Tentamos obter o objeto 'Categoria' associado à linha
                var c = dgvCategorias.SelectedRows[0].DataBoundItem as Categoria;

                // Verificamos se a conversão funcionou
                if (c != null)
                {
                    // Se funcionou, atualizamos as variáveis e campos
                    editingId = c.Id;
                    txtNome.Text = c.Nome;
                    btnSalvar.Text = "Atualizar";

                    // --- NOSSA MENSAGEM DE TESTE ---
                    // Esta caixa vai aparecer para confirmar que o código chegou até aqui
                    //MessageBox.Show($"ID capturado: {c.Id}\nNome: {c.Nome}\n\nA variável 'editingId' agora vale: {editingId}",
                                    //"Debug - Seleção Funcionou!");
                }
                else
                {
                    // Se 'c' for nulo, a conversão falhou por algum motivo
                    MessageBox.Show("A linha foi selecionada, mas não foi possível converter o item para 'Categoria'.",
                                    "Debug - Erro de Conversão!");
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (editingId == 0)
            {
                MessageBox.Show("Selecione uma categoria para excluir.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Deseja realmente excluir?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var dao = new CategoriaDAO();
                dao.Excluir(editingId); // <-- método void

                CarregarCategorias();

                MessageBox.Show("Categoria excluída com sucesso.", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            dgvCategorias.ClearSelection();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {

        }
    }
}
