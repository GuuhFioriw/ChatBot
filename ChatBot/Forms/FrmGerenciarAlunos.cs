using System;
using System.Collections.Generic; // Adicionado para suporte a List
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ChatBot
{
    public partial class FrmGerenciarAlunos : Form
    {
        private string strConexao;
        private int idSelecionado = 0;

        public FrmGerenciarAlunos()
        {
            InitializeComponent();

            string path = Properties.Settings.Default.CaminhoBanco;
            this.strConexao = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};";

            ConfigurarDicas();
        }

        private void FrmGerenciarAlunos_Load(object sender, EventArgs e)
        {
            // Configura os itens dos ComboBoxes de Filtro
            cmbFiltroEmail.Items.Clear();
            cmbFiltroEmail.Items.AddRange(new string[] { "Todos", "Não Cadastrado", "Cadastrados" });
            cmbFiltroEmail.SelectedIndex = 0;

            cmbFiltroTelAluno.Items.Clear();
            cmbFiltroTelAluno.Items.AddRange(new string[] { "Todos", "Sem Numero", "Cadastrados" });
            cmbFiltroTelAluno.SelectedIndex = 0;

            cmbFiltroTelResp.Items.Clear();
            cmbFiltroTelResp.Items.AddRange(new string[] { "Todos", "Sem Numero", "Cadastrados" });
            cmbFiltroTelResp.SelectedIndex = 0;

            CarregarDados();
        }

        private void CarregarDados()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.CaminhoBanco))
            {
                MessageBox.Show("O banco de dados não foi configurado. Vá em Configurações.", "Erro");
                return;
            }
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    string query = "SELECT [Código], Nome, Email, TelAluno, TelResponsavel FROM Alunos ORDER BY Nome ASC";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int totalAlunos = dt.Rows.Count;
                    int totalEmails = 0;
                    int totalTels = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["Email"].ToString())) totalEmails++;
                        if (!string.IsNullOrWhiteSpace(row["TelAluno"].ToString()) || !string.IsNullOrWhiteSpace(row["TelResponsavel"].ToString())) totalTels++;

                        if (string.IsNullOrWhiteSpace(row["TelAluno"].ToString())) row["TelAluno"] = "Sem número";
                        if (string.IsNullOrWhiteSpace(row["TelResponsavel"].ToString())) row["TelResponsavel"] = "Sem número";
                        if (string.IsNullOrWhiteSpace(row["Email"].ToString())) row["Email"] = "Não cadastrado";
                    }

                    dataalunos.DataSource = dt;

                    lblTotalAlunos.Text = $"Total de Alunos: {totalAlunos}";
                    lblTotalEmails.Text = $"E-mails: {totalEmails}";
                    lblTotalTels.Text = $"Com Telefone: {totalTels}";

                    dataalunos.ReadOnly = true;
                    dataalunos.AllowUserToAddRows = false;
                    dataalunos.AllowUserToDeleteRows = false;
                    dataalunos.AllowUserToOrderColumns = false;
                    dataalunos.AllowUserToResizeColumns = false;
                    dataalunos.AllowUserToResizeRows = false;
                    dataalunos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    dataalunos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataalunos.MultiSelect = false;
                    dataalunos.EditMode = DataGridViewEditMode.EditProgrammatically;
                    dataalunos.RowHeadersVisible = false;
                    dataalunos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 235);
                    dataalunos.BackgroundColor = Color.White;

                    if (dataalunos.Columns.Contains("Código")) dataalunos.Columns["Código"].Visible = false;

                    dataalunos.Columns["Nome"].DisplayIndex = 0;
                    dataalunos.Columns["Email"].DisplayIndex = 1;
                    dataalunos.Columns["TelAluno"].DisplayIndex = 2;
                    dataalunos.Columns["TelResponsavel"].DisplayIndex = 3;

                    dataalunos.Columns["Nome"].HeaderText = "Nome do Aluno";
                    dataalunos.Columns["Email"].HeaderText = "E-mail";
                    dataalunos.Columns["TelAluno"].HeaderText = "Tell Aluno";
                    dataalunos.Columns["TelResponsavel"].HeaderText = "Tell Responsável";

                    dataalunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataalunos.Columns["Email"].Width = 180;
                    dataalunos.Columns["TelAluno"].Width = 110;
                    dataalunos.Columns["TelResponsavel"].Width = 110;
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar: " + ex.Message); }
        }

        // --- NOVA FUNÇÃO DE FILTRO UNIFICADO ---
        private void AplicarFiltros()
        {
            if (dataalunos.DataSource == null) return;

            DataView dv = ((DataTable)dataalunos.DataSource).DefaultView;
            List<string> filtros = new List<string>();

            // Filtro de E-mail
            if (cmbFiltroEmail.Text == "Não Cadastrado")
                filtros.Add("Email = 'Não cadastrado'");
            else if (cmbFiltroEmail.Text == "Cadastrados")
                filtros.Add("Email <> 'Não cadastrado'");

            // Filtro de Tel Aluno
            if (cmbFiltroTelAluno.Text == "Sem Numero")
                filtros.Add("TelAluno = 'Sem número'");
            else if (cmbFiltroTelAluno.Text == "Cadastrados")
                filtros.Add("TelAluno <> 'Sem número'");

            // Filtro de Tel Responsável
            if (cmbFiltroTelResp.Text == "Sem Numero")
                filtros.Add("TelResponsavel = 'Sem número'");
            else if (cmbFiltroTelResp.Text == "Cadastrados")
                filtros.Add("TelResponsavel <> 'Sem número'");

            // Filtro de Texto (Pesquisa)
            if (!string.IsNullOrWhiteSpace(txtPesquisa.Text))
            {
                string busca = txtPesquisa.Text.Replace("'", "''");
                filtros.Add($"(Nome LIKE '%{busca}%' OR Email LIKE '%{busca}%' OR TelAluno LIKE '%{busca}%' OR TelResponsavel LIKE '%{busca}%')");
            }

            // Aplica todos os filtros juntos
            dv.RowFilter = string.Join(" AND ", filtros);
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        // Vincule estes eventos no Designer (SelectedIndexChanged)
        private void cmbFiltroEmail_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltros();
        private void cmbFiltroTelAluno_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltros();
        private void cmbFiltroTelResp_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltros();

        private string FormatarTelefone(string tel)
        {
            if (string.IsNullOrWhiteSpace(tel) || tel == "Sem número") return "";
            string apenasNumeros = Regex.Replace(tel, @"[^\d]", "");
            if (string.IsNullOrWhiteSpace(apenasNumeros)) return "";
            if (!apenasNumeros.StartsWith("55") && apenasNumeros.Length >= 10) apenasNumeros = "55" + apenasNumeros;
            return apenasNumeros;
        }

        private void Btnadicionar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text)) { MessageBox.Show("Informe o nome!"); return; }

            string telAlunoFormatado = FormatarTelefone(txtTelAluno.Text);
            string telRespFormatado = FormatarTelefone(txtTelResp.Text);

            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();
                    string sql = idSelecionado == 0
                        ? "INSERT INTO Alunos (Nome, Email, TelAluno, TelResponsavel) VALUES (?, ?, ?, ?)"
                        : "UPDATE Alunos SET Nome=?, Email=?, TelAluno=?, TelResponsavel=? WHERE [Código]=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", txtNome.Text);
                        cmd.Parameters.AddWithValue("?", txtEmail.Text);
                        cmd.Parameters.AddWithValue("?", telAlunoFormatado);
                        cmd.Parameters.AddWithValue("?", telRespFormatado);
                        if (idSelecionado != 0) cmd.Parameters.AddWithValue("?", idSelecionado);
                        cmd.ExecuteNonQuery();
                    }
                }
                LimparCampos();
                CarregarDados();
                MessageBox.Show("Salvo com sucesso!");
            }
            catch (Exception ex) { MessageBox.Show("Erro ao salvar: " + ex.Message); }
        }

        private void Btnapagar_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0) { MessageBox.Show("Selecione um aluno na lista."); return; }
            var res = MessageBox.Show("Deseja excluir este aluno?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(strConexao))
                    {
                        conn.Open();
                        string sql = "DELETE FROM Alunos WHERE [Código] = ?";
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("?", idSelecionado);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LimparCampos();
                    CarregarDados();
                    MessageBox.Show("Removido!");
                }
                catch (Exception ex) { MessageBox.Show("Erro ao excluir: " + ex.Message); }
            }
        }

        private void BtnImportar_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog { Filter = "Excel Files (*.xlsx)|*.xlsx" };
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strConexaoExcel = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={open.FileName};Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1';";
                    using (OleDbConnection connExcel = new OleDbConnection(strConexaoExcel))
                    {
                        connExcel.Open();
                        OleDbCommand cmdExcel = new OleDbCommand("SELECT * FROM [Planilha1$]", connExcel);
                        using (OleDbDataReader reader = cmdExcel.ExecuteReader())
                        {
                            using (OleDbConnection connAccess = new OleDbConnection(strConexao))
                            {
                                connAccess.Open();
                                while (reader.Read())
                                {
                                    string nome = reader[0]?.ToString().Trim();
                                    string email = reader[1]?.ToString().Trim();
                                    if (string.IsNullOrEmpty(nome)) continue;
                                    using (OleDbCommand cmd = new OleDbCommand("UPDATE Alunos SET Email = ? WHERE Nome = ?", connAccess))
                                    {
                                        cmd.Parameters.AddWithValue("?", email);
                                        cmd.Parameters.AddWithValue("?", nome);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    CarregarDados();
                    MessageBox.Show("Importação concluída!");
                }
                catch (Exception ex) { MessageBox.Show("Erro ao ler Excel: " + ex.Message); }
            }
        }

        private void dataalunos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha válida (não no cabeçalho)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataalunos.Rows[e.RowIndex];

                // --- LÓGICA PARA COPIAR O NOME ---
                // Verifica se a coluna clicada é a de "Nome"
                if (dataalunos.Columns[e.ColumnIndex].Name == "Nome")
                {
                    string nomeParaCopiar = row.Cells["Nome"].Value?.ToString();
                    if (!string.IsNullOrEmpty(nomeParaCopiar))
                    {
                        Clipboard.SetText(nomeParaCopiar);

                        // Opcional: Um feedback visual rápido na barra de status ou ToolTip
                        // MessageBox.Show("Nome copiado!"); // Descomente se quiser um aviso (meio invasivo)
                    }
                }
                // --------------------------------

                // Restante do seu código original para preencher os campos
                idSelecionado = Convert.ToInt32(row.Cells["Código"].Value);
                txtNome.Text = row.Cells["Nome"].Value?.ToString();

                string email = row.Cells["Email"].Value?.ToString();
                txtEmail.Text = email == "Não cadastrado" ? "" : email;

                string tAluno = row.Cells["TelAluno"].Value?.ToString();
                txtTelAluno.Text = tAluno == "Sem número" ? "" : tAluno;

                string tResp = row.Cells["TelResponsavel"].Value?.ToString();
                txtTelResp.Text = tResp == "Sem número" ? "" : tResp;

                Btnadicionar.Text = "Atualizar";
            }
        }

        private void LimparCampos()
        {
            idSelecionado = 0;
            txtNome.Clear();
            txtEmail.Clear();
            txtTelAluno.Clear();
            txtTelResp.Clear();
            Btnadicionar.Text = "Adicionar";
            txtNome.Focus();
        }
        private void ConfigurarDicas()
        {
            // Cria o componente ToolTip
            ToolTip toolTip = new ToolTip();

            // Configurações visuais (opcional)
            toolTip.AutoPopDelay = 10000; // Tempo que a dica fica visível (10 segundos)
            toolTip.InitialDelay = 500;   // Atraso para aparecer (0,5 segundo)
            toolTip.ReshowDelay = 500;    // Atraso ao passar de um controle para outro
            toolTip.ShowAlways = true;    // Mostrar mesmo se a janela não estiver em foco
            toolTip.IsBalloon = true;     // Estilo balão (fica mais moderno)
            toolTip.ToolTipTitle = "Instrução:"; // Título do balão
            toolTip.ToolTipIcon = ToolTipIcon.Info;

            toolTip.SetToolTip(Btnadicionar, "Atualizar Contato / Adicionar Contato no BD.");
            toolTip.SetToolTip(Btnapagar, "Excluir contato do DB.");
            toolTip.SetToolTip(BtnImportar, "Importar Planilha Excel com : Nome e E-mail apenas.");

        }
        private void dataalunos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataalunos.Columns[e.ColumnIndex].Name == "Nome")
            {
                dataalunos.Cursor = Cursors.Hand;
            }
            else
            {
                dataalunos.Cursor = Cursors.Default;
            }
        }
    }
}