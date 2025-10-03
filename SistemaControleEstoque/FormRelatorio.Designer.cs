
namespace SistemaControleEstoque
{
    partial class FormRelatorio
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
            this.lblTituloRelatorio = new System.Windows.Forms.Label();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.dgvRelatorio = new System.Windows.Forms.DataGridView();
            this.btnAtualizarRel = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloRelatorio
            // 
            this.lblTituloRelatorio.AutoSize = true;
            this.lblTituloRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloRelatorio.Location = new System.Drawing.Point(12, 28);
            this.lblTituloRelatorio.Name = "lblTituloRelatorio";
            this.lblTituloRelatorio.Size = new System.Drawing.Size(375, 24);
            this.lblTituloRelatorio.TabIndex = 0;
            this.lblTituloRelatorio.Text = "Relatório: Produtos com Estoque Baixo";
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotal.Location = new System.Drawing.Point(12, 315);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(301, 24);
            this.lblValorTotal.TabIndex = 1;
            this.lblValorTotal.Text = "Valor total em estoque: R$ 0,00";
            // 
            // dgvRelatorio
            // 
            this.dgvRelatorio.AllowUserToAddRows = false;
            this.dgvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelatorio.Location = new System.Drawing.Point(16, 65);
            this.dgvRelatorio.Name = "dgvRelatorio";
            this.dgvRelatorio.ReadOnly = true;
            this.dgvRelatorio.Size = new System.Drawing.Size(428, 223);
            this.dgvRelatorio.TabIndex = 2;
            // 
            // btnAtualizarRel
            // 
            this.btnAtualizarRel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarRel.Location = new System.Drawing.Point(16, 363);
            this.btnAtualizarRel.Name = "btnAtualizarRel";
            this.btnAtualizarRel.Size = new System.Drawing.Size(137, 94);
            this.btnAtualizarRel.TabIndex = 5;
            this.btnAtualizarRel.Text = "Gerar / Atualizar Relatório";
            this.btnAtualizarRel.UseVisualStyleBackColor = true;
            this.btnAtualizarRel.Click += new System.EventHandler(this.btnAtualizarRel_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(309, 363);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(135, 98);
            this.btnVoltar.TabIndex = 11;
            this.btnVoltar.Text = "Voltar ao Menu";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // FormRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 494);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnAtualizarRel);
            this.Controls.Add(this.dgvRelatorio);
            this.Controls.Add(this.lblValorTotal);
            this.Controls.Add(this.lblTituloRelatorio);
            this.Name = "FormRelatorio";
            this.Text = "FormRelatorio";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTituloRelatorio;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.DataGridView dgvRelatorio;
        private System.Windows.Forms.Button btnAtualizarRel;
        private System.Windows.Forms.Button btnVoltar;
    }
}