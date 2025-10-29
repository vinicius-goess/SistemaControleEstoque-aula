using System;
using System.Windows.Forms;

namespace SistemaControleEstoque
{
    public partial class FormMenu : Form
    {
        private readonly string usuarioLogado;
        private readonly string nivelAcesso;

        public FormMenu(string usuario, string nivel)
        {
            InitializeComponent();
            usuarioLogado = usuario;
            nivelAcesso = nivel;

            lblUsuario.Text = "Usuário: " + usuarioLogado;
            lblUsNivel.Text = "Nível de acesso: " + nivelAcesso;
            ConfigurarPermissoes();
        }

        private void ConfigurarPermissoes()
        {
            // Controle de permissões baseado em nível
            switch (nivelAcesso)
            {
                case "Usuario":
                    btnCadastro.Enabled = false;
                    btnSaida.Enabled = false;
                    btnUsuarios.Visible = false;
                    btnCategoria.Enabled = false;
                    break;

                case "Gerente":
                    btnUsuarios.Visible = false;
                    break;

                case "Administrador":
                default:
                    // Acesso total
                    break;
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            FormCadastro form = new FormCadastro();
            form.ShowDialog();
        }

        private void btnListagem_Click(object sender, EventArgs e)
        {
            if (nivelAcesso == "Administrador")
            {
                FormListagem frm = new FormListagem(nivelAcesso);
                frm.ShowDialog();
            }
            else
            {
                FormListagemUsuario frmUser = new FormListagemUsuario();
                frmUser.ShowDialog();
            }
        }

        private void btnSaida_Click(object sender, EventArgs e)
        {
            FormSaida frm = new FormSaida();
            frm.ShowDialog();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            FormRelatorio frm = new FormRelatorio(nivelAcesso);
            frm.ShowDialog();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FormCadastroUsuario frm = new FormCadastroUsuario(nivelAcesso);
            frm.ShowDialog();
        }

        private void btnSairMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FormCategorias frm = new FormCategorias();
            frm.ShowDialog();
        }
        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            new FormListagemFornecedor(nivelAcesso).ShowDialog();
        }
    }
}