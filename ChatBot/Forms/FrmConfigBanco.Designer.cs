namespace ChatBot.Forms
{
    partial class FrmConfigBanco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigBanco));
            btnProcurar = new Button();
            btnSalvar = new Button();
            txtCaminho = new TextBox();
            btnRestaurarPadrao = new Button();
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnProcurar
            // 
            btnProcurar.Location = new Point(176, 90);
            btnProcurar.Name = "btnProcurar";
            btnProcurar.Size = new Size(75, 23);
            btnProcurar.TabIndex = 0;
            btnProcurar.Text = "Procurar";
            btnProcurar.UseVisualStyleBackColor = true;
            btnProcurar.Click += btnProcurar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(257, 90);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(75, 23);
            btnSalvar.TabIndex = 1;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // txtCaminho
            // 
            txtCaminho.Location = new Point(12, 61);
            txtCaminho.Name = "txtCaminho";
            txtCaminho.Size = new Size(320, 23);
            txtCaminho.TabIndex = 2;
            // 
            // btnRestaurarPadrao
            // 
            btnRestaurarPadrao.FlatStyle = FlatStyle.System;
            btnRestaurarPadrao.Location = new Point(12, 153);
            btnRestaurarPadrao.Name = "btnRestaurarPadrao";
            btnRestaurarPadrao.Size = new Size(75, 23);
            btnRestaurarPadrao.TabIndex = 3;
            btnRestaurarPadrao.Text = "Baixar DB";
            btnRestaurarPadrao.UseVisualStyleBackColor = true;
            btnRestaurarPadrao.Click += btnRestaurarPadrao_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 136);
            label1.Name = "label1";
            label1.Size = new Size(146, 13);
            label1.TabIndex = 4;
            label1.Text = "Perdeu o Banco de Dados ?";
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(342, 40);
            panel1.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Book Antiqua", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(153, 23);
            label2.TabIndex = 6;
            label2.Text = "Banco de Dados";
            // 
            // FrmConfigBanco
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(338, 182);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(btnRestaurarPadrao);
            Controls.Add(txtCaminho);
            Controls.Add(btnSalvar);
            Controls.Add(btnProcurar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmConfigBanco";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Banco de Dados";
            Load += FrmConfigBanco_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnProcurar;
        private Button btnSalvar;
        private TextBox txtCaminho;
        private Button btnRestaurarPadrao;
        private Label label1;
        private Panel panel1;
        private Label label2;
    }
}