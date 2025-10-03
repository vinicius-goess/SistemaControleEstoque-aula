
namespace SistemaControleEstoque
{
    partial class FormSaida
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
            this.lblProduto = new System.Windows.Forms.Label();
            this.lblQtdSaida = new System.Windows.Forms.Label();
            this.cmbProduto = new System.Windows.Forms.ComboBox();
            this.nudSaida = new System.Windows.Forms.NumericUpDown();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaida)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduto.Location = new System.Drawing.Point(12, 63);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(207, 24);
            this.lblProduto.TabIndex = 0;
            this.lblProduto.Text = "Selecione o Produto:";
            // 
            // lblQtdSaida
            // 
            this.lblQtdSaida.AutoSize = true;
            this.lblQtdSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtdSaida.Location = new System.Drawing.Point(12, 141);
            this.lblQtdSaida.Name = "lblQtdSaida";
            this.lblQtdSaida.Size = new System.Drawing.Size(211, 24);
            this.lblQtdSaida.TabIndex = 1;
            this.lblQtdSaida.Text = "Quantidade da Saída:";
            // 
            // cmbProduto
            // 
            this.cmbProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduto.FormattingEnabled = true;
            this.cmbProduto.Location = new System.Drawing.Point(234, 66);
            this.cmbProduto.Name = "cmbProduto";
            this.cmbProduto.Size = new System.Drawing.Size(186, 21);
            this.cmbProduto.TabIndex = 2;
            // 
            // nudSaida
            // 
            this.nudSaida.Location = new System.Drawing.Point(234, 141);
            this.nudSaida.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSaida.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSaida.Name = "nudSaida";
            this.nudSaida.Size = new System.Drawing.Size(186, 20);
            this.nudSaida.TabIndex = 3;
            this.nudSaida.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(62, 218);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(137, 94);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar Saída";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(234, 216);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(135, 98);
            this.btnVoltar.TabIndex = 11;
            this.btnVoltar.Text = "Voltar ao Menu";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // FormSaida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 360);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.nudSaida);
            this.Controls.Add(this.cmbProduto);
            this.Controls.Add(this.lblQtdSaida);
            this.Controls.Add(this.lblProduto);
            this.Name = "FormSaida";
            this.Text = "FormSaida";
            ((System.ComponentModel.ISupportInitialize)(this.nudSaida)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.Label lblQtdSaida;
        private System.Windows.Forms.ComboBox cmbProduto;
        private System.Windows.Forms.NumericUpDown nudSaida;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnVoltar;
    }
}