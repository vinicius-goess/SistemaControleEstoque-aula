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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtSenha.Text;

            UsuarioDAO dao = new UsuarioDAO();
            if (dao.ValidarLogin(user, pass))
            {
                this.Hide();
                using (var menu = new FormMenu())
                {
                    menu.ShowDialog();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            { Application.Exit(); }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if(txtSenha.UseSystemPasswordChar)
            {
                txtSenha.UseSystemPasswordChar = false;
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
            }
        }
        }
    }

