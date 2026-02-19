using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChatBot.Forms
{
    public partial class FrmIdentificacao : Form
    {
        private readonly string operadorAtual;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        // Construtor que recebe o operador atual do painel
        public FrmIdentificacao(string operador = "")
        {
            InitializeComponent();
            this.operadorAtual = operador;

            txtNomeOperador.CharacterCasing = CharacterCasing.Upper;
            this.AcceptButton = btnAcessar;
            this.MouseDown += FrmIdentificacao_MouseDown;
        }

        private void FrmIdentificacao_Load(object sender, EventArgs e)
        {
            txtNomeOperador.Text = operadorAtual ?? "";

            if (!string.IsNullOrEmpty(operadorAtual))
            {
                txtNomeOperador.SelectionStart = operadorAtual.Length;
            }
        }

        private void FrmIdentificacao_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        // === MÉTODO ATUALIZADO QUE VOCÊ PEDIU ===
        private void btnAcessar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNomeOperador.Text))
            {
                Properties.Settings.Default.OperadorAtual = txtNomeOperador.Text.Trim().ToUpper();
                Properties.Settings.Default.Save();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, identifique-se.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}