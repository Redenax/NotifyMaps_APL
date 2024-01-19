using System.ComponentModel;
using System.Windows.Forms;
using WinFormsApp1.Struttura;

namespace WinFormsApp1
{
    partial class LoginWindow : Form
    {
        private IContainer _components = null;
        private Label _lblHeader;
        private Label _lblDescription;
        private Label _lblEmail;
        private TextBox _txtEmail;
        private Label _lblPassword;
        private TextBox _txtPassword;
        private Button _btnLogin;
        private Label _lblOutput;
        private Button _backBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // Inizializza i controlli per la finestra di login
            _lblHeader = new Label();
            _lblDescription = new Label();
            _lblEmail = new Label();
            _txtEmail = new TextBox();
            _lblPassword = new Label();
            _txtPassword = new TextBox();
            _btnLogin = new Button();
            _lblOutput = new Label();
            _backBack = new Button();

            // Configura lo stile della finestra
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Imposta le dimensioni della finestra
            Size = new System.Drawing.Size((int)(screenWidth * 0.5), (int)(screenHeight * 0.5));

            // Configura la Label dell'header
            _lblHeader.AutoSize = true;
            _lblHeader.Font = new Font("Arial", 28, FontStyle.Bold);
            _lblHeader.Text = "Login";
            _lblHeader.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.1)); // Centra orizzontalmente
            Controls.Add(_lblHeader);

            // Configura la descrizione sotto l'header
            _lblDescription.AutoSize = true;
            _lblDescription.Font = new Font("Arial", 14, FontStyle.Regular);
            _lblDescription.Text = "Inserire dati per l'accesso";
            _lblDescription.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.45), (int)(this.ClientSize.Height * 0.2)); // Centra orizzontalmente
            Controls.Add(_lblDescription);

            // Configura le etichette "Email" e "Password" 
            ConfiguraCampo(_lblEmail, "Email", (int)(this.ClientSize.Height * 0.5));
            ConfiguraCampo(_lblPassword, "Password", (int)(this.ClientSize.Height * 0.6));

            // Configura i campi di testo
            ConfiguraCampo(_txtEmail, (int)(this.ClientSize.Height * 0.5));
            ConfiguraCampo(_txtPassword, (int)(this.ClientSize.Height * 0.6));

            // Configura il pulsante di login al centro in basso
            _btnLogin.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.8)); // Centra orizzontalmente
            _btnLogin.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
            _btnLogin.Text = "Login";
            _btnLogin.Click += new System.EventHandler(btnLogin_Click);

            // Configura la Label di output
            _lblOutput.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Width * 0.8));
            _lblOutput.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Width * 0.05));

            // Aggiunge i controlli al form
            Controls.Add(_lblEmail);
            Controls.Add(_txtEmail);
            Controls.Add(_lblPassword);
            Controls.Add(_txtPassword);
            Controls.Add(_btnLogin);
            Controls.Add(_lblOutput);

            // Configura il titolo della finestra
            Text = "Login Utente";

            // Configura il pulsante "Back"
            _backBack.Text = "Back";
            _backBack.ImageAlign = ContentAlignment.MiddleLeft;
            _backBack.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
            _backBack.Click += new System.EventHandler(backButton_Click);
            Controls.Add(_backBack);

        }

        private void ConfiguraCampo(Label label, string nomeCampo, int yPos)
        {
            // Configura la Label
            label.AutoSize = true;
            label.Font = new Font("Arial", 14, FontStyle.Regular);
            label.Text = nomeCampo;
            label.Location = new System.Drawing.Point((int)(this.ClientSize.Width*0.4), yPos); // A sinistra
            Controls.Add(label);
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            // Nascondi la finestra corrente
            this.Hide();

            // Crea o ottieni la finestra precedente (sostituisci con il nome effettivo della tua finestra precedente)
            StartupWindow precedente = new StartupWindow();

            // Mostra la finestra precedente in modalità dialog
            precedente.ShowDialog();

            // Chiudi la finestra corrente quando la finestra precedente viene chiusa
            this.Close();
        }

        private void ConfiguraCampo(TextBox textBox, int yPos)
        {
            // Configura il TextBox
            textBox.Location = new System.Drawing.Point((int)(this.ClientSize.Width*0.5), yPos);
            textBox.Size = new System.Drawing.Size((int)(this.ClientSize.Width*0.1), (int)(this.ClientSize.Height*0.01));
            Controls.Add(textBox);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Recupera i valori dai campi di testo Email e Password
            string email = _txtEmail.Text;
            string password = _txtPassword.Text;

            // Crea una richiesta di login con le credenziali inserite
            RichiestaLogin Request = new RichiestaLogin(email, password);

            // Esegui la richiesta di login usando il metodo EseguireLogin asincrono
            var risultato = await Request.EseguireLogin(Request);

            // Gestisci la risposta della richiesta di login
            if (risultato == "Authorized")
            {
                // Se l'accesso è autorizzato, mostra un messaggio positivo e apri la nuova finestra
                MessageBox.Show("Esito: " + risultato, "Accesso Autorizzato");
                UiManagerWindow nuovaMaschera = new UiManagerWindow(Request);
                this.Hide();
                nuovaMaschera.ShowDialog();
                this.Close();
            }
            else
            {
                // Se l'accesso non è autorizzato, mostra un messaggio negativo
                MessageBox.Show(risultato, "Accesso Negato");
            }

            // Aggiorna il label di output con un messaggio informativo
            _lblOutput.Text = $"Accesso effettuato per l'email {email}";

        }

        #endregion
    }
}
