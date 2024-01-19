using System.ComponentModel;
using System.Net.Mime;
using System.Windows.Forms;
using WinFormsApp1.Struttura;

namespace WinFormsApp1
{
    partial class RegisterWindow
    {
        private IContainer _components = null;
        private Label _lblHeader;
        private Label _lblDescription;
        private Label _lblNome;
        private TextBox _txtNome;
        private Label _lblCognome;
        private TextBox _txtCognome;
        private Label _lblEmail;
        private TextBox _txtEmail;
        private Label _lblPassword;
        private TextBox _txtPassword;
        private Label _lblConfermaPassword;
        private TextBox _txtConfermaPassword;
        private Button _btnSubmit;
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
            // Inizializza i controlli
            _backBack = new Button();
            _lblHeader = new Label();
            _lblDescription = new Label();
            _lblNome = new Label();
            _txtNome = new TextBox();
            _lblCognome = new Label();
            _txtCognome = new TextBox();
            _lblEmail = new Label();
            _txtEmail = new TextBox();
            _lblPassword = new Label();
            _txtPassword = new TextBox();
            _lblConfermaPassword = new Label();
            _txtConfermaPassword = new TextBox();
            _btnSubmit = new Button();
            _lblOutput = new Label();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Ottiene le dimensioni dello schermo principale
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            //La dimensione della finestra
            Size = new System.Drawing.Size((int)(screenWidth * 0.5), (int)(screenHeight * 0.5));

            // Configura il testo centrale in testa
            _lblHeader.AutoSize = true;
            _lblHeader.Font = new Font("Arial", 28, FontStyle.Bold);
            _lblHeader.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.1));
            _lblHeader.Text = "Registrazione";
            Controls.Add(_lblHeader);

            // Configura la piccola descrizione sotto
            _lblDescription.AutoSize = true;
            _lblDescription.Font = new Font("Arial", 14, FontStyle.Regular);
            _lblDescription.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.15));
            _lblDescription.Text = "Inserire dati per la registrazione";
            Controls.Add(_lblDescription);

            // Configura le Label e i TextBox
            ConfiguraCampo(_lblNome, _txtNome, "Nome", (int)(this.ClientSize.Height * 0.35));
            ConfiguraCampo(_lblCognome, _txtCognome, "Cognome", (int)(this.ClientSize.Height * 0.40));
            ConfiguraCampo(_lblEmail, _txtEmail, "Email", (int)(this.ClientSize.Height * 0.45));
            ConfiguraCampo(_lblPassword, _txtPassword, "Password", (int)(this.ClientSize.Height * 0.50));
            ConfiguraCampo(_lblConfermaPassword, _txtConfermaPassword, "Conferma Password", (int)(this.ClientSize.Height * 0.55));

            // Configura il Button al centro in basso
            _btnSubmit.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.90));
            _btnSubmit.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
            _btnSubmit.Text = "Submit";
            _btnSubmit.Click += new System.EventHandler(btnSubmit_Click);

            // Configura il Label di output
            _lblOutput.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.9));
            _lblOutput.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.05));

            // Aggiungi i controlli al form
            Controls.Add(_lblNome);
            Controls.Add(_txtNome);
            Controls.Add(_lblCognome);
            Controls.Add(_txtCognome);
            Controls.Add(_lblEmail);
            Controls.Add(_txtEmail);
            Controls.Add(_lblPassword);
            Controls.Add(_txtPassword);
            Controls.Add(_lblConfermaPassword);
            Controls.Add(_txtConfermaPassword);
            Controls.Add(_btnSubmit);
            Controls.Add(_lblOutput);

            // Configura il form
            Text = "Registrazione Utente";

            // Configura il pulsante "Back"
            _backBack.Text = "Back";
            _backBack.ImageAlign = ContentAlignment.MiddleLeft;
            _backBack.Click += new System.EventHandler(backButton_Click);
            _backBack.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
            Controls.Add(_backBack);

        }

        // Gestore dell'evento per il pulsante "Back"
        private void backButton_Click(object sender, EventArgs e)
        {
            // Nasconde la finestra corrente
            this.Hide();

            // Crea una nuova istanza della finestra di avvio
            StartupWindow precedente = new StartupWindow();

            // Visualizza la finestra di avvio come finestra modale
            precedente.ShowDialog();

            // Chiude la finestra corrente dopo la visualizzazione della finestra di avvio
            this.Close();
        }

        
        private void ConfiguraCampo(Label label, TextBox textBox, string nomeCampo, int yPos)
        {
            // Configura la Label
            label.AutoSize = true;
            label.Location = new System.Drawing.Point((int)(this.ClientSize.Width*0.4), yPos);
            label.Text = nomeCampo;
         

            // Configura il TextBox
            textBox.Location = new System.Drawing.Point((int)(this.ClientSize.Width*0.5), yPos);
            textBox.Size = new System.Drawing.Size((int)(this.ClientSize.Width*0.1), (int)(this.ClientSize.Height*0.1));

            // Aggiungi i controlli al form
            Controls.Add(label);
            Controls.Add(textBox);
        }

        // Gestore dell'evento per il pulsante "Submit"
        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            // Recupera i valori dai campi di input
            string nome = _txtNome.Text;
            string cognome = _txtCognome.Text;
            string email = _txtEmail.Text;
            string password = _txtPassword.Text;
            string confermaPassword = _txtConfermaPassword.Text;

            // Trova la posizione del simbolo '@' nell'indirizzo email
            int atIndex = email.IndexOf('@');

            // Verifica se tutti i campi sono stati compilati
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(cognome) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confermaPassword))
            {
                // Verifica se le password coincidono
                if (password == confermaPassword)
                {
                    // Verifica se l'indirizzo email è valido
                    if (atIndex != -1 && atIndex > 0 && atIndex < email.Length - 1)
                    {
                        // Messaggio di output positivo
                        _lblOutput.Text = "I dati sono stati inseriti correttamente";
                        
                        // Creazione di una richiesta di registrazione
                        RichiestaRegistrazione Request = new RichiestaRegistrazione(email, password, nome, cognome);
                        
                        // Esegui la richiesta POST per la registrazione
                        var risultato = await Request.EseguireRegisterPost();

                        // Gestione dell'esito della registrazione
                        if (risultato == "Registrazione riuscita!")
                        {
                            // Visualizza un messaggio di successo e apri la finestra di login
                            MessageBox.Show("Esito: " + risultato + "\nOra puoi effettuare il Login", "Esito Positivo");
                            LoginWindow nuovaMaschera = new LoginWindow();
                            this.Hide();
                            nuovaMaschera.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            // Visualizza un messaggio di errore e aggiorna l'etichetta di output
                            MessageBox.Show("Esito: " + risultato, "Esito Negativo");
                            _lblOutput.Text = risultato;
                        }
                    }
                    else
                    {
                        // Indirizzo email non valido
                        _lblOutput.Text = "Si prega di inserire un indirizzo email valido";
                    }
                }
                else
                {
                    // Le password non coincidono
                    _lblOutput.Text = "Le due password non coincidono";
                }
            }
            else
            {
                // Non tutti i campi sono stati compilati
                _lblOutput.Text = "Si prega di compilare tutti i campi";
            }
        }


        #endregion
    }
}
