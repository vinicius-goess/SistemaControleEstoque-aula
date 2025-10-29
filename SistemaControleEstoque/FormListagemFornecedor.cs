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
using SistemaControleEstoque.Model;
using SistemaControleEstoque.Util;

namespace SistemaControleEstoque
{
    public partial class FormListagemFornecedor : Form
    {
        private List<Fornecedor> lista;

        private readonly string nivelUsuario;

        public FormListagemFornecedor(string nivel)

        {

            InitializeComponent();

            nivelUsuario = nivel ?? string.Empty;


            // Ajustes visuais para o grid

            dgvFornecedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvFornecedores.ReadOnly = true;

            dgvFornecedores.AllowUserToAddRows = false;

            dgvFornecedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            // Se o usuário for 'Usuario', desativa ações de edição/atualização/exclusão

            if (string.Equals(nivelUsuario, "Usuario", StringComparison.OrdinalIgnoreCase))

            {

                btnNovo.Enabled = false;

                btnEditar.Enabled = false;

                btnAtualizar.Enabled = false;

                btnExcluir.Enabled = false;

            }

            AtualizarGrid();
        }

        public void AtualizarGrid()
        {
            try

            {

                var dao = new FornecedorDAO();

                lista = dao.ObterTodos();


                // Use vinculação de dados para que as colunas sejam geradas automaticamente

                dgvFornecedores.DataSource = null;

                dgvFornecedores.AutoGenerateColumns = true;

                dgvFornecedores.DataSource = lista;

                if (dgvFornecedores.Columns.Contains("IdFornecedor"))

                    dgvFornecedores.Columns["IdFornecedor"].Visible = false;

            }

            catch (Exception ex)

            {

                Logger.LogException(ex, "Erro ao atualizar grid de fornecedores");

                MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message);

            }

        }


        private Fornecedor GetSelecionado()

        {

            if (dgvFornecedores.SelectedRows.Count > 0)

            {

                int id = Convert.ToInt32(dgvFornecedores.SelectedRows[0].Cells[0].Value);

                var dao = new FornecedorDAO();

                return dao.ObterPorId(id);

            }

            return null;

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            new FormCadastroFornecedor().ShowDialog();

            AtualizarGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var f = GetSelecionado();

            if (f == null) { MessageBox.Show("Selecione um fornecedor."); return; }

            var form = new FormCadastroFornecedor(); // Reutilizar ou criar outro construtor para edição

            // Preencher campos do form de edição - exemplo simples:

            form.Tag = f; // passar via Tag ou criar método para carregar

            form.ShowDialog();

            AtualizarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var f = GetSelecionado();

            if (f == null) { MessageBox.Show("Selecione um fornecedor."); return; }


            var confirm = MessageBox.Show($"Deseja excluir o fornecedor {f.NomeFantasia}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)

            {

                try

                {

                    var dao = new FornecedorDAO();

                    dao.Excluir(f.IdFornecedor);

                    Logger.LogInfo($"Fornecedor {f.NomeFantasia} excluído.");

                    AtualizarGrid();

                }

                catch (Exception ex)

                {

                    Logger.LogException(ex, "Erro ao excluir fornecedor", f.NomeFantasia);

                    MessageBox.Show("Erro ao excluir fornecedor: " + ex.Message);

                }

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