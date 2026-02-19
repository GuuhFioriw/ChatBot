namespace ChatBot
{
    partial class FrmConfigEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigEmail));
            lblEmail = new Label();
            lblSenha = new Label();
            lblNomeUnidade = new Label();
            txtEmail = new TextBox();
            txtSenha = new TextBox();
            txtNomeUnidade = new TextBox();
            btnSalvar = new Button();
            cmbEmails = new ComboBox();
            lblAssunto = new Label();
            txtAssunto = new TextBox();
            SuspendLayout();
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(7, 30);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(134, 15);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "E-mail de Envio (Gmail):";
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Location = new Point(7, 74);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(145, 15);
            lblSenha.TabIndex = 1;
            lblSenha.Text = "Senha de App (16 dígitos):";
            // 
            // lblNomeUnidade
            // 
            lblNomeUnidade.AutoSize = true;
            lblNomeUnidade.Location = new Point(7, 122);
            lblNomeUnidade.Name = "lblNomeUnidade";
            lblNomeUnidade.Size = new Size(106, 15);
            lblNomeUnidade.TabIndex = 2;
            lblNomeUnidade.Text = "Nome da Unidade:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(7, 48);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(215, 23);
            txtEmail.TabIndex = 3;
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(7, 92);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(215, 23);
            txtSenha.TabIndex = 4;
            // 
            // txtNomeUnidade
            // 
            txtNomeUnidade.Location = new Point(7, 140);
            txtNomeUnidade.Name = "txtNomeUnidade";
            txtNomeUnidade.Size = new Size(215, 23);
            txtNomeUnidade.TabIndex = 5;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(147, 222);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 23);
            btnSalvar.TabIndex = 6;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // cmbEmails
            // 
            cmbEmails.FormattingEnabled = true;
            cmbEmails.Location = new Point(7, 4);
            cmbEmails.Name = "cmbEmails";
            cmbEmails.Size = new Size(215, 23);
            cmbEmails.TabIndex = 7;
            cmbEmails.SelectedIndexChanged += cmbEmails_SelectedIndexChanged;
            // 
            // lblAssunto
            // 
            lblAssunto.AutoSize = true;
            lblAssunto.Location = new Point(7, 174);
            lblAssunto.Name = "lblAssunto";
            lblAssunto.Size = new Size(107, 15);
            lblAssunto.TabIndex = 8;
            lblAssunto.Text = "Assunto do E-mail:";
            // 
            // txtAssunto
            // 
            txtAssunto.Location = new Point(7, 192);
            txtAssunto.Name = "txtAssunto";
            txtAssunto.Size = new Size(215, 23);
            txtAssunto.TabIndex = 9;
            // 
            // FrmConfigEmail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(227, 250);
            Controls.Add(txtAssunto);
            Controls.Add(lblAssunto);
            Controls.Add(cmbEmails);
            Controls.Add(btnSalvar);
            Controls.Add(txtNomeUnidade);
            Controls.Add(txtSenha);
            Controls.Add(txtEmail);
            Controls.Add(lblNomeUnidade);
            Controls.Add(lblSenha);
            Controls.Add(lblEmail);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmConfigEmail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastro de E-mail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEmail;
        private Label lblSenha;
        private Label lblNomeUnidade;
        private TextBox txtEmail;
        private TextBox txtSenha;
        private TextBox txtNomeUnidade;
        private Button btnSalvar;
        private ComboBox cmbEmails;
        private Label lblAssunto;
        private TextBox txtAssunto;
    }
}