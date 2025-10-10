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
         //Theme.ApplyToForm(this);
         txtSenha.UseSystemPasswordChar = true;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
                  string user = txtUsuario.Text.Trim();
                 string pass = txtSenha.Text;
     
                 try
                 {
                     var dao = new UsuarioDAO();
                     string nivelAcesso = dao.ValidarLogin(user, pass);
     
                     if (!string.IsNullOrEmpty(nivelAcesso))
                     {
                         this.Hide();
                         // Criamos o FormMenu e exibimos como um diálogo.
                         // O código para aqui até o FormMenu ser fechado.
                         FormMenu formMenu = new FormMenu(user, nivelAcesso);
                        formMenu.ShowDialog();
     
                         // Após o FormMenu ser fechado, a aplicação inteira é encerrada.
                         this.Close();
                     }
                     else
                     {
                         MessageBox.Show("Usuário ou senha incorretos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                 }
                catch (Exception ex)
                {
                   MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                btnMostrar.Text = "Ocultar";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                btnMostrar.Text = "Mostrar";
            }
        }

        //private Random rnd = new Random();

        private void btnMostrar_MouseEnter(object sender, EventArgs e)
        {
            btnMostrar.BackColor = Color.Yellow;
            //int maxX = this.ClientSize.Width - btnMostrar.Width;
          //int maxY = this.ClientSize.Height - btnMostrar.Height;

            //int newX = rnd.Next(0, maxX);
           // int newY = rnd.Next(0, maxY);

          // btnMostrar.Location = new Point(newX, newY);

        }

        private void btnMostrar_MouseLeave(object sender, EventArgs e)
        {
            btnMostrar.BackColor = SystemColors.Control;

        }

        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {
            btnEntrar.BackColor = Color.LightGreen;
        }

        private void btnEntrar_MouseLeave(object sender, EventArgs e)
        {
            btnEntrar.BackColor = SystemColors.Control;
        }

        private void btnSair_MouseEnter(object sender, EventArgs e)
        {
            btnSair.BackColor = Color.Red;
        }

        private void btnSair_MouseLeave(object sender, EventArgs e)
        {
            btnSair.BackColor = SystemColors.Control;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FormCadastroUsuario form = new FormCadastroUsuario();
            form.ShowDialog();
        }
    }
    }

