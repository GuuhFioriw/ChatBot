namespace ChatBot.Forms
{
    partial class FrmRegistroMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistroMsg));
            panel1 = new Panel();
            panel3 = new Panel();
            label5 = new Label();
            dtpDataInicio = new DateTimePicker();
            dtpDataFim = new DateTimePicker();
            label3 = new Label();
            cboStatus = new ComboBox();
            label1 = new Label();
            DgvMsg = new DataGridView();
            LblQuantidade = new Label();
            label4 = new Label();
            BtnExportar = new Button();
            txtPesquisaAluno = new TextBox();
            CbmOperador = new ComboBox();
            label2 = new Label();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvMsg).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1254, 85);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BackColor = Color.White;
            panel3.Controls.Add(label5);
            panel3.Controls.Add(dtpDataInicio);
            panel3.Controls.Add(dtpDataFim);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(cboStatus);
            panel3.Location = new Point(0, 47);
            panel3.Name = "panel3";
            panel3.Size = new Size(1254, 38);
            panel3.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(898, 14);
            label5.Name = "label5";
            label5.Size = new Size(23, 13);
            label5.TabIndex = 18;
            label5.Text = "até";
            // 
            // dtpDataInicio
            // 
            dtpDataInicio.Format = DateTimePickerFormat.Short;
            dtpDataInicio.Location = new Point(811, 9);
            dtpDataInicio.Name = "dtpDataInicio";
            dtpDataInicio.Size = new Size(85, 23);
            dtpDataInicio.TabIndex = 0;
            dtpDataInicio.ValueChanged += dtpDataInicio_ValueChanged;
            // 
            // dtpDataFim
            // 
            dtpDataFim.Format = DateTimePickerFormat.Short;
            dtpDataFim.Location = new Point(923, 9);
            dtpDataFim.Name = "dtpDataFim";
            dtpDataFim.Size = new Size(85, 23);
            dtpDataFim.TabIndex = 2;
            dtpDataFim.ValueChanged += dtpDataFim_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(646, 14);
            label3.Name = "label3";
            label3.Size = new Size(47, 13);
            label3.TabIndex = 17;
            label3.Text = "Status : ";
            // 
            // cboStatus
            // 
            cboStatus.FormattingEnabled = true;
            cboStatus.Items.AddRange(new object[] { "Todos", "Sucesso", "Erro" });
            cboStatus.Location = new Point(693, 9);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(82, 23);
            cboStatus.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(238, 26);
            label1.TabIndex = 9;
            label1.Text = "Registros de Mensagens";
            // 
            // DgvMsg
            // 
            DgvMsg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvMsg.Dock = DockStyle.Fill;
            DgvMsg.Location = new Point(0, 85);
            DgvMsg.Name = "DgvMsg";
            DgvMsg.Size = new Size(1254, 331);
            DgvMsg.TabIndex = 1;
            // 
            // LblQuantidade
            // 
            LblQuantidade.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LblQuantidade.AutoSize = true;
            LblQuantidade.BackColor = Color.White;
            LblQuantidade.Location = new Point(1108, 58);
            LblQuantidade.Name = "LblQuantidade";
            LblQuantidade.Size = new Size(12, 15);
            LblQuantidade.TabIndex = 2;
            LblQuantidade.Text = "-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(3, 60);
            label4.Name = "label4";
            label4.Size = new Size(90, 13);
            label4.TabIndex = 11;
            label4.Text = "Pesquisar Aluno:";
            // 
            // BtnExportar
            // 
            BtnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExportar.BackColor = Color.White;
            BtnExportar.Location = new Point(1015, 56);
            BtnExportar.Name = "BtnExportar";
            BtnExportar.Size = new Size(75, 23);
            BtnExportar.TabIndex = 13;
            BtnExportar.Text = "Exportar";
            BtnExportar.UseVisualStyleBackColor = false;
            BtnExportar.Click += BtnExportar_Click;
            // 
            // txtPesquisaAluno
            // 
            txtPesquisaAluno.BackColor = Color.White;
            txtPesquisaAluno.Location = new Point(99, 55);
            txtPesquisaAluno.Name = "txtPesquisaAluno";
            txtPesquisaAluno.Size = new Size(204, 23);
            txtPesquisaAluno.TabIndex = 14;
            txtPesquisaAluno.TextChanged += txtPesquisaAluno_TextChanged;
            txtPesquisaAluno.Enter += txtPesquisaAluno_Enter;
            txtPesquisaAluno.Leave += txtPesquisaAluno_Leave;
            // 
            // CbmOperador
            // 
            CbmOperador.FormattingEnabled = true;
            CbmOperador.Location = new Point(425, 56);
            CbmOperador.Name = "CbmOperador";
            CbmOperador.Size = new Size(215, 23);
            CbmOperador.TabIndex = 15;
            CbmOperador.SelectedIndexChanged += CbmOperador_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(309, 61);
            label2.Name = "label2";
            label2.Size = new Size(110, 13);
            label2.TabIndex = 16;
            label2.Text = "Pesquisar Operador:";
            // 
            // FrmRegistroMsg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1254, 416);
            Controls.Add(label2);
            Controls.Add(CbmOperador);
            Controls.Add(txtPesquisaAluno);
            Controls.Add(BtnExportar);
            Controls.Add(label4);
            Controls.Add(LblQuantidade);
            Controls.Add(DgvMsg);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmRegistroMsg";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registros de Mensagens";
            WindowState = FormWindowState.Maximized;
            Load += FrmRegistroMsg_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DgvMsg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView DgvMsg;
        private Label LblQuantidade;
        private Label label4;
        private Button BtnExportar;
        private TextBox txtPesquisaAluno;
        private ComboBox CbmOperador;
        private Label label2;
        private Panel panel3;
        private DateTimePicker dtpDataInicio;
        private ComboBox cboStatus;
        private DateTimePicker dtpDataFim;
        private Label label3;
        private Label label5;
    }
}