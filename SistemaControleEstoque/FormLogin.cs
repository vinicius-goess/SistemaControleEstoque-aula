using SistemaControleEstoque.DAO;

using System;

using System.Drawing;

using System.Windows.Forms;

//using SistemaControleEstoque.Util;


namespace SistemaControleEstoque

{

    public partial class FormLogin : Form

    {

        public FormLogin()

        {

            InitializeComponent();

            txtSenha.UseSystemPasswordChar = true;

        }


        private void btnEntrar_Click(object sender, EventArgs e)

        {

            string user = txtUsuario.Text.Trim();

            string pass = txtSenha.Text;


            try

            {

                UsuarioDAO dao = new UsuarioDAO();

                string nivelAcesso = dao.ValidarLogin(user, pass);


                if (!string.IsNullOrEmpty(nivelAcesso))

                {

                    this.Hide();

                    FormMenu menu = new FormMenu(user, nivelAcesso);

                    menu.ShowDialog();

                    this.Close();

                }

                else

                {

                    MessageBox.Show("Usuário ou senha incorretos.", "Atenção",

                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }

            catch (Exception ex)

            {

                //Logger.LogException(ex, "Erro ao autenticar usuário", user);

                MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message,

                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


        private void btnSair_Click(object sender, EventArgs e)

        {

            Application.Exit();

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

            btnMostrar.BackColor = Color.FromArgb(255, 235, 59);

        }


        private void btnMostrar_MouseLeave(object sender, EventArgs e)

        {

            btnMostrar.BackColor = SystemColors.Control;

        }


        private void btnSair_MouseEnter(object sender, EventArgs e)

        {

            btnSair.BackColor = Color.FromArgb(244, 67, 54);

        }


        private void btnSair_MouseLeave(object sender, EventArgs e)

        {

            btnSair.BackColor = SystemColors.Control;

        }


        private void btnCadastrar_Click(object sender, EventArgs e)

        {

            FormCadastroUsuario cadastro = new FormCadastroUsuario();

            cadastro.ShowDialog();

        }

    }

}