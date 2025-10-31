using SistemaControleEstoque.DAO;

using System;

using System.Linq;

using System.Windows.Forms;

using SistemaControleEstoque.Util;


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
            pbForcaSenha.Value = 0;
            lblForca.Text = "Força:";
        }


        private void btnSalvar_Click(object sender, EventArgs e)

        {

            string nome = txtNome.Text.Trim();
            string login = txtLogin.Text.Trim();
            string senha = txtSenha.Text;
            string senhaConfirm = txtSenhaConfirm.Text; // 3. Obter a senha de confirmação
            string nivel = cmbNivel.SelectedItem.ToString();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            // 4. VERIFICAÇÃO DE IGUALDADE
            if (!string.Equals(senha, senhaConfirm, StringComparison.Ordinal))
            {
                MessageBox.Show("As senhas não coincidem.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. VALIDAÇÃO DE FORÇA
            // Usamos a classe Seguranca. Se ela retornar 'false', mostramos os erros.
            if (!Seguranca.ValidarForcaSenha(senha, out var erros))
            {
                // Concatena a lista de erros em uma única string com quebras de linha
                MessageBox.Show("Senha inválida:\n" + string.Join("\n", erros), "Senha fraca", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 6. GERAÇÃO DO HASH
            // Se a senha passou em todas as validações, geramos o hash seguro.
            // Note que 'salt' é retornado via 'out', mas não precisamos dele aqui,
            // pois ele já está embutido no 'senhaHash'.
            string senhaHash = Seguranca.GerarHashSenha(senha, out _);

            var dao = new UsuarioDAO();
            try
            {
                //Speech.Falar("Cadastrando usuário com sucesso!");

                // 7. SALVAR O HASH
                // Enviamos o 'senhaHash' para o banco, NUNCA a 'senha' original.
                dao.CadastrarUsuario(nome, login, senhaHash, nivel);
                MessageBox.Show("Usuário cadastrado com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro no cadastro de usuário", ex.Message);
                MessageBox.Show("Erro: " + ex.Message);
            }

        }


        private void btnVoltar_Click(object sender, EventArgs e)

        {

            this.Close();

        }


        private void btnMostrar_Click(object sender, EventArgs e)

        {
            if (txtSenha.UseSystemPasswordChar)
            {
                txtSenha.UseSystemPasswordChar = false;
                txtSenhaConfirm.UseSystemPasswordChar = false; // Adicionado
                btnMostrar.Text = "Ocultar";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                txtSenhaConfirm.UseSystemPasswordChar = true; // Adicionado
                btnMostrar.Text = "Mostrar";
            }

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            {
                UpdatePasswordStrengthIndicator(txtSenha.Text);
            }
        }
            // 10. MÉTODO DO INDICADOR DE FORÇA
            // Este método avalia a senha e atualiza a interface.
            private void UpdatePasswordStrengthIndicator(string senha)
            {
                // Score simples: soma de regras cumpridas (de 0 a 5)
                // Este é um cálculo RÁPIDO apenas para o indicador visual.
                // A validação COMPLETA (com Regex) só acontece no clique do botão Salvar.
                int score = 0;
                if (!string.IsNullOrEmpty(senha))
                {
                    if (senha.Length >= 12) score++;
                    // Usamos Regex aqui para uma verificação rápida
                    if (System.Text.RegularExpressions.Regex.IsMatch(senha, "[A-Z]")) score++;
                    if (System.Text.RegularExpressions.Regex.IsMatch(senha, "[a-z]")) score++;
                    if (System.Text.RegularExpressions.Regex.IsMatch(senha, "[0-9]")) score++;
                    if (System.Text.RegularExpressions.Regex.IsMatch(senha, "[^a-zA-Z0-9]")) score++;
                }

                // Atualiza a ProgressBar (Valor vai de 0 a 100. Como temos 5 regras, score  20)
                pbForcaSenha.Value = Math.Min(100, score * 20);

                // Atualiza o texto da Label
                switch (score)
                {
                    case 5:
                        lblForca.Text = "Força: Muito forte";
                        break;
                    case 4:
                        lblForca.Text = "Força: Forte";
                        break;
                    case 3:
                        lblForca.Text = "Força: Média";
                        break;
                    case 2:
                        lblForca.Text = "Força: Fraca";
                        break;
                    default: // 0 ou 1
                        lblForca.Text = "Força: Muito fraca";
                        break;
                }
            }
        }

    }
