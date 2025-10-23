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
         
        private readonly string nivelUsuarioAtual;

        public FormCadastroUsuario() : this(string.Empty) { }

        public FormCadastroUsuario(string nivelUsuario)
        {
            InitializeComponent();
            nivelUsuarioAtual = nivelUsuario ?? string.Empty;

            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                var niveis = dao.ObterNiveis();
                if (niveis != null && niveis.Count > 0)
                {
                    cmbNivel.Items.AddRange(niveis.ToArray());
                }
                else
                {
                    cmbNivel.Items.AddRange(new object[] { "Usuario", "Administrador" });
                }
            }
            catch (Exception ex)
            {
                cmbNivel.Items.AddRange(new object[] { "Usuario", "Administrador" });
                MessageBox.Show("Não foi possível carregar os níveis do banco de dados: " + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Controle de acesso ao nível
            if (string.Equals(nivelUsuarioAtual, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                cmbNivel.Enabled = true;
                var item = cmbNivel.Items.Cast<object>()
                    .FirstOrDefault(i => string.Equals(i.ToString(), "Usuario", StringComparison.OrdinalIgnoreCase));
                if (item != null)
                    cmbNivel.SelectedItem = item;
                else if (cmbNivel.Items.Count > 0)
                    cmbNivel.SelectedIndex = 0;
            }
            else
            {
                cmbNivel.Enabled = false;
                var item = cmbNivel.Items.Cast<object>()
                    .FirstOrDefault(i => string.Equals(i.ToString(), "Usuario", StringComparison.OrdinalIgnoreCase));
                if (item != null)
                    cmbNivel.SelectedItem = item;
                else if (cmbNivel.Items.Count > 0)
                    cmbNivel.SelectedIndex = 0;
            }
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
            string nivel = cmbNivel.SelectedItem != null ? cmbNivel.SelectedItem.ToString() : string.Empty;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            try
            {
                UsuarioDAO dao = new UsuarioDAO();
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

