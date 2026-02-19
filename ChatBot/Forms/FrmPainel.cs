using ChatBot.Forms;
using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using QRCoder;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeleniumKeys = OpenQA.Selenium.Keys;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using AutoUpdaterDotNET;

namespace ChatBot
{
    public partial class FrmPainel : Form
    {
        private IWebDriver driver;
        private bool conectado = false;
        private QRCodeGenerator qrGenerator = new QRCodeGenerator();
        private List<dynamic> filaDeEnvio = new List<dynamic>();
        private FrmLogin janelaLogin;
        private Random rnd = new Random();

        // CORREÇÃO: A string de conexão agora busca o caminho salvo nas configurações
        private string strConexao
        {
            get
            {
                // Busca o caminho que você salvou no FrmConfigBanco
                string caminhoBanco = Properties.Settings.Default.CaminhoBanco;

                // Monta a string de conexão dinamicamente
                return $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={caminhoBanco};";
            }
        }

        public FrmPainel()
        {
            InitializeComponent();

            // Ela vai checar se tem versão nova no seu GitHub assim que o app abrir
            AutoUpdater.Start("https://raw.githubusercontent.com/GuuhFioriw/ChatBot/main/update.xml");
            // -------------------------------

            this.Load += new System.EventHandler(this.Painel_Load);
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisarPlanilha_Click);
            this.btnEnviarMassa.Click += new System.EventHandler(this.btnEnviarMassa_Click);
            this.btnDesconectar.Click += new System.EventHandler(this.btnDesconectar_Click);
            this.FormClosing += new FormClosingEventHandler(this.FrmPainel_FormClosing);

            ConfigurarDicas();
        }

        private async void Painel_Load(object sender, EventArgs e)
        {
            txtHistorico.ReadOnly = true;
            btnEnviarMassa.Enabled = false;
            btnDesconectar.Enabled = false;
            EsconderProgresso();
            AtualizarStatusVisual("Iniciando...", Color.Gray);

            VerificarOperador();

            // --- A MÁGICA PARA A VERSÃO AUTOMÁTICA ---
            // Isso lê o número da versão que está nas propriedades do seu projeto
            string versaoReal = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            AtualizarHistorico("------------------------------------------");
            AtualizarHistorico("🚀 BEM-VINDO AO CHATBOT!");
            AtualizarHistorico("👨‍💻 Desenvolvido por: Gustavo Rodrigues Fiori");
            AtualizarHistorico($"⚙️ Versão ChatBot : {versaoReal}"); // Agora usa a variável
            AtualizarHistorico("------------------------------------------");

            _ = Task.Run(() => IniciarZapAutomatico());
        }

        private void VerificarOperador()
        {
            string operadorAtual = Properties.Settings.Default.OperadorAtual ?? "";

            // Enquanto não tiver operador cadastrado, abre a tela
            while (string.IsNullOrEmpty(operadorAtual))
            {
                using (var frm = new FrmIdentificacao(operadorAtual))
                {
                    frm.ShowDialog();
                }

                // Atualiza após o usuário salvar
                operadorAtual = Properties.Settings.Default.OperadorAtual ?? "";

                if (string.IsNullOrEmpty(operadorAtual))
                {
                    MessageBox.Show("Você precisa se identificar para usar o ChatBot!", "Atenção");
                }
            }

            AtualizarHistorico($"👤 OPERADOR ATUAL: {operadorAtual}");
        }

        private void EsconderProgresso()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { EsconderProgresso(); }); return; }
            if (pgbProgresso != null) pgbProgresso.Visible = false;
            if (lblPorcentagem != null) lblPorcentagem.Visible = false;
        }

        private void MostrarProgresso()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { MostrarProgresso(); }); return; }
            if (pgbProgresso != null) pgbProgresso.Visible = true;
            if (lblPorcentagem != null) lblPorcentagem.Visible = true;
        }

        private void AtualizarStatusVisual(string texto, Color cor)
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { AtualizarStatusVisual(texto, cor); }); return; }
            lblStatus.Text = texto.ToUpper();
            lblStatus.ForeColor = cor;
            if (pnlStatusLed != null)
            {
                pnlStatusLed.BackColor = cor;
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, pnlStatusLed.Width - 1, pnlStatusLed.Height - 1);
                pnlStatusLed.Region = new Region(gp);
            }
        }

        private async Task IniciarZapAutomatico(int tentativa = 1)
        {
            try
            {
                // 1. LIMPEZA DE SEGURANÇA
                try
                {
                    if (tentativa > 1)
                    {
                        foreach (var p in Process.GetProcessesByName("msedge")) p.Kill();
                    }
                    foreach (var p in Process.GetProcessesByName("msedgedriver")) p.Kill();
                }
                catch { }

                // --- COMPATIBILIDADE AUTOMÁTICA (A MÁGICA ACONTECE AQUI) ---
                // Este comando verifica a versão do seu Edge e baixa o Driver compatível automaticamente.
                new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);

                var options = new EdgeOptions();

                // --- CONFIGURAÇÃO DE PERSISTÊNCIA ---
                string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string pastaSessao = Path.Combine(caminhoAppData, "ChatBotWhatsApp", "DadosDoPerfil");

                if (!Directory.Exists(pastaSessao))
                    Directory.CreateDirectory(pastaSessao);

                options.AddArgument($"--user-data-dir={pastaSessao}");
                options.AddArgument("--profile-directory=Default");

                // --- MODO E ESTABILIDADE ---
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");

                // User-agent atualizado para evitar bloqueios do WhatsApp
                options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36");

                var service = EdgeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                // 2. INICIALIZAÇÃO DO DRIVER
                driver = new EdgeDriver(service, options);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

                driver.Navigate().GoToUrl("https://web.whatsapp.com");

                // 3. MONITORAMENTO DE LOGIN / QR CODE
                _ = Task.Run(async () =>
                {
                    while (driver != null && !conectado)
                    {
                        try
                        {
                            // Verifica se o painel lateral ou campo de busca apareceu (Logado)
                            var logado = driver.FindElements(By.XPath("//div[@id='pane-side'] | //div[@contenteditable='true'][@data-tab='3']")).Count > 0;
                            if (logado)
                            {
                                conectado = true;
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (janelaLogin != null) { janelaLogin.FecharLogin(); janelaLogin = null; }
                                    btnDesconectar.Enabled = true;
                                    btnEnviarMassa.Enabled = filaDeEnvio.Count > 0;
                                    AtualizarStatusVisual("ChatBot Conectado", Color.ForestGreen);
                                    AtualizarHistorico("✅ SISTEMA: ChatBot Conectado!");
                                });
                                break;
                            }

                            // Se não logou, procura o QR Code
                            var qrElements = driver.FindElements(By.XPath("//div[@data-ref]"));
                            if (qrElements.Count > 0)
                            {
                                string tokenQR = qrElements[0].GetAttribute("data-ref");
                                Image imgQR = GerarImagemQR(tokenQR);
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (janelaLogin == null || janelaLogin.IsDisposed)
                                    {
                                        janelaLogin = new FrmLogin();
                                        janelaLogin.Show();
                                    }
                                    janelaLogin.AtualizarQR(imgQR);
                                    AtualizarStatusVisual("Aguardando QR", Color.OrangeRed);
                                });
                            }
                        }
                        catch { }
                        await Task.Delay(2000);
                    }
                });
            }
            catch (Exception ex)
            {
                // SE DER O ERRO DE CRASH (SessionNotCreated / DevToolsActivePort)
                if (tentativa < 2)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AtualizarHistorico("⚠️ Falha no navegador detectada. Reiniciando processos...");
                    });

                    await Task.Delay(2000);
                    await IniciarZapAutomatico(2);
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AtualizarHistorico("❌ ERRO PERSISTENTE: " + ex.Message);
                    });
                }
            }
        }

        private async void btnAnalisarPlanilha_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel|*.xlsx" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filaDeEnvio.Clear();
                List<string> nomesParaVerificar = new List<string>();

                // 1. Primeiro, apenas lemos os nomes da planilha
                using (var workbook = new XLWorkbook(ofd.FileName))
                {
                    var worksheet = workbook.Worksheet(1);
                    var linhas = worksheet.RangeUsed().RowsUsed();
                    foreach (var linha in linhas)
                    {
                        string nome = linha.Cell(1).GetString().Trim();
                        if (!string.IsNullOrEmpty(nome)) nomesParaVerificar.Add(nome);
                    }
                }

                List<string> nomesNaoEncontrados = new List<string>();

                // 2. Buscamos no banco de dados ATUALIZADO
                await Task.Run(() =>
                {
                    foreach (var nome in nomesParaVerificar)
                    {
                        var dados = BuscarDadosNoAccess(nome);
                        if (dados != null)
                        {
                            filaDeEnvio.Add(new
                            {
                                Nome = nome,
                                TelAluno = (string)dados.TelAluno,
                                TelResp = (string)dados.TelResp,
                                Email = (string)dados.Email
                            });
                        }
                        else
                        {
                            nomesNaoEncontrados.Add(nome);
                        }
                    }
                });

                // 3. Se não achou alguém (ou se o cadastro mudou), abre a tela de cadastro
                if (nomesNaoEncontrados.Count > 0)
                {
                    using (var frm = new FrmCadastro(nomesNaoEncontrados, strConexao))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var alunoCadastrado in frm.AlunosConfirmados)
                            {
                                // Evita duplicados caso o usuário tenha cadastrado manualmente agora
                                if (!filaDeEnvio.Any(f => f.Nome == alunoCadastrado.Nome))
                                    filaDeEnvio.Add(alunoCadastrado);
                            }
                        }
                    }
                }

                AtualizarInterface();
                AtualizarHistorico($"✅ Fila atualizada: {filaDeEnvio.Count} alunos prontos.");
            }
        }

        private void btnEnviarMassa_Click(object sender, EventArgs e)
        {
            _ = EnviarMassaAsync();
        }

        private async Task EnviarMassaAsync()
        {
            if (!conectado || filaDeEnvio.Count == 0) return;

            string msgBase = txtMensagem.Text;
            btnAnalisar.Enabled = btnEnviarMassa.Enabled = false;

            int totalProcessos = filaDeEnvio.Count;
            int processados = 0;

            this.Invoke((MethodInvoker)delegate
            {
                MostrarProgresso();
                pgbProgresso.Maximum = totalProcessos;
                pgbProgresso.Value = 0;
                lblPorcentagem.Text = $"0 / {totalProcessos}";
            });

            await Task.Run(async () =>
            {
                foreach (var aluno in filaDeEnvio)
                {
                    if (!conectado)
                    {
                        this.Invoke((MethodInvoker)delegate { AtualizarHistorico("⚠️ Conexão perdida. Tentando reconectar..."); });
                        _ = IniciarZapAutomatico();
                        await Task.Delay(5000);
                        if (!conectado) continue;
                    }

                    string msgFinal = msgBase.Replace("{nome}", aluno.Nome);
                    string statusAcumulado = "";
                    string errosAcumulados = "";

                    // 1. Envio para o Aluno (WhatsApp)
                    if (!string.IsNullOrEmpty(aluno.TelAluno) && aluno.TelAluno != "Sem número")
                    {
                        try
                        {
                            await EnviarMensagemSuperTurbo(aluno.TelAluno, msgFinal);
                            statusAcumulado += "[Zap Aluno: OK] ";

                            // --- ADICIONADO: Feedback de sucesso no histórico ---
                            this.Invoke((MethodInvoker)delegate {
                                AtualizarHistorico($"✅ Zap enviado para Aluno: {aluno.Nome} ({aluno.TelAluno})");
                            });
                        }
                        catch (Exception ex)
                        {
                            string motivo = ex.Message.Contains("Timed out")
                                ? "Tempo esgotado (O número pode não ter WhatsApp / Incorreto.)"
                                : ex.Message;

                            statusAcumulado += "[Zap Aluno: Falhou] ";
                            errosAcumulados += $"Erro Aluno: {motivo}; ";

                            this.Invoke((MethodInvoker)delegate {
                                AtualizarHistorico($"❌ Erro Aluno {aluno.Nome}: {motivo}");
                            });
                        }
                        await Task.Delay(rnd.Next(1500, 3000)); // Delay humano
                    }

                    // 2. Envio para o Responsável (WhatsApp)
                    if (!string.IsNullOrEmpty(aluno.TelResp) && aluno.TelResp != "Sem número")
                    {
                        try
                        {
                            await EnviarMensagemSuperTurbo(aluno.TelResp, msgFinal);
                            statusAcumulado += "[Zap Resp: OK] ";

                            // --- ADICIONADO: Feedback de sucesso no histórico ---
                            this.Invoke((MethodInvoker)delegate {
                                AtualizarHistorico($"✅ Zap enviado para Resp. de: {aluno.Nome} ({aluno.TelResp})");
                            });
                        }
                        catch (Exception ex)
                        {
                            string motivo = ex.Message.Contains("Timed out")
                                ? "Tempo esgotado (O número pode não ter WhatsApp / Incorreto.)"
                                : ex.Message;

                            statusAcumulado += "[Zap Resp: Falhou] ";
                            errosAcumulados += $"Erro Resp: {motivo}; ";

                            this.Invoke((MethodInvoker)delegate {
                                AtualizarHistorico($"❌ Erro Resp {aluno.Nome}: {motivo}");
                            });
                        }
                        await Task.Delay(rnd.Next(1500, 3000));
                    }

                    // 3. Envio de E-mail
                    if (!string.IsNullOrEmpty(aluno.Email) && aluno.Email != "Não cadastrado" && aluno.Email.Contains("@"))
                    {
                        try
                        {
                            EnviarEmail(aluno.Email, "", msgFinal);
                            statusAcumulado += "[E-mail: OK] ";
                            // O método EnviarEmail já possui seu próprio AtualizarHistorico interno
                        }
                        catch (Exception ex)
                        {
                            statusAcumulado += "[E-mail: Falhou] ";
                            errosAcumulados += $"Erro E-mail: {ex.Message}; ";
                            this.Invoke((MethodInvoker)delegate { AtualizarHistorico($"❌ Erro e-mail {aluno.Nome}: {ex.Message}"); });
                        }
                    }

                    // --- REGISTRO ÚNICO NO BANCO DE DADOS (FINAL DO ALUNO) ---
                    if (!string.IsNullOrEmpty(statusAcumulado))
                    {
                        string erroFinal = string.IsNullOrWhiteSpace(errosAcumulados) ? "Sucesso" : errosAcumulados.Trim();

                        SalvarLogNoAccess(
                            aluno.Nome,
                            aluno.TelAluno,
                            aluno.TelResp,
                            aluno.Email,
                            msgFinal,
                            statusAcumulado.Trim(),
                            erroFinal
                        );
                    }

                    processados++;
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pgbProgresso.Value = processados;
                        lblPorcentagem.Text = $"{processados} / {totalProcessos}";
                    }));
                }

                this.Invoke((MethodInvoker)(() =>
                {
                    btnAnalisar.Enabled = btnEnviarMassa.Enabled = true;
                    filaDeEnvio.Clear();
                    AtualizarInterface();
                    EsconderProgresso();
                    AtualizarHistorico("------------------------------------------");
                    AtualizarHistorico("🚀 DISPARO CONCLUÍDO E REGISTRADO!");
                    AtualizarHistorico("------------------------------------------");
                    MessageBox.Show("Disparo concluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });
        }

        private void EnviarEmail(string destino, string assuntoPadrao, string corpo)
        {
            try
            {
                string pastaAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChatBot");
                string caminhoTxt = Path.Combine(pastaAppData, "config_envio.txt");

                string assuntoFinal = assuntoPadrao; // Caso o arquivo não exista, usa o que foi passado

                if (File.Exists(caminhoTxt))
                {
                    string[] config = File.ReadAllLines(caminhoTxt);
                    // Se o arquivo tiver 4 linhas, a quarta linha (índice 3) é o nosso assunto customizado
                    if (config.Length >= 4 && !string.IsNullOrWhiteSpace(config[3]))
                    {
                        assuntoFinal = config[3];
                    }

                    // Verificação de segurança para os outros dados
                    if (config.Length < 3) return;

                    string emailRemetente = config[0];
                    string senhaApp = config[1];
                    string nomeExibicao = config[2];

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(emailRemetente, senhaApp),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(emailRemetente, nomeExibicao),
                        Subject = assuntoFinal, // <--- AQUI USAMOS O ASSUNTO DINÂMICO
                        Body = corpo,
                        IsBodyHtml = false,
                    };

                    mailMessage.To.Add(destino);
                    smtpClient.Send(mailMessage);
                    AtualizarHistorico($"📧 E-mail enviado: {destino}");
                }
            }
            catch (Exception ex)
            {
                AtualizarHistorico($"❌ Erro E-mail {destino}: {ex.Message}");
            }
        }

        private async Task EnviarMensagemSuperTurbo(string numero, string texto)
        {
            if (driver == null || !conectado) throw new Exception("WhatsApp não conectado.");

            string num = new string(numero.Where(char.IsDigit).ToArray());
            if (num.Length == 10 || num.Length == 11) num = "55" + num;

            driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={num}");

            // Diminuí o tempo de espera total para 45s e adicionei verificação de erro
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));

            IWebElement campo = wait.Until(d =>
            {
                try
                {
                    // 1. Verifica se apareceu a caixa de alerta de número inválido
                    // O XPath busca por textos comuns em avisos de erro do WhatsApp
                    var erroJanela = d.FindElements(By.XPath("//div[contains(text(), 'inválido') or contains(text(), 'invalid') or contains(text(), 'URL')]"));
                    if (erroJanela.Count > 0) throw new Exception("O número digitado é inválido.");

                    // 2. Tenta localizar o campo de texto (footer)
                    var el = d.FindElements(By.XPath("//footer//div[@contenteditable='true']")).FirstOrDefault();
                    if (el != null && el.Displayed && el.Enabled) return el;
                }
                catch (Exception ex) when (!ex.Message.Contains("O número digitado é inválido"))
                {
                    // Ignora erros genéricos de busca, mas deixa passar o nosso erro customizado
                }
                return null;
            });

            await Task.Delay(1000);
            campo.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string textoFormatado = texto.Replace("\r\n", "\n").Replace("\r", "\n");

            js.ExecuteScript(@"
        var el = arguments[0];
        var txt = arguments[1];
        el.focus();
        const dataTransfer = new DataTransfer();
        dataTransfer.setData('text/plain', txt);
        const event = new ClipboardEvent('paste', { clipboardData: dataTransfer, bubbles: true });
        el.dispatchEvent(event);
        el.dispatchEvent(new Event('input', { bubbles: true }));
    ", campo, textoFormatado);

            await Task.Delay(1000);

            IWebElement btnEnviar = wait.Until(d =>
            {
                var btn = d.FindElement(By.XPath("//button[@aria-label='Enviar'] | //span[@data-icon='send']/parent::button"));
                return (btn.Displayed && btn.Enabled) ? btn : null;
            });

            btnEnviar.Click();
            await Task.Delay(2000);
        }

        private dynamic BuscarDadosNoAccess(string nome)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();
                    string sql = "SELECT TelAluno, TelResponsavel, Email FROM Alunos WHERE TRIM(Nome) = ?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", nome.Trim());
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) return new
                            {
                                TelAluno = reader[0]?.ToString() ?? "",
                                TelResp = reader[1]?.ToString() ?? "",
                                Email = reader[2]?.ToString() ?? "Não cadastrado"
                            };
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private void AtualizarInterface()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { AtualizarInterface(); }); return; }
            lblContador.Text = $"Alunos na fila: {filaDeEnvio.Count}";
            btnEnviarMassa.Enabled = (filaDeEnvio.Count > 0 && conectado);
        }

        private void AtualizarHistorico(string texto)
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { AtualizarHistorico(texto); }); return; }
            txtHistorico.AppendText(texto + Environment.NewLine);
            txtHistorico.SelectionStart = txtHistorico.Text.Length;
            txtHistorico.ScrollToCaret();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Para o monitoramento
                conectado = false;

                // 2. Fecha o driver e o navegador com segurança
                if (driver != null)
                {
                    try { driver.Quit(); } catch { }
                    driver = null;
                }

                // 3. LIMPEZA DA SESSÃO (Isso faz o QR Code voltar)
                string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string pastaSessao = Path.Combine(caminhoAppData, "ChatBotWhatsApp", "DadosDoPerfil");

                if (Directory.Exists(pastaSessao))
                {
                    // Espera um pouco para o Windows soltar os arquivos após o Quit()
                    System.Threading.Thread.Sleep(1000);
                    try
                    {
                        Directory.Delete(pastaSessao, true);
                        AtualizarHistorico("🧹 SISTEMA: Dados de sessão limpos.");
                    }
                    catch { AtualizarHistorico("⚠️ Aviso: Não foi possível limpar todos os arquivos da sessão."); }
                }

                // 4. Feedback visual
                AtualizarStatusVisual("Desconectado", Color.Red);
                AtualizarHistorico("⚠️ SISTEMA: Sessão encerrada.");

                // 5. CHAMA O QR CODE DE NOVO
                // Agora, como a pasta foi apagada, ele vai abrir pedindo o QR Code
                _ = Task.Run(() => IniciarZapAutomatico());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao desconectar e limpar sessão: " + ex.Message);
            }
        }

        private Image GerarImagemQR(string texto)
        {
            using (var qrData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrData)) { return qrCode.GetGraphic(20); }
        }

        private void btnAlunos_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmGerenciarAlunos())
            {
                frm.ShowDialog();
            }
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPainelConfigs())
            {
                // Se a tela de configs retornar OK (clique no trocar operador) 
                // ou Retry (clique no resetar navegador)
                var resultado = frm.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    // Executa a troca de operador aqui no contexto do Painel
                    VerificarOperador();

                    AtualizarHistorico("------------------------------------------");
                    AtualizarHistorico($"🔄 TROCA DE OPERADOR: Ativo agora: {Properties.Settings.Default.OperadorAtual}");
                    AtualizarHistorico("------------------------------------------");
                }
                else if (resultado == DialogResult.Retry)
                {
                    // Se você usou o botão de Resetar Navegador
                    _ = Task.Run(() => IniciarZapAutomatico());
                }
            }
        }

        private void FrmPainel_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                conectado = false;
                if (driver != null)
                {
                    driver.Quit();
                    driver = null;
                }
            }
            catch { }
        }

        private void SalvarLogNoAccess(string nome, string telAlu, string telRes, string email, string msg, string status, string erro = "")
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();

                    // 1. NomeAluno, 2. TellAluno, 3. TellResp, 4. EmailDestino, 5. DataHora, 6. [Status], 7. Operador, 8. Mensagem, 9. ErroMensagem
                    string sql = "INSERT INTO HistoricoEnvio (NomeAluno, TellAluno, TellResp, EmailDestino, DataHora, [Status], Operador, Mensagem, ErroMensagem) " +
                                 "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // A ordem dos parâmetros deve ser idêntica à ordem do SQL acima
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = nome ?? "";          // 1
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = telAlu ?? "";        // 2
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = telRes ?? "";        // 3
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = email ?? "";         // 4

                        // DATAHORA: Usando DateTime.Now para registrar o momento exato
                        cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;           // 5

                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = status ?? "";        // 6

                        // --- CORREÇÃO DO OPERADOR ---
                        // Agora puxa dinamicamente do seu Settings.settings
                        string operadorLogado = Properties.Settings.Default.OperadorAtual;
                        if (string.IsNullOrEmpty(operadorLogado)) operadorLogado = "Não Identificado";

                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = operadorLogado;      // 7 
                                                                                                // ----------------------------

                        cmd.Parameters.Add("?", OleDbType.LongVarChar).Value = msg ?? "";       // 8 (Texto Longo no Access)
                        cmd.Parameters.Add("?", OleDbType.LongVarChar).Value = erro ?? "";      // 9 (Texto Longo no Access)

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Usa BeginInvoke para não travar a interface em caso de erro no banco
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AtualizarHistorico("⚠️ Erro Crítico no Banco: " + ex.Message);
                });
            }
        }

        private void LimparProcessosEdge()
        {
            try
            {
                // Encerra qualquer resquício do Edge ou do Driver que ficou travado
                foreach (var process in Process.GetProcessesByName("msedge")) process.Kill();
                foreach (var process in Process.GetProcessesByName("msedgedriver")) process.Kill();

                AtualizarHistorico("♻️ Ambiente resetado. Tentando abrir navegador...");
            }
            catch { /* Silencioso se não houver processos */ }
        }

        private void BtnRegistros_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmRegistroMsg())
            {
                frm.ShowDialog();
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
            
            toolTip.SetToolTip(BtnRegistros, "Registro de todas Mensagens enviadas pelo ChatBot.");

            toolTip.SetToolTip(btnAlunos, "Todos Contatos Salvos do ChatBot.");

            toolTip.SetToolTip(BtnConfig, "Configurações ChatBot.");

            toolTip.SetToolTip(btnDesconectar, "Desconectar ChatBot do WhatsApp.");

            toolTip.SetToolTip(btnAnalisar, "Anexe Arquivo Excel apenas com os nomes.");

        }
    }
}
