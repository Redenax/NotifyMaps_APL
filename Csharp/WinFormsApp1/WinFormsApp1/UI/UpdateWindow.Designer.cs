using System.ComponentModel;
using System.Text.Json;
using Newtonsoft.Json;
using WinFormsApp1.Struttura;

namespace WinFormsApp1;

partial class UpdateWindow
{
    private IContainer _components = null;
    private Label _lblHeader;
    private Label _lblTextAd;
    private Label _lblDescription;
    private Label _lblNome;
    private TextBox _txtNome;
    private Label _lblCognome;
    private TextBox _txtCognome;
    private Label _lblPassword;
    private TextBox _txtPassword;
    private Label _lblConfermaPassword;
    private TextBox _txtConfermaPassword;
    private Button _btnUpdate;
    private Label _lblOutput;
    private Button _backBack;
    private Button _btnDelete;
    private RichiestaRegistrazione _oldValue;
    
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
        
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        
        // Impostazioni della finestra
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Size = new System.Drawing.Size((int)(screenWidth * 0.5), (int)(screenHeight * 0.5));
        this.Text = "UpdateWindow";
        
        // Inizializza i controlli
        _backBack = new Button();
        _lblHeader = new Label();
        _lblTextAd = new Label();
        _lblDescription = new Label();
        _lblNome = new Label();
        _txtNome = new TextBox();
        _lblCognome = new Label();
        _txtCognome = new TextBox();
        _lblPassword = new Label();
        _txtPassword = new TextBox();
        _lblConfermaPassword = new Label();
        _txtConfermaPassword = new TextBox();
        _btnUpdate = new Button();
        _lblOutput = new Label();
        _btnDelete = new Button();
        
        // Configura il testo centrale in testa
        _lblHeader.AutoSize = true;
        _lblHeader.Font = new Font("Arial", 28, FontStyle.Bold);
        _lblHeader.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.40), (int)(this.ClientSize.Height * 0.1));
        _lblHeader.Text = "Modifca dati utente";
        Controls.Add(_lblHeader);
        
        // Configura il testo di avviso per l'utente
        _lblTextAd.AutoSize = true;
        _lblTextAd.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.40), (int)(this.ClientSize.Height * 0.30));
        _lblTextAd.Text = "Per confermare il cambio dei dati inserire la vecchia o una nuova password!";
        Controls.Add(_lblTextAd);
        
        // Configura le Label e i TextBox
        ConfiguraCampo(_lblNome, _txtNome, "Nome", (int)(this.ClientSize.Height * 0.35));
        ConfiguraCampo(_lblCognome, _txtCognome, "Cognome", (int)(this.ClientSize.Height * 0.40));
        ConfiguraCampo(_lblPassword, _txtPassword, "Password", (int)(this.ClientSize.Height * 0.45));
        ConfiguraCampo(_lblConfermaPassword, _txtConfermaPassword, "Conferma Password", (int)(this.ClientSize.Height * 0.50));

        getUser();
        
        // Configura il Button al centro in basso
        _btnUpdate.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.90));
        _btnUpdate.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
        _btnUpdate.Text = "Update";
        _btnUpdate.Click += new System.EventHandler(btnUpdate_Click);
        
        // Configura il Label di output
        _lblOutput.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.75));
        _lblOutput.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.05));

        Controls.Add(_btnUpdate);
        Controls.Add(_lblOutput);
        
        // Configura il pulsante "Back"
        _backBack.Text = "Back";
        _backBack.ImageAlign = ContentAlignment.MiddleLeft;
        _backBack.Click += new System.EventHandler(backButton_Click);
        _backBack.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
        Controls.Add(_backBack);
        
        // Configura il pulsante "delete"
        _btnDelete.Text = "Delete account";
        _btnDelete.Click += new System.EventHandler(deleteButton_Click);
        _btnDelete.Location = new System.Drawing.Point((int)(this.ClientSize.Width*0.9), (int)(this.ClientSize.Height * 0.01));
        _btnDelete.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
        Controls.Add(_btnDelete);

    }
    
    // Metodo per ottenere le informazioni dell'utente
    public async Task getUser()
    {
            
        RichiestaRest request = new RichiestaRest();
    
        // URL dell'API per ottenere le province
        var url = "http://127.0.0.1:25536/api/v1/getuserdata";

        try
        {
            var data = request.GetOggettoSerializzato(_struttura);
            // Esegue una richiesta Post all'API per ottenere l'utente
            var risultato = await request.EseguireRichiestaPost(url, data);
            // Parsifica la stringa JSON in un JsonDocument
            JsonDocument jsonDocument = JsonDocument.Parse(risultato);

            // Ottieni la radice dell'albero JSON
            JsonElement root = jsonDocument.RootElement;

            // Accedi ai campi uno per uno
            _txtNome.Text = root.GetProperty("Nome").GetString();
            _txtCognome.Text = root.GetProperty("Cognome").GetString();

            _oldValue = new RichiestaRegistrazione(_struttura.Email, _struttura.Password,
                root.GetProperty("Nome").GetString(), root.GetProperty("Cognome").GetString());

        }
        catch (Exception ex)
        {
            // Gestisce eventuali eccezioni durante la richiesta e mostra un messaggio di errore
            MessageBox.Show("Errore durante la richiesta: " + ex.Message, "Errore");
        }
    }
    
    
    // Gestore dell'evento per il pulsante "Back"
    private void backButton_Click(object sender, EventArgs e)
    {
        // Crea una nuova istanza della finestra di avvio
        UiManagerWindow precedente = new UiManagerWindow(_struttura);
        // Nasconde la finestra corrente
        this.Hide();

        // Visualizza la finestra di avvio come finestra modale
        precedente.ShowDialog();

        // Chiude la finestra corrente dopo la visualizzazione della finestra di avvio
        this.Close();
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
        // Recupera i valori dai campi di input
        string password = _txtPassword.Text;
        string confermaPassword = _txtConfermaPassword.Text;
        int atIndex = _struttura.Email.IndexOf('@');



        RichiestaRegistrazione Request = new RichiestaRegistrazione(_struttura.Email, _struttura.Password);

        var risultato = Request.EseguireDeletePost();

        // Mostra un messaggio con l'esito dell'operazione
        MessageBox.Show("Esito: Account eliminato con successo", "Account eliminato!");

        StartupWindow nextWindow = new StartupWindow();

        this.Hide();

        nextWindow.ShowDialog();
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

    
     // Gestore dell'evento per il pulsante "Update"
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            // Recupera i valori dai campi di input
            string nome = _txtNome.Text;
            string cognome = _txtCognome.Text;
            string email = _struttura.Email;
            string password = _txtPassword.Text;
            string confermaPassword = _txtConfermaPassword.Text;

            // Trova la posizione del simbolo '@' nell'indirizzo email
            int atIndex = email.IndexOf('@');

            // Verifica se tutti i campi sono stati compilati
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(cognome) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confermaPassword))
            {
                // Verifica se le password coincidono
                if (password == confermaPassword)
                {

                    // Messaggio di output positivo
                    _lblOutput.Text = "I dati sono stati inseriti correttamente";

                    // Creazione di una richiesta di registrazione
                    RichiestaRegistrazione Request = new RichiestaRegistrazione(email, password, nome, cognome);

                    // Esegui la richiesta POST per la registrazione
                    var risultato = await Request.EseguireUpdatePost(_oldValue);

                    // Gestione dell'esito della registrazione
                    if (risultato.Contains(Request.Email) && risultato.Contains(Request.Password) &&
                        risultato.Contains(Request.Nome) && risultato.Contains(Request.Cognome))
                    {
                        // Visualizza un messaggio di successo e apri la finestra di login
                        MessageBox.Show("Esito: Utente modifcato con successo", "Esito Positivo");

                        RichiestaLogin dato = new RichiestaLogin(email, Request.Password);

                        UiManagerWindow nuovaMaschera = new UiManagerWindow(dato);

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