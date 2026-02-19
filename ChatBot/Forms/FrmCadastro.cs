using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Linq;

namespace ChatBot
{
    public partial class FrmCadastro : Form
    {
        public List<dynamic> AlunosConfirmados = new List<dynamic>();
        private string _conexao;

        public FrmCadastro(List<string> nomes, string conexao)
        {
            InitializeComponent();
            _conexao = conexao;

            // Ajustes de Janela
            this.Text = "Cadastro de Novos Alunos";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            ConfigurarGradeCompacta();

            foreach (var nome in nomes)
            {
                dgvPendentes.Rows.Add(nome, "", "", "");
            }

            btnSalvar.Click += BtnSalvar_Click;

            // Evento de clique para copiar o nome
            dgvPendentes.CellClick += (s, e) => {
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    var val = dgvPendentes.Rows[e.RowIndex].Cells[0].Value?.ToString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        Clipboard.SetText(val);
                    }
                }
            };
        }

        private void ConfigurarGradeCompacta()
        {
            dgvPendentes.Location = new Point(12, 12);
            dgvPendentes.Size = new Size(560, 380);
            dgvPendentes.EditMode = DataGridViewEditMode.EditOnEnter;

            dgvPendentes.AllowUserToResizeColumns = false;
            dgvPendentes.AllowUserToResizeRows = false;
            dgvPendentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvPendentes.BackgroundColor = Color.White;
            dgvPendentes.RowHeadersVisible = false;
            dgvPendentes.Font = new Font("Segoe UI", 9f);
            dgvPendentes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvPendentes.Columns.Clear();
            dgvPendentes.Columns.Add("Nome", "ALUNO (Clique p/ Copiar)");
            dgvPendentes.Columns.Add("TelAluno", "TEL. ALUNO");
            dgvPendentes.Columns.Add("TelResp", "TEL. RESP.");
            dgvPendentes.Columns.Add("Email", "E-MAIL");

            dgvPendentes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPendentes.Columns[1].Width = 120;
            dgvPendentes.Columns[2].Width = 120;
            dgvPendentes.Columns[3].Width = 180;

            dgvPendentes.Columns[0].ReadOnly = true;
            dgvPendentes.AllowUserToAddRows = false;

            btnSalvar.Size = new Size(150, 40);
            btnSalvar.Location = new Point(422, 405);
            btnSalvar.Text = "Confirmar Tudo";
            btnSalvar.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnSalvar.BackColor = Color.LightGreen;
            btnSalvar.FlatStyle = FlatStyle.Flat;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_conexao))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dgvPendentes.Rows)
                    {
                        string nome = row.Cells[0].Value?.ToString();
                        string tA_bruto = row.Cells[1].Value?.ToString();
                        string tR_bruto = row.Cells[2].Value?.ToString();
                        string email = row.Cells[3].Value?.ToString();

                        // Limpeza profunda antes de gravar no Access
                        string tA = LimparEPadronizar(tA_bruto);
                        string tR = LimparEPadronizar(tR_bruto);

                        // Só salva se pelo menos um telefone for preenchido
                        if (!string.IsNullOrWhiteSpace(tA) || !string.IsNullOrWhiteSpace(tR))
                        {
                            string sql = "INSERT INTO Alunos (Nome, TelAluno, TelResponsavel, Email) VALUES (?, ?, ?, ?)";
                            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("?", nome);
                                cmd.Parameters.AddWithValue("?", tA);
                                cmd.Parameters.AddWithValue("?", tR);
                                cmd.Parameters.AddWithValue("?", email ?? "");
                                cmd.ExecuteNonQuery();
                            }

                            AlunosConfirmados.Add(new
                            {
                                Nome = nome,
                                TelAluno = tA,
                                TelResp = tR,
                                Email = email
                            });
                        }
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        private string LimparEPadronizar(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) return "";

            // Regex: Remove tudo que não for dígito
            string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(numero, @"[^\d]", "");

            // Lógica para DDI (Brasil = 55)
            // 10 dígitos = DDD + Número | 11 dígitos = DDD + 9 + Número
            if (apenasNumeros.Length == 10 || apenasNumeros.Length == 11)
            {
                apenasNumeros = "55" + apenasNumeros;
            }

            return apenasNumeros;
        }
    }
}