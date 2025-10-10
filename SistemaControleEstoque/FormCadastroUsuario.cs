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
        public partial class FormCadastroUsuario : Form
        {
            public FormCadastroUsuario()
            {
                InitializeComponent();
                cmbNivel.Items.Add("Usuario");
                cmbNivel.Items.Add("Administrador");
                cmbNivel.Items.Add("Gerente");
                cmbNivel.SelectedIndex = 0;
            }

            private void btnMostrar_Click(object sender, EventArgs e)
        {
            if (txtSenha.UseSystemPasswordChar)
            {
                txtSenha.UseSystemPasswordChar = false;
                btnMostrar.Text = "Ocultar";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                btnMostrar.Text = "Mostrar";
            }
        }

        private void btnMostrar_MouseEnter(object sender, EventArgs e)
        {
            btnMostrar.BackColor = Color.Yellow;
        }

        private void btnMostrar_MouseLeave(object sender, EventArgs e)
        {
            btnMostrar.BackColor = SystemColors.Control;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string login = txtLogin.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string nivel = cmbNivel.SelectedItem.ToString();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            UsuarioDAO dao = new UsuarioDAO();
            try
            {
                dao.CadastrarUsuario(nome, login, senha, nivel);
                MessageBox.Show("Usuário cadastrado com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

