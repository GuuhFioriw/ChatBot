namespace ChatBot
{
    partial class FrmCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadastro));
            dgvPendentes = new DataGridView();
            btnSalvar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPendentes).BeginInit();
            SuspendLayout();
            // 
            // dgvPendentes
            // 
            dgvPendentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPendentes.Location = new Point(8, 12);
            dgvPendentes.Name = "dgvPendentes";
            dgvPendentes.Size = new Size(644, 270);
            dgvPendentes.TabIndex = 8;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(573, 288);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 23);
            btnSalvar.TabIndex = 9;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            // 
            // FrmCadastro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(660, 318);
            Controls.Add(btnSalvar);
            Controls.Add(dgvPendentes);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmCadastro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastro";
            ((System.ComponentModel.ISupportInitialize)dgvPendentes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvPendentes;
        private Button btnSalvar;
    }
}