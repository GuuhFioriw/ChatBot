namespace ChatBot
{
    partial class FrmGerenciarAlunos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGerenciarAlunos));
            Btnadicionar = new Button();
            Btnapagar = new Button();
            txtNome = new TextBox();
            txtTelAluno = new TextBox();
            txtTelResp = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtPesquisa = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtEmail = new TextBox();
            BtnImportar = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            lblTotalAlunos = new Label();
            lblTotalEmails = new Label();
            lblTotalTels = new Label();
            dataalunos = new DataGridView();
            panel3 = new Panel();
            cmbFiltroTelResp = new ComboBox();
            cmbFiltroTelAluno = new ComboBox();
            cmbFiltroEmail = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataalunos).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // Btnadicionar
            // 
            Btnadicionar.Location = new Point(17, 182);
            Btnadicionar.Name = "Btnadicionar";
            Btnadicionar.Size = new Size(75, 23);
            Btnadicionar.TabIndex = 0;
            Btnadicionar.Text = "Atualizar";
            Btnadicionar.UseVisualStyleBackColor = true;
            Btnadicionar.Click += Btnadicionar_Click;
            // 
            // Btnapagar
            // 
            Btnapagar.Location = new Point(98, 182);
            Btnapagar.Name = "Btnapagar";
            Btnapagar.Size = new Size(75, 23);
            Btnapagar.TabIndex = 1;
            Btnapagar.Text = "Excluir";
            Btnapagar.UseVisualStyleBackColor = true;
            Btnapagar.Click += Btnapagar_Click;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(12, 25);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(277, 23);
            txtNome.TabIndex = 3;
            // 
            // txtTelAluno
            // 
            txtTelAluno.Location = new Point(12, 69);
            txtTelAluno.Name = "txtTelAluno";
            txtTelAluno.Size = new Size(277, 23);
            txtTelAluno.TabIndex = 4;
            // 
            // txtTelResp
            // 
            txtTelResp.Location = new Point(12, 111);
            txtTelResp.Name = "txtTelResp";
            txtTelResp.Size = new Size(277, 23);
            txtTelResp.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.DarkBlue;
            label1.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(90, 13);
            label1.TabIndex = 6;
            label1.Text = "Nome do Aluno:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.DarkBlue;
            label2.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 53);
            label2.Name = "label2";
            label2.Size = new Size(102, 13);
            label2.TabIndex = 7;
            label2.Text = "Telefone do Aluno:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.DarkBlue;
            label3.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 95);
            label3.Name = "label3";
            label3.Size = new Size(137, 13);
            label3.TabIndex = 8;
            label3.Text = "Telefone do Responsavel:";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(507, 182);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(180, 23);
            txtPesquisa.TabIndex = 9;
            txtPesquisa.TextChanged += txtPesquisa_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.DarkBlue;
            label4.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(414, 186);
            label4.Name = "label4";
            label4.Size = new Size(90, 13);
            label4.TabIndex = 10;
            label4.Text = "Pesquisar Aluno:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.DarkBlue;
            label5.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(12, 137);
            label5.Name = "label5";
            label5.Size = new Size(42, 13);
            label5.TabIndex = 12;
            label5.Text = "E-mail:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(12, 153);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(277, 23);
            txtEmail.TabIndex = 11;
            // 
            // BtnImportar
            // 
            BtnImportar.Location = new Point(179, 181);
            BtnImportar.Name = "BtnImportar";
            BtnImportar.Size = new Size(103, 25);
            BtnImportar.TabIndex = 13;
            BtnImportar.Text = "Importar E-mails";
            BtnImportar.UseVisualStyleBackColor = true;
            BtnImportar.Click += BtnImportar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(301, 219);
            panel1.TabIndex = 14;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkBlue;
            panel2.Location = new Point(295, 171);
            panel2.Name = "panel2";
            panel2.Size = new Size(409, 42);
            panel2.TabIndex = 15;
            // 
            // lblTotalAlunos
            // 
            lblTotalAlunos.AutoSize = true;
            lblTotalAlunos.Location = new Point(4, 476);
            lblTotalAlunos.Name = "lblTotalAlunos";
            lblTotalAlunos.Size = new Size(12, 15);
            lblTotalAlunos.TabIndex = 17;
            lblTotalAlunos.Text = "-";
            // 
            // lblTotalEmails
            // 
            lblTotalEmails.AutoSize = true;
            lblTotalEmails.Location = new Point(295, 476);
            lblTotalEmails.Name = "lblTotalEmails";
            lblTotalEmails.Size = new Size(12, 15);
            lblTotalEmails.TabIndex = 18;
            lblTotalEmails.Text = "-";
            // 
            // lblTotalTels
            // 
            lblTotalTels.AutoSize = true;
            lblTotalTels.Location = new Point(520, 476);
            lblTotalTels.Name = "lblTotalTels";
            lblTotalTels.Size = new Size(12, 15);
            lblTotalTels.TabIndex = 19;
            lblTotalTels.Text = "-";
            // 
            // dataalunos
            // 
            dataalunos.AllowUserToResizeRows = false;
            dataalunos.BackgroundColor = Color.White;
            dataalunos.BorderStyle = BorderStyle.None;
            dataalunos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataalunos.Location = new Point(-1, 211);
            dataalunos.MultiSelect = false;
            dataalunos.Name = "dataalunos";
            dataalunos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataalunos.Size = new Size(705, 256);
            dataalunos.TabIndex = 2;
            dataalunos.CellClick += dataalunos_CellClick;
            dataalunos.CellMouseEnter += dataalunos_CellMouseEnter;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkBlue;
            panel3.Controls.Add(cmbFiltroTelResp);
            panel3.Controls.Add(cmbFiltroTelAluno);
            panel3.Controls.Add(cmbFiltroEmail);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label6);
            panel3.Location = new Point(327, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(377, 150);
            panel3.TabIndex = 20;
            // 
            // cmbFiltroTelResp
            // 
            cmbFiltroTelResp.FormattingEnabled = true;
            cmbFiltroTelResp.Location = new Point(234, 84);
            cmbFiltroTelResp.Name = "cmbFiltroTelResp";
            cmbFiltroTelResp.Size = new Size(93, 23);
            cmbFiltroTelResp.TabIndex = 6;
            cmbFiltroTelResp.SelectedIndexChanged += cmbFiltroTelResp_SelectedIndexChanged;
            // 
            // cmbFiltroTelAluno
            // 
            cmbFiltroTelAluno.FormattingEnabled = true;
            cmbFiltroTelAluno.Location = new Point(137, 84);
            cmbFiltroTelAluno.Name = "cmbFiltroTelAluno";
            cmbFiltroTelAluno.Size = new Size(93, 23);
            cmbFiltroTelAluno.TabIndex = 5;
            cmbFiltroTelAluno.SelectedIndexChanged += cmbFiltroTelAluno_SelectedIndexChanged;
            // 
            // cmbFiltroEmail
            // 
            cmbFiltroEmail.FormattingEnabled = true;
            cmbFiltroEmail.Location = new Point(38, 86);
            cmbFiltroEmail.Name = "cmbFiltroEmail";
            cmbFiltroEmail.Size = new Size(93, 23);
            cmbFiltroEmail.TabIndex = 4;
            cmbFiltroEmail.SelectedIndexChanged += cmbFiltroEmail_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(249, 66);
            label9.Name = "label9";
            label9.Size = new Size(63, 17);
            label9.TabIndex = 3;
            label9.Text = "Tell Resp";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(148, 66);
            label8.Name = "label8";
            label8.Size = new Size(71, 17);
            label8.TabIndex = 2;
            label8.Text = "Tell Aluno";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(61, 66);
            label7.Name = "label7";
            label7.Size = new Size(47, 17);
            label7.TabIndex = 1;
            label7.Text = "E-mail";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(87, 25);
            label6.Name = "label6";
            label6.Size = new Size(197, 25);
            label6.TabIndex = 0;
            label6.Text = "Filtros para Contatos";
            // 
            // FrmGerenciarAlunos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 500);
            Controls.Add(panel3);
            Controls.Add(lblTotalTels);
            Controls.Add(lblTotalEmails);
            Controls.Add(lblTotalAlunos);
            Controls.Add(BtnImportar);
            Controls.Add(label5);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(txtPesquisa);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTelResp);
            Controls.Add(txtTelAluno);
            Controls.Add(txtNome);
            Controls.Add(dataalunos);
            Controls.Add(Btnapagar);
            Controls.Add(Btnadicionar);
            Controls.Add(panel1);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmGerenciarAlunos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Central Aluno";
            Load += FrmGerenciarAlunos_Load;
            ((System.ComponentModel.ISupportInitialize)dataalunos).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btnadicionar;
        private Button Btnapagar;
        private TextBox txtNome;
        private TextBox txtTelAluno;
        private TextBox txtTelResp;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtPesquisa;
        private Label label4;
        private Label label5;
        private TextBox txtEmail;
        private Button BtnImportar;
        private Panel panel1;
        private Panel panel2;
        private Label lblTotalAlunos;
        private Label lblTotalEmails;
        private Label lblTotalTels;
        private DataGridView dataalunos;
        private Panel panel3;
        private Label label6;
        private Label label9;
        private Label label8;
        private Label label7;
        private ComboBox cmbFiltroTelResp;
        private ComboBox cmbFiltroTelAluno;
        private ComboBox cmbFiltroEmail;
    }
}