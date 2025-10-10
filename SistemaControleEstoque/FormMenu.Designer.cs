
namespace SistemaControleEstoque
{
    partial class FormMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSairMenu = new System.Windows.Forms.Button();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.btnSaida = new System.Windows.Forms.Button();
            this.btnListagem = new System.Windows.Forms.Button();
            this.btnCadastro = new System.Windows.Forms.Button();
            this.btnCategoria = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSairMenu
            // 
            this.btnSairMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSairMenu.Location = new System.Drawing.Point(200, 274);
            this.btnSairMenu.Name = "btnSairMenu";
            this.btnSairMenu.Size = new System.Drawing.Size(122, 100);
            this.btnSairMenu.TabIndex = 9;
            this.btnSairMenu.Text = "Sair do Sistema";
            this.btnSairMenu.UseVisualStyleBackColor = true;
            this.btnSairMenu.Click += new System.EventHandler(this.btnSairMenu_Click);
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorio.Location = new System.Drawing.Point(349, 26);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(122, 100);
            this.btnRelatorio.TabIndex = 8;
            this.btnRelatorio.Text = "Visualizar Relatórios";
            this.btnRelatorio.UseVisualStyleBackColor = true;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // btnSaida
            // 
            this.btnSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaida.Location = new System.Drawing.Point(33, 153);
            this.btnSaida.Name = "btnSaida";
            this.btnSaida.Size = new System.Drawing.Size(122, 100);
            this.btnSaida.TabIndex = 7;
            this.btnSaida.Text = "Registrar Saída de Estoque";
            this.btnSaida.UseVisualStyleBackColor = true;
            this.btnSaida.Click += new System.EventHandler(this.btnSaida_Click);
            // 
            // btnListagem
            // 
            this.btnListagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListagem.Location = new System.Drawing.Point(200, 26);
            this.btnListagem.Name = "btnListagem";
            this.btnListagem.Size = new System.Drawing.Size(122, 100);
            this.btnListagem.TabIndex = 6;
            this.btnListagem.Text = "Listar / Editar Produtos";
            this.btnListagem.UseVisualStyleBackColor = true;
            this.btnListagem.Click += new System.EventHandler(this.btnListagem_Click);
            // 
            // btnCadastro
            // 
            this.btnCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastro.Location = new System.Drawing.Point(33, 26);
            this.btnCadastro.Name = "btnCadastro";
            this.btnCadastro.Size = new System.Drawing.Size(122, 100);
            this.btnCadastro.TabIndex = 5;
            this.btnCadastro.Text = "Cadastrar Novo Produto";
            this.btnCadastro.UseVisualStyleBackColor = true;
            this.btnCadastro.Click += new System.EventHandler(this.btnCadastro_Click);
            // 
            // btnCategoria
            // 
            this.btnCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategoria.Location = new System.Drawing.Point(349, 153);
            this.btnCategoria.Name = "btnCategoria";
            this.btnCategoria.Size = new System.Drawing.Size(122, 100);
            this.btnCategoria.TabIndex = 10;
            this.btnCategoria.Text = "Atualizar Categorias";
            this.btnCategoria.UseVisualStyleBackColor = true;
            this.btnCategoria.Click += new System.EventHandler(this.btnCategoria_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.Location = new System.Drawing.Point(200, 153);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(122, 100);
            this.btnUsuarios.TabIndex = 11;
            this.btnUsuarios.Text = "Gerenciar Usuários";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(346, 288);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 13);
            this.lblUsuario.TabIndex = 12;
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 403);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.btnUsuarios);
            this.Controls.Add(this.btnCategoria);
            this.Controls.Add(this.btnSairMenu);
            this.Controls.Add(this.btnRelatorio);
            this.Controls.Add(this.btnSaida);
            this.Controls.Add(this.btnListagem);
            this.Controls.Add(this.btnCadastro);
            this.Name = "FormMenu";
            this.Text = "FormMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSairMenu;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Button btnSaida;
        private System.Windows.Forms.Button btnListagem;
        private System.Windows.Forms.Button btnCadastro;
        private System.Windows.Forms.Button btnCategoria;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Label lblUsuario;
    }
}