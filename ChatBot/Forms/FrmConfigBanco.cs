using System;
using System.IO;
using System.Reflection; // Necessário para acessar o recurso embutido
using System.Windows.Forms;

namespace ChatBot.Forms
{
    public partial class FrmConfigBanco : Form
    {
        public FrmConfigBanco()
        {
            InitializeComponent();
        }

        private void FrmConfigBanco_Load(object sender, EventArgs e)
        {
            txtCaminho.Text = Properties.Settings.Default.CaminhoBanco;
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Banco Access (*.accdb)|*.accdb|Todos os arquivos (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtCaminho.Text = ofd.FileName;
                }
            }
        }

        // --- NOVO BOTÃO: RESTAURAR PADRÃO ---
        private void btnRestaurarPadrao_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Caminho da Área de Trabalho
                string areaDeTrabalho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string destinoFinal = Path.Combine(areaDeTrabalho, "Dados.accdb");

                // 2. O NOME DO RECURSO (Tente exatamente assim, respeitando as maiúsculas)
                // Se o nome do seu projeto for exatamente 'ChatBot', o caminho é:
                string resourceName = "ChatBot.BD.Dados.accdb";

                var assembly = Assembly.GetExecutingAssembly();

                using (Stream input = assembly.GetManifestResourceStream(resourceName))
                {
                    if (input == null)
                    {
                        // Se der erro, vamos listar os nomes reais para você ver qual é o certo
                        string nomesDisponiveis = string.Join("\n", assembly.GetManifestResourceNames());
                        MessageBox.Show("Não achei o banco. Nomes disponíveis no sistema:\n\n" + nomesDisponiveis);
                        return;
                    }

                    using (Stream output = File.Create(destinoFinal))
                    {
                        input.CopyTo(output);
                    }
                }

                txtCaminho.Text = destinoFinal;
                MessageBox.Show("Sucesso! O banco foi criado na sua Área de Trabalho.", "Feito!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string novoCaminho = txtCaminho.Text;

            if (File.Exists(novoCaminho))
            {
                Properties.Settings.Default.CaminhoBanco = novoCaminho;
                Properties.Settings.Default.Save();

                MessageBox.Show("Configuração salva!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado! Verifique o caminho.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}