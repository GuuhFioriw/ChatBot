using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class FrmAutoAtendimento : Form
    {
        // Esta variável vai guardar o navegador que veio do Painel
        private IWebDriver driver;
        private bool assistenteLigado = false;

        // CORREÇÃO AQUI: O construtor agora aceita o argumento 'IWebDriver driverAtivo'
        public FrmAutoAtendimento(IWebDriver driverAtivo)
        {
            InitializeComponent();
            this.driver = driverAtivo; // Recebe o navegador pronto
        }

        // Adicione este construtor à classe FrmAutoAtendimento
        public FrmAutoAtendimento(IWebDriver driver)
        {
            // Inicialize componentes e armazene o driver conforme necessário
            InitializeComponent();
            // Exemplo: this.driver = driver; // se houver um campo para armazenar
        }

        private void btnLigarAssistente_Click(object sender, EventArgs e)
        {
            if (driver == null)
            {
                MessageBox.Show("Erro: Navegador não encontrado.");
                return;
            }

            assistenteLigado = !assistenteLigado;
            btnLigarAssistente.Text = assistenteLigado ? "Desligar Auto-Atendimento" : "Ligar Auto-Atendimento";
            btnLigarAssistente.BackColor = assistenteLigado ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightCoral;

            if (assistenteLigado)
            {
                _ = Task.Run(() => LoopAtendimento());
            }
        }

        private async Task LoopAtendimento()
        {
            while (assistenteLigado)
            {
                try
                {
                    // Busca chats com mensagens não lidas
                    var chatsNaoLidos = driver.FindElements(By.XPath("//span[@aria-label='Não lida'] | //span[contains(@class, '_2H60Q')]"));

                    if (chatsNaoLidos.Count > 0)
                    {
                        chatsNaoLidos[0].Click();
                        await Task.Delay(1000);

                        var mensagens = driver.FindElements(By.XPath("//div[contains(@class, 'message-in')]//span[contains(@class, 'selectable-text')]"));

                        if (mensagens.Count > 0)
                        {
                            string textoRecebido = mensagens.Last().Text.ToLower().Trim();
                            string resposta = ProcessarResposta(textoRecebido);

                            if (!string.IsNullOrEmpty(resposta))
                            {
                                EnviarRespostaAuto(resposta);
                            }
                        }
                    }
                }
                catch { }
                await Task.Delay(2500);
            }
        }

        private string ProcessarResposta(string entrada)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                return "🚨 *ATENÇÃO!* Nas Sextas-feiras não temos aulas!\n\nMe envia outro dia e horário que você consegue estar vindo!\n\nHorários de funcionamento:\nSegunda: 08:00 - 20:00\nTerça: 08:00 - 20:00\nQuarta: 08:00 - 17:00\nQuinta: 08:00 - 20:00\nSexta: Fechado.\nSábado: 08:00 - 12:00\nDomingo: Fechado.";
            }

            switch (entrada)
            {
                case "1":
                    return "Claro. Vamos agendar sua reposição, me envie um dia e horário que você consegue estar vindo.";
                case "2":
                    return "Ah sim, me envie o motivo de sua falta e um atestado / comprovante abaixo e aproveitando me envie um dia e horário que você consegue estar vindo realizar essas aulas!";
                case "3":
                    return "Ah claro, para agendar a aula do feriado me envie um dia e horário que você consegue estar vindo, por gentileza.";
                case "4":
                    return "Claro, abaixo está os nosso horários de funcionamento.\n\nSegunda-feira: 08:00 - 20:00\nTerça-feira: 08:00 - 20:00\nQuarta-feira: 08:00 - 17:00\nQuinta-feira: 08:00 - 20:00\nSexta-feira: Fechado\nSábado: 07:00 - 15:00\nDomingo: Fechado";
                case "5":
                    return "Certo. aguarde até um dos professores está te dando o suporte necessário!";
                default:
                    return "*SUPORTE MICROLINS* 💙\n\nOlá! Para facilitar o seu contato, selecione abaixo o que você precisa resolver hoje.\nÉ só digitar o número: ✅\n\n1 ) 📅 Agendar Reposição\n2 ) 📑 Falta ou Atestado\n3 ) 🗓️ Aula de Feriado\n4 ) 🕒 Horário de Funcionamento\n5 ) 💬 Outros Assuntos";
            }
        }

        private void EnviarRespostaAuto(string msg)
        {
            try
            {
                IWebElement campo = driver.FindElement(By.CssSelector("footer div[contenteditable='true']"));
                if (campo != null)
                {
                    this.Invoke((MethodInvoker)delegate { Clipboard.SetText(msg); });
                    campo.Click();
                    campo.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                    Task.Delay(300).Wait();
                    campo.SendKeys(OpenQA.Selenium.Keys.Enter);
                }
            }
            catch { }
        }
    }
}