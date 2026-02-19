using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class FrmConfigEmail : Form
    {
        // Configurações de conexão e caminhos
        private string strConexao;
        private string caminhoTxt;

        public FrmConfigEmail()
        {
            InitializeComponent();

            // 1. Busca o caminho do banco das configurações globais do sistema
            string pathBanco = Properties.Settings.Default.CaminhoBanco;
            this.strConexao = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={pathBanco};";

            // 2. Define o caminho na pasta AppData/Roaming/ChatBot para o arquivo de texto
            string pastaAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChatBot");

            if (!Directory.Exists(pastaAppData))
            {
                Directory.CreateDirectory(pastaAppData);
            }

            caminhoTxt = Path.Combine(pastaAppData, "config_envio.txt");

            // Eventos
            this.Load += FrmConfig_Load;
            this.cmbEmails.SelectedIndexChanged += cmbEmails_SelectedIndexChanged;

            ConfigurarDicas();

        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            // Verifica se o banco está configurado antes de tentar ler
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.CaminhoBanco))
            {
                MessageBox.Show("Caminho do banco não configurado nas propriedades do sistema.", "Aviso");
            }
            else
            {
                AtualizarListaDoBanco();
            }

            CarregarConfiguracaoLocal();
        }

        // BUSCA OS EMAILS DO ACCESS PARA A COMBOBOX
        private void AtualizarListaDoBanco()
        {
            try
            {
                cmbEmails.Items.Clear();
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();
                    string sql = "SELECT EmailSmtp FROM Configuracoes";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbEmails.Items.Add(reader["EmailSmtp"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar lista de e-mails do banco: " + ex.Message);
            }
        }

        // LÊ O ARQUIVO TXT LOCAL (4 LINHAS)
        private void CarregarConfiguracaoLocal()
        {
            try
            {
                if (File.Exists(caminhoTxt))
                {
                    string[] dados = File.ReadAllLines(caminhoTxt);

                    // Agora esperamos 4 linhas: Email, Senha, Nome, Assunto
                    if (dados.Length >= 4)
                    {
                        txtEmail.Text = dados[0];
                        txtSenha.Text = dados[1];
                        txtNomeUnidade.Text = dados[2];
                        txtAssunto.Text = dados[3];
                    }
                    else if (dados.Length == 3) // Caso o arquivo ainda seja da versão antiga
                    {
                        txtEmail.Text = dados[0];
                        txtSenha.Text = dados[1];
                        txtNomeUnidade.Text = dados[2];
                        txtAssunto.Text = "Mensagem importante do Suporte Microlins!";
                    }
                }
            }
            catch { /* Ignora erros de leitura inicial */ }
        }

        // AO SELECIONAR NA COMBOBOX, BUSCA OS DADOS COMPLETOS NO ACCESS
        private void cmbEmails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();
                    // SQL ajustado para o nome da coluna 'AssuntoPadrao' da sua imagem
                    string sql = "SELECT EmailSmtp, SenhaSmtp, NomeExibicao, AssuntoPadrao FROM Configuracoes WHERE EmailSmtp = ?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", cmbEmails.Text);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtEmail.Text = reader["EmailSmtp"].ToString();
                                txtSenha.Text = reader["SenhaSmtp"].ToString();
                                txtNomeUnidade.Text = reader["NomeExibicao"].ToString();

                                // Verifica se o assunto não está nulo no banco
                                txtAssunto.Text = reader["AssuntoPadrao"] != DBNull.Value
                                                  ? reader["AssuntoPadrao"].ToString()
                                                  : "Mensagem importante do Suporte Microlins!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar dados do e-mail selecionado: " + ex.Message);
            }
        }

        // SALVA AS CONFIGURAÇÕES NO TXT LOCAL (PARA USO DO ROBÔ)
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string senha = txtSenha.Text.Trim();
                string nome = txtNomeUnidade.Text.Trim();
                string assunto = txtAssunto.Text.Trim();

                // 1. Validação básica
                if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                {
                    MessageBox.Show("Por favor, insira um e-mail válido.");
                    return;
                }

                // 2. SALVAR NO BANCO DE DADOS (ACCESS)
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();

                    // Verifica se o e-mail já existe
                    string sqlCheck = "SELECT COUNT(*) FROM Configuracoes WHERE EmailSmtp = ?";
                    bool existe;
                    using (OleDbCommand cmdCheck = new OleDbCommand(sqlCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("?", email);
                        existe = (int)cmdCheck.ExecuteScalar() > 0;
                    }

                    if (existe)
                    {
                        // UPDATE: Se já existe, atualiza os dados
                        string sqlUpdate = "UPDATE Configuracoes SET SenhaSmtp = ?, NomeExibicao = ?, AssuntoPadrao = ? WHERE EmailSmtp = ?";
                        using (OleDbCommand cmdUpdate = new OleDbCommand(sqlUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("?", senha);
                            cmdUpdate.Parameters.AddWithValue("?", nome);
                            cmdUpdate.Parameters.AddWithValue("?", assunto);
                            cmdUpdate.Parameters.AddWithValue("?", email);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // INSERT: Se não existe, cria um novo
                        string sqlInsert = "INSERT INTO Configuracoes (EmailSmtp, SenhaSmtp, NomeExibicao, AssuntoPadrao) VALUES (?, ?, ?, ?)";
                        using (OleDbCommand cmdInsert = new OleDbCommand(sqlInsert, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("?", email);
                            cmdInsert.Parameters.AddWithValue("?", senha);
                            cmdInsert.Parameters.AddWithValue("?", nome);
                            cmdInsert.Parameters.AddWithValue("?", assunto);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }

                // 3. SALVAR NO TXT LOCAL (PARA O ROBÔ)
                string[] config = { email, senha, nome, assunto };
                File.WriteAllLines(caminhoTxt, config);

                MessageBox.Show("Configurações salvas no banco e no arquivo local com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar configurações: " + ex.Message);
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

            // --- DEFINIR AS MENSAGENS PARA CADA LABEL OU TEXTBOX ---

            // Dica para o E-mail
            toolTip.SetToolTip(lblEmail, "Insira o e-mail do Gmail que fará os envios.");

            // Dica para a Senha (A MAIS IMPORTANTE)
            toolTip.SetToolTip(lblSenha, "NÃO use sua senha comum!\n" +
                                         "1. Ative a Verificação em 2 Etapas no Google.\n" +
                                         "2. Vá em 'Segurança' > 'Senhas de App'.\n" +
                                         "3. Gere uma senha para 'E-mail' e cole aqui.");

            // Dica para o Nome da Unidade
            toolTip.SetToolTip(lblNomeUnidade, "Nome que aparecerá no E-mail enviado.");

            // Dica para o Assunto
            toolTip.SetToolTip(lblAssunto, "O título que aparecerá na caixa de entrada do e-mail.");
        }
    }
}