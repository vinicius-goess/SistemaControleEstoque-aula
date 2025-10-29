using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaControleEstoque.DAO;
using SistemaControleEstoque.Model;
using SistemaControleEstoque.Util;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SistemaControleEstoque
{
    public partial class FormCadastroFornecedor : Form
    {
        public FormCadastroFornecedor()
        {
            InitializeComponent();
         }

        private bool ValidarCNPJ(string cnpj)
        {
            // Validação simples de formato (apenas números)
            var onlyDigits = Regex.Replace(cnpj, "\\D", "");
            return onlyDigits.Length == 14;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (btnSalvar.Text == "Atualizar")
            {
                // preencher objeto Fornecedor com Id e chamar dao.Atualizar(f);
            }

            if (string.IsNullOrWhiteSpace(txtRazaoSocial.Text) ||
               string.IsNullOrWhiteSpace(txtNomeFantasia.Text) ||
               string.IsNullOrWhiteSpace(mtxtCNPJ.Text) ||
               string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCNPJ(mtxtCNPJ.Text))
            {
                MessageBox.Show("CNPJ inválido. Verifique o formato.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var f = new Fornecedor
                {
                    RazaoSocial = txtRazaoSocial.Text.Trim(),
                    NomeFantasia = txtNomeFantasia.Text.Trim(),
                    CNPJ = mtxtCNPJ.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                var dao = new FornecedorDAO();
                dao.Inserir(f);

                Logger.LogInfo($"Fornecedor {f.NomeFantasia} cadastrado com sucesso.");
                MessageBox.Show("Fornecedor cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Erro ao salvar fornecedor", txtNomeFantasia.Text);
                MessageBox.Show("Erro ao cadastrar fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
