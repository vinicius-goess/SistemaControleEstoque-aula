using SistemaControleEstoque.DAO;

using System;

using System.Drawing;

using System.Windows.Forms;

using SistemaControleEstoque.Util;

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
                var dao = new UsuarioDAO();
                // Chamamos o método corrigido, que não precisa mais do parâmetro 'nivel'.
                string nivelAcesso = dao.ValidarLogin(user, pass);

                // Verificamos se o nível de acesso retornado NÃO é nulo ou vazio.
                if (!string.IsNullOrEmpty(nivelAcesso))
                {
                    // Se o login for válido, abrimos o FormMenu passando o usuário e o nível
                    // de acesso que veio DIRETAMENTE do banco de dados.
                    this.Hide(); // Opcional: esconde a tela de login
                    new FormMenu(user, nivelAcesso).ShowDialog();
                    this.Close(); // Fecha a aplicação ao fechar o menu.
                }
                else
                { // Erro de autenticação feito pelo usuario

                    // Registra tentativa de login inválida como erro (não exceção)

                    Logger.LogError("Login inválido", $"Usuário: {user} tentou autenticar com credenciais inválidas.");


                    MessageBox.Show("Usuário ou senha incorretos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            catch (Exception ex)
            {
                // --- PONTO DE INTEGRAÇÃO ---
                // Log centralizado com contexto do usuário que tentou autenticar
                Logger.LogException(ex, "Erro ao autenticar usuário", user);
                MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {
            btnEntrar.BackColor = Color.FromArgb(255, 160, 217, 208);
        }

        private void btnEntrar_MouseLeave(object sender, EventArgs e)
        {
            btnEntrar.BackColor = SystemColors.Control;
        }
    }

}