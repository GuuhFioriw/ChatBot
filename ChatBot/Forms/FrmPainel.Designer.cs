namespace ChatBot
{
    partial class FrmPainel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPainel));
            txtHistorico = new TextBox();
            txtMensagem = new TextBox();
            btnAnalisar = new Button();
            lblStatus = new Label();
            lblContador = new Label();
            btnEnviarMassa = new Button();
            panel1 = new Panel();
            BtnRegistros = new PictureBox();
            BtnConfig = new PictureBox();
            pnlStatusLed = new Panel();
            btnAlunos = new PictureBox();
            label1 = new Label();
            panel2 = new Panel();
            lblPorcentagem = new Label();
            pgbProgresso = new ProgressBar();
            label2 = new Label();
            btnDesconectar = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BtnRegistros).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnConfig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAlunos).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnDesconectar).BeginInit();
            SuspendLayout();
            // 
            // txtHistorico
            // 
            txtHistorico.Location = new Point(-5, 235);
            txtHistorico.Multiline = true;
            txtHistorico.Name = "txtHistorico";
            txtHistorico.ReadOnly = true;
            txtHistorico.ScrollBars = ScrollBars.Vertical;
            txtHistorico.Size = new Size(315, 141);
            txtHistorico.TabIndex = 0;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(1, 89);
            txtMensagem.Multiline = true;
            txtMensagem.Name = "txtMensagem";
            txtMensagem.ScrollBars = ScrollBars.Vertical;
            txtMensagem.Size = new Size(309, 82);
            txtMensagem.TabIndex = 0;
            // 
            // btnAnalisar
            // 
            btnAnalisar.Location = new Point(65, 206);
            btnAnalisar.Name = "btnAnalisar";
            btnAnalisar.Size = new Size(103, 23);
            btnAnalisar.TabIndex = 1;
            btnAnalisar.Text = "Enviar Planilha";
            btnAnalisar.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(3, 48);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(103, 15);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status: Iniciando...";
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.BackColor = Color.MidnightBlue;
            lblContador.ForeColor = Color.White;
            lblContador.Location = new Point(3, 107);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(0, 15);
            lblContador.TabIndex = 5;
            // 
            // btnEnviarMassa
            // 
            btnEnviarMassa.Location = new Point(180, 206);
            btnEnviarMassa.Name = "btnEnviarMassa";
            btnEnviarMassa.Size = new Size(124, 23);
            btnEnviarMassa.TabIndex = 6;
            btnEnviarMassa.Text = "Disparar Mensagem";
            btnEnviarMassa.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(BtnRegistros);
            panel1.Controls.Add(BtnConfig);
            panel1.Controls.Add(pnlStatusLed);
            panel1.Controls.Add(btnAlunos);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(-5, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(326, 45);
            panel1.TabIndex = 7;
            // 
            // BtnRegistros
            // 
            BtnRegistros.Cursor = Cursors.Hand;
            BtnRegistros.Image = Properties.Resources.icons8_archive_list_of_parts_30;
            BtnRegistros.Location = new Point(201, 6);
            BtnRegistros.Name = "BtnRegistros";
            BtnRegistros.Size = new Size(34, 30);
            BtnRegistros.SizeMode = PictureBoxSizeMode.Zoom;
            BtnRegistros.TabIndex = 12;
            BtnRegistros.TabStop = false;
            BtnRegistros.Click += BtnRegistros_Click;
            // 
            // BtnConfig
            // 
            BtnConfig.Cursor = Cursors.Hand;
            BtnConfig.Image = Properties.Resources.icons8_config_50;
            BtnConfig.Location = new Point(275, 7);
            BtnConfig.Name = "BtnConfig";
            BtnConfig.Size = new Size(34, 30);
            BtnConfig.SizeMode = PictureBoxSizeMode.Zoom;
            BtnConfig.TabIndex = 11;
            BtnConfig.TabStop = false;
            BtnConfig.Click += BtnConfig_Click;
            // 
            // pnlStatusLed
            // 
            pnlStatusLed.Location = new Point(17, 48);
            pnlStatusLed.Name = "pnlStatusLed";
            pnlStatusLed.Size = new Size(15, 15);
            pnlStatusLed.TabIndex = 9;
            // 
            // btnAlunos
            // 
            btnAlunos.Cursor = Cursors.Hand;
            btnAlunos.Image = Properties.Resources.icons8_contacts_50;
            btnAlunos.Location = new Point(238, 7);
            btnAlunos.Name = "btnAlunos";
            btnAlunos.Size = new Size(34, 30);
            btnAlunos.SizeMode = PictureBoxSizeMode.Zoom;
            btnAlunos.TabIndex = 9;
            btnAlunos.TabStop = false;
            btnAlunos.Click += btnAlunos_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(7, 12);
            label1.Name = "label1";
            label1.Size = new Size(162, 21);
            label1.TabIndex = 8;
            label1.Text = "ChatBot - Microlins";
            // 
            // panel2
            // 
            panel2.BackColor = Color.MidnightBlue;
            panel2.Controls.Add(lblPorcentagem);
            panel2.Controls.Add(pgbProgresso);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblContador);
            panel2.Location = new Point(1, 67);
            panel2.Name = "panel2";
            panel2.Size = new Size(309, 171);
            panel2.TabIndex = 8;
            // 
            // lblPorcentagem
            // 
            lblPorcentagem.AutoSize = true;
            lblPorcentagem.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPorcentagem.ForeColor = Color.White;
            lblPorcentagem.Location = new Point(275, 110);
            lblPorcentagem.Name = "lblPorcentagem";
            lblPorcentagem.Size = new Size(0, 13);
            lblPorcentagem.TabIndex = 7;
            // 
            // pgbProgresso
            // 
            pgbProgresso.Location = new Point(189, 112);
            pgbProgresso.Name = "pgbProgresso";
            pgbProgresso.Size = new Size(76, 10);
            pgbProgresso.TabIndex = 6;
            pgbProgresso.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(2, 2);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 0;
            label2.Text = "Mensagen: ";
            // 
            // btnDesconectar
            // 
            btnDesconectar.BackColor = Color.Transparent;
            btnDesconectar.Cursor = Cursors.Hand;
            btnDesconectar.Image = Properties.Resources.icons8_imac_exit_48;
            btnDesconectar.Location = new Point(274, 45);
            btnDesconectar.Name = "btnDesconectar";
            btnDesconectar.Size = new Size(28, 22);
            btnDesconectar.SizeMode = PictureBoxSizeMode.Zoom;
            btnDesconectar.TabIndex = 13;
            btnDesconectar.TabStop = false;
            btnDesconectar.Click += btnDesconectar_Click;
            // 
            // FrmPainel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(311, 376);
            Controls.Add(btnDesconectar);
            Controls.Add(panel1);
            Controls.Add(lblStatus);
            Controls.Add(btnEnviarMassa);
            Controls.Add(btnAnalisar);
            Controls.Add(txtMensagem);
            Controls.Add(txtHistorico);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmPainel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tela Principal";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BtnRegistros).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnConfig).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAlunos).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnDesconectar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtHistorico;
        private TextBox txtMensagem;
        private Button btnAnalisar;
        private Label lblStatus;
        private Label lblContador;
        private Button btnEnviarMassa;
        private Panel panel1;
        private Label label1;
        private PictureBox btnAlunos;
        private Panel panel2;
        private Label label2;
        private Panel pnlStatusLed;
        private ProgressBar pgbProgresso;
        private Label lblPorcentagem;
        private PictureBox BtnConfig;
        private PictureBox BtnRegistros;
        private PictureBox btnDesconectar;
    }
}
