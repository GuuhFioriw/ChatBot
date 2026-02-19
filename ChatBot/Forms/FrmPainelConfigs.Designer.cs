namespace ChatBot.Forms
{
    partial class FrmPainelConfigs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPainelConfigs));
            BtnconfigEmail = new PictureBox();
            panel1 = new Panel();
            btnTrocarOperador = new PictureBox();
            label4 = new Label();
            lbl23 = new Label();
            BtnReset = new PictureBox();
            label2 = new Label();
            BtnConfigDb = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)BtnconfigEmail).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnTrocarOperador).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnReset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnConfigDb).BeginInit();
            SuspendLayout();
            // 
            // BtnconfigEmail
            // 
            BtnconfigEmail.Cursor = Cursors.Hand;
            BtnconfigEmail.Image = Properties.Resources.icons8_email_60;
            BtnconfigEmail.Location = new Point(153, 4);
            BtnconfigEmail.Name = "BtnconfigEmail";
            BtnconfigEmail.Size = new Size(34, 30);
            BtnconfigEmail.SizeMode = PictureBoxSizeMode.Zoom;
            BtnconfigEmail.TabIndex = 11;
            BtnconfigEmail.TabStop = false;
            BtnconfigEmail.Click += BtnconfigEmail_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(btnTrocarOperador);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lbl23);
            panel1.Controls.Add(BtnReset);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(BtnConfigDb);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(BtnconfigEmail);
            panel1.Location = new Point(-4, 44);
            panel1.Name = "panel1";
            panel1.Size = new Size(299, 52);
            panel1.TabIndex = 12;
            // 
            // btnTrocarOperador
            // 
            btnTrocarOperador.Cursor = Cursors.Hand;
            btnTrocarOperador.Image = Properties.Resources.icons8_operator_48;
            btnTrocarOperador.Location = new Point(30, 4);
            btnTrocarOperador.Name = "btnTrocarOperador";
            btnTrocarOperador.Size = new Size(34, 30);
            btnTrocarOperador.SizeMode = PictureBoxSizeMode.Zoom;
            btnTrocarOperador.TabIndex = 18;
            btnTrocarOperador.TabStop = false;
            btnTrocarOperador.Click += btnTrocarOperador_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(19, 37);
            label4.Name = "label4";
            label4.Size = new Size(56, 13);
            label4.TabIndex = 17;
            label4.Text = "Operador";
            // 
            // lbl23
            // 
            lbl23.AutoSize = true;
            lbl23.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl23.ForeColor = Color.Transparent;
            lbl23.Location = new Point(92, 37);
            lbl23.Name = "lbl23";
            lbl23.Size = new Size(35, 13);
            lbl23.TabIndex = 16;
            lbl23.Text = "Reset";
            // 
            // BtnReset
            // 
            BtnReset.Cursor = Cursors.Hand;
            BtnReset.Image = Properties.Resources.icons8_restart_50;
            BtnReset.Location = new Point(92, 4);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(34, 30);
            BtnReset.SizeMode = PictureBoxSizeMode.Zoom;
            BtnReset.TabIndex = 15;
            BtnReset.TabStop = false;
            BtnReset.Click += BtnReset_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(204, 37);
            label2.Name = "label2";
            label2.Size = new Size(90, 13);
            label2.TabIndex = 14;
            label2.Text = "Banco de Dados";
            // 
            // BtnConfigDb
            // 
            BtnConfigDb.Cursor = Cursors.Hand;
            BtnConfigDb.Image = Properties.Resources.icons8_database_50;
            BtnConfigDb.Location = new Point(232, 4);
            BtnConfigDb.Name = "BtnConfigDb";
            BtnConfigDb.Size = new Size(34, 30);
            BtnConfigDb.SizeMode = PictureBoxSizeMode.Zoom;
            BtnConfigDb.TabIndex = 13;
            BtnConfigDb.TabStop = false;
            BtnConfigDb.Click += BtnConfigDb_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(151, 37);
            label1.Name = "label1";
            label1.Size = new Size(39, 13);
            label1.TabIndex = 12;
            label1.Text = "E-mail";
            // 
            // FrmPainelConfigs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 141);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmPainelConfigs";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Painel Configurações";
            ((System.ComponentModel.ISupportInitialize)BtnconfigEmail).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnTrocarOperador).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnReset).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnConfigDb).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox BtnconfigEmail;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private PictureBox BtnConfigDb;
        private Label lbl23;
        private PictureBox BtnReset;
        private Label label4;
        private PictureBox btnTrocarOperador;
    }
}