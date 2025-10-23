using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            lblUsuario.Text = $"Usuário: {usuarioLogado} ({nivelAcesso})";
            ConfigurarPermissoes();
        }

        private void ConfigurarPermissoes()
        {
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
            new FormCadastro().ShowDialog();
        }

        private void btnListagem_Click(object sender, EventArgs e)
        {
            new FormListagem().ShowDialog();
        }

        private void btnSaida_Click(object sender, EventArgs e)
        {
            new FormSaida().ShowDialog();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            new FormRelatorio().ShowDialog();
        }

        private void btnSairMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            new FormCategorias().ShowDialog();

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            new FormCadastroUsuario().ShowDialog();
        }
    }
}
