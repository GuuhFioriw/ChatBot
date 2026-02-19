namespace ChatBot.Forms
{
    partial class FrmIdentificacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIdentificacao));
            panel1 = new Panel();
            label1 = new Label();
            label2 = new Label();
            txtNomeOperador = new TextBox();
            btnAcessar = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(274, 50);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(6, 9);
            label1.Name = "label1";
            label1.Size = new Size(262, 30);
            label1.TabIndex = 1;
            label1.Text = "Cadastro de Identificação";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 66);
            label2.Name = "label2";
            label2.Size = new Size(62, 13);
            label2.TabIndex = 1;
            label2.Text = "Operador :";
            // 
            // txtNomeOperador
            // 
            txtNomeOperador.Location = new Point(6, 82);
            txtNomeOperador.Name = "txtNomeOperador";
            txtNomeOperador.Size = new Size(262, 23);
            txtNomeOperador.TabIndex = 2;
            // 
            // btnAcessar
            // 
            btnAcessar.Location = new Point(193, 111);
            btnAcessar.Name = "btnAcessar";
            btnAcessar.Size = new Size(75, 23);
            btnAcessar.TabIndex = 3;
            btnAcessar.Text = "Salvar";
            btnAcessar.UseVisualStyleBackColor = true;
            btnAcessar.Click += btnAcessar_Click;
            // 
            // FrmIdentificacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(274, 144);
            Controls.Add(btnAcessar);
            Controls.Add(txtNomeOperador);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmIdentificacao";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastro de Identificação";
            Load += FrmIdentificacao_Load;
            MouseDown += FrmIdentificacao_MouseDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private TextBox txtNomeOperador;
        private Button btnAcessar;
    }
}