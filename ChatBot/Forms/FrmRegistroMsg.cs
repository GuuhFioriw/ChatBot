using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ChatBot.Forms
{
    public partial class FrmRegistroMsg : Form
    {
        private readonly string strConexao =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Properties.Settings.Default.CaminhoBanco};Persist Security Info=False;";

        public FrmRegistroMsg()
        {
            InitializeComponent();
            ConfigurarGrade();
            ConfigurarDicas();

            dtpDataInicio.ValueChanged += (s, e) => CarregarDados();
            dtpDataFim.ValueChanged += (s, e) => CarregarDados(); // Novo evento
            cboStatus.SelectedIndexChanged += cboStatus_SelectedIndexChanged;
        }

        private void FrmRegistroMsg_Load(object sender, EventArgs e)
        {
            txtPesquisaAluno.Text = "Digite o nome do aluno...";
            txtPesquisaAluno.ForeColor = System.Drawing.Color.Gray;

            // CONFIGURAÇÃO DE DATAS:
            // Define a data de início para o dia 1 do mês atual, ano atual
            dtpDataInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Define a data de fim para o momento atual (hoje)
            dtpDataFim.Value = DateTime.Now;

            cboStatus.SelectedIndex = 0; // Garante que começa em "Todos"

            CarregarOperadores();
            CarregarDados();
        }

        private void ConfigurarGrade()
        {
            DgvMsg.ReadOnly = true;
            DgvMsg.AllowUserToAddRows = false;
            DgvMsg.RowHeadersVisible = false;
            DgvMsg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvMsg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Bloqueio de redimensionamento
            DgvMsg.AllowUserToResizeColumns = false;
            DgvMsg.AllowUserToResizeRows = false;
            DgvMsg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Visual Branco e Centralização Total
            DgvMsg.BackgroundColor = System.Drawing.Color.White;
            DgvMsg.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            DgvMsg.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);

            DgvMsg.EnableHeadersVisualStyles = false;
            DgvMsg.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 0, 128);
            DgvMsg.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DgvMsg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvMsg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvMsg.ColumnHeadersHeight = 35;
        }

        private void CarregarOperadores()
        {
            try
            {
                using (var conn = new OleDbConnection(strConexao))
                {
                    string sql = "SELECT DISTINCT Operador FROM HistoricoEnvio WHERE Operador IS NOT NULL ORDER BY Operador";
                    var da = new OleDbDataAdapter(sql, conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    CbmOperador.Items.Clear();
                    CbmOperador.Items.Add("Todos os Operadores");

                    foreach (DataRow row in dt.Rows)
                        CbmOperador.Items.Add(row["Operador"].ToString());

                    CbmOperador.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void CarregarDados()
        {
            try
            {
                string filtroNome = (txtPesquisaAluno.Text == "Digite o nome do aluno...") ? "" : txtPesquisaAluno.Text.Trim();
                string filtroOp = CbmOperador.SelectedItem?.ToString() ?? "Todos os Operadores";
                string filtroStatus = cboStatus.SelectedItem?.ToString() ?? "Todos";

                // Pega apenas a DATA (sem hora) do início e o final do dia do fim
                DateTime dataInicio = dtpDataInicio.Value.Date;
                DateTime dataFim = dtpDataFim.Value.Date.AddDays(1).AddSeconds(-1); // Vai até 23:59:59 do dia final

                using (var conn = new OleDbConnection(strConexao))
                {
                    string sql = @"
                SELECT 
                    NomeAluno, DataHora, Mensagem, TellAluno, 
                    TellResp, EmailDestino, Operador, ErroMensagem AS Status 
                FROM HistoricoEnvio WHERE 1=1";

                    var cmd = new OleDbCommand();

                    // 1. Filtro de Nome
                    if (!string.IsNullOrWhiteSpace(filtroNome))
                    {
                        sql += " AND NomeAluno LIKE ?";
                        cmd.Parameters.AddWithValue("?", "%" + filtroNome + "%");
                    }

                    // 2. Filtro de Operador
                    if (filtroOp != "Todos os Operadores")
                    {
                        sql += " AND Operador = ?";
                        cmd.Parameters.AddWithValue("?", filtroOp);
                    }

                    // 3. Filtro de Intervalo de Datas (Mais robusto que DateValue)
                    sql += " AND DataHora BETWEEN ? AND ?";
                    cmd.Parameters.AddWithValue("?", dataInicio);
                    cmd.Parameters.AddWithValue("?", dataFim);

                    // 4. Filtro de Status
                    if (filtroStatus == "Sucesso")
                    {
                        sql += " AND ErroMensagem = 'Sucesso'";
                    }
                    else if (filtroStatus == "Erro")
                    {
                        sql += " AND ErroMensagem <> 'Sucesso'";
                    }

                    sql += " ORDER BY DataHora DESC";
                    cmd.CommandText = sql;
                    cmd.Connection = conn;

                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        DgvMsg.DataSource = dt;
                        LblQuantidade.Text = $"Total de registros: {dt.Rows.Count}";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar dados: " + ex.Message); }
        }

        private void txtPesquisaAluno_TextChanged(object sender, EventArgs e) => CarregarDados();
        private void CbmOperador_SelectedIndexChanged(object sender, EventArgs e) => CarregarDados();

        private void txtPesquisaAluno_Enter(object sender, EventArgs e)
        {
            if (txtPesquisaAluno.Text == "Digite o nome do aluno...")
            {
                txtPesquisaAluno.Text = "";
                txtPesquisaAluno.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtPesquisaAluno_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPesquisaAluno.Text))
            {
                txtPesquisaAluno.Text = "Digite o nome do aluno...";
                txtPesquisaAluno.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            if (DgvMsg.Rows.Count == 0) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Arquivo PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Relatorio_{DateTime.Now:dd-MM-yyyy_HHmm}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var doc = new Document(PageSize.A4.Rotate(), 10, 10, 15, 15))
                        {
                            PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                            doc.Open();

                            var fonteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                            var fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE);
                            var fonteTexto = FontFactory.GetFont(FontFactory.HELVETICA, 7);

                            var p = new Paragraph("HISTÓRICO DE MENSAGENS", fonteTitulo);
                            p.Alignment = Element.ALIGN_CENTER;
                            doc.Add(p);
                            doc.Add(new Paragraph("\n"));

                            var table = new PdfPTable(8) { WidthPercentage = 100f };
                            table.SetWidths(new float[] { 16f, 12f, 18f, 10f, 10f, 14f, 10f, 10f });

                            string[] headers = { "Nome Aluno", "Data/Hora", "Mensagem", "Tell Aluno", "Tell Resp", "Email", "Operador", "Status" };
                            foreach (var h in headers)
                            {
                                var cell = new PdfPCell(new Phrase(h, fonteCabecalho))
                                {
                                    BackgroundColor = new BaseColor(0, 0, 128),
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    VerticalAlignment = Element.ALIGN_MIDDLE,
                                    Padding = 4
                                };
                                table.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in DgvMsg.Rows)
                            {
                                if (row.IsNewRow) continue;
                                for (int i = 0; i < 8; i++)
                                {
                                    var cell = new PdfPCell(new Phrase(row.Cells[i].Value?.ToString() ?? "", fonteTexto))
                                    {
                                        HorizontalAlignment = Element.ALIGN_CENTER,
                                        VerticalAlignment = Element.ALIGN_MIDDLE,
                                        Padding = 3
                                    };
                                    table.AddCell(cell);
                                }
                            }
                            doc.Add(table);
                            doc.Close();
                        }
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                    catch (Exception ex) { MessageBox.Show("Erro ao gerar PDF: " + ex.Message); }
                }
            }
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

            toolTip.SetToolTip(CbmOperador, "Responsavel de quem enviou a Mensagem.");
            toolTip.SetToolTip(BtnExportar, "Exportar Registros de Mensagens.");

        }

        private void dtpDataInicio_ValueChanged(object sender, EventArgs e)
        {
            CarregarDados();
        }

        // Evento para a data final
        private void dtpDataFim_ValueChanged(object sender, EventArgs e)
        {
            CarregarDados();
        }

        // Evento para o status (Sucesso/Erro)
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarDados();
        }
    }
}
