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
        public FormMenu()
        {
            InitializeComponent();
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
    }
}
