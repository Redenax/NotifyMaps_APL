using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WinFormsApp1.Struttura;

namespace WinFormsApp1
{
    partial class UiManagerWindow : Form
    {
        private System.ComponentModel.IContainer _components = null;
        private List<Tuple<Label, Button>> _labelButtonPairs;
        private Button _refreshButton;
        private Label _emailLabel;
        private Label _subscribeText;
        private ComboBox _firstNameComboBox;
        private ComboBox _lastNameComboBox;
        private Button _submitButton;
        private Button _logoutButton;
        private Button _btnUpdate;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private async void InitializeComponent()
        {
            // Ottieni le dimensioni dello schermo
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Impostazioni della finestra
             this.FormBorderStyle = FormBorderStyle.FixedSingle;
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.Size = new System.Drawing.Size((int)(screenWidth * 0.5), (int)(screenHeight * 0.5));
             this.Text = "MapsNotifyManager - Desktop Version";

             // Label per l'indirizzo email
             this._emailLabel = new Label();
             this._emailLabel.Text = _struttura.Email;
             this._emailLabel.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.01), (int)(this.ClientSize.Height * 0.01));
             this.Controls.Add(this._emailLabel);

             // Testo centrato
             this._subscribeText = new Label();
             this._subscribeText.Text = "Iscriviti a una delle tratte!";
             this._subscribeText.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.05));
             this._subscribeText.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.1));
             this.Controls.Add(this._subscribeText);

             // ComboBox per il Nome
             this._firstNameComboBox = new ComboBox();
             this._firstNameComboBox.Items.AddRange(new object[] { });
             this._firstNameComboBox.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.4), (int)(this.ClientSize.Height * 0.2));
             this._firstNameComboBox.KeyPress += ComboBox_KeyPress;
             this.Controls.Add(this._firstNameComboBox);

             // ComboBox per il Cognome
             this._lastNameComboBox = new ComboBox();
             this._lastNameComboBox.Items.AddRange(new object[] { });
             this._lastNameComboBox.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * 0.2));
             this._lastNameComboBox.KeyPress += ComboBox_KeyPress;
             this.Controls.Add(this._lastNameComboBox);

             // Carica le province
             GetProvince();
             
             
             // Inizializza una lista di tuple contenente coppie di Label e Button
             _labelButtonPairs = new List<Tuple<Label, Button>>();
             ReloadLabels(_labelButtonPairs);

             // Bottone di Aggiornamento
             this._refreshButton = new Button();
             this._refreshButton.Text = "Aggiorna";
             this._refreshButton.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.2));
             this._refreshButton.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.05), (int)(this.ClientSize.Height * 0.05));
             this._refreshButton.Click += RefreshButton_Click;
             this.Controls.Add(this._refreshButton);

             // Bottone di Iscrizione
             this._submitButton = new Button();
             this._submitButton.Text = "Iscriviti";
             this._submitButton.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.6), (int)(this.ClientSize.Height * 0.2));
             this._submitButton.Size = new System.Drawing.Size(80, 30);
             this._submitButton.Click += SubmitButton_Click;
             this.Controls.Add(this._submitButton);
             
             // Bottone di Update
             this._btnUpdate = new System.Windows.Forms.Button();
             this._btnUpdate.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.3));
             this._btnUpdate.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.05), (int)(this.ClientSize.Height * 0.05));
             this._btnUpdate.Text = "Update";
             this._btnUpdate.Click += btnUpdate_Click;
             this.Controls.Add(this._btnUpdate);
             
             // Bottone di Logout
             this._logoutButton = new System.Windows.Forms.Button();
             this._logoutButton.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.8), (int)(this.ClientSize.Height * 0.1));
             this._logoutButton.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.05), (int)(this.ClientSize.Height * 0.05));
             this._logoutButton.Text = "Logout";
             this._logoutButton.Click += LogoutButton_Click;
             this.Controls.Add(this._logoutButton);
             
        }
        #endregion

        // Gestisce l'evento KeyPress per le ComboBox
        private static void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Annulla l'evento KeyPress per impedire l'inserimento di testo
            e.Handled = true;
        }

        // Gestisce l'evento Click del pulsante di invio
        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            // Ottieni i valori selezionati dalle ComboBox e l'indirizzo email
            string partenza = _firstNameComboBox.Text;
            string destinazione = _lastNameComboBox.Text;
            string email = _emailLabel.Text;

            // Verifica se partenza e destinazione sono diverse
            if (partenza != destinazione)
            {
                // Crea un'istanza di RichiestaRoute con i valori selezionati
                RichiestaRoute Request = new RichiestaRoute(partenza, destinazione, email);

                // Esegue l'inserimento della tratta e ottiene il risultato
                var risultato = Request.EseguireInsertRoute(Request);

                // Mostra un messaggio con l'esito dell'operazione
                MessageBox.Show("Esito: Iscrizione Effettuata", "Iscrizione Effettuata");

                // Cancella i valori delle label
                ClearLabels();

                // Ricarica solo le label
                ReloadLabels(_labelButtonPairs);
            }
            else
            {
                // Mostra il messaggio se partenza e destinazione sono uguali
                MessageBox.Show("La partenza e la destinazione devono essere diverse", "Esito Negativo");
            }
        }
        
        // Metodo per ottenere le province da un'API
        public async Task GetProvince()
        {
            
            RichiestaRest request = new RichiestaRest();
    
            // URL dell'API per ottenere le province
            var url = "http://127.0.0.1:25536/api/v1/getprovince";

            try
            {
                // Esegue una richiesta GET all'API per ottenere la lista delle province
                var risultato = await request.EseguireRichiestaGetList(url);

                // Pulisce gli elementi precedenti e aggiunge le province alle ComboBox
                _firstNameComboBox.Items.Clear();
                _firstNameComboBox.Items.AddRange(risultato.ToArray());
        
                _lastNameComboBox.Items.Clear();
                _lastNameComboBox.Items.AddRange(risultato.ToArray());
            }
            catch (Exception ex)
            {
                // Gestisce eventuali eccezioni durante la richiesta e mostra un messaggio di errore
                MessageBox.Show("Errore durante la richiesta: " + ex.Message, "Errore");
            }
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateWindow nuovaMaschera = new UpdateWindow(_struttura);
            
            this.Hide();
            nuovaMaschera.ShowDialog();
            this.Close();
        }
        
        // Gestisce l'evento Click del pulsante di aggiornamento
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            // Chiama il metodo per cancellare i valori delle label
            ClearLabels();

            // Chiama il metodo per ricaricare solo le label
            ReloadLabels(_labelButtonPairs);
        }

        
        // Metodo per ricaricare le Label
        private async void ReloadLabels(List<Tuple<Label, Button>> labelButtonPairs)
        {
            
            // Crea un'istanza di RichiestaRoute con l'indirizzo email corrente
            RichiestaRoute Request = new RichiestaRoute("", "", _emailLabel.Text);
    
            try
            {
                // Esegue una richiesta per ottenere l'elenco delle tratte dell'utente
                var risultato = await Request.EseguireGetRoute();

                await Task.Delay(100);
                
                // Verifica se l'utente è iscritto a delle tratte
                if (!risultato.SequenceEqual(new List<string> { "Lista vuota" }))
                {
                    // Se l'utente è iscritto a almeno una tratta, reimposta gli indici delle ComboBox
                    if (risultato.Count >= 0)
                    {
                        _firstNameComboBox.SelectedIndex = 0;
                        _lastNameComboBox.SelectedIndex = 0;
                    }

                    // Converte l'elenco delle tratte in un array di nomi
                    string[] labelNames = risultato.ToArray();
            
                    // Posizione iniziale delle Label nella GUI
                    double labelY = 0.3;

                    // Itera attraverso ogni tratta per creare Label e pulsanti
                    foreach (var labelName in labelNames)
                    {
                        // Crea una nuova Label
                        Label label = new Label();
                        label.Text = labelName;
                        label.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.02));
                        label.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.4), (int)(this.ClientSize.Height * labelY));
                        this.Controls.Add(label);

                        // Crea un nuovo pulsante per disiscriversi
                        Button unsubscribeButton = new Button();
                        unsubscribeButton.Text = "Disiscriviti";
                        unsubscribeButton.Tag = label;
                        unsubscribeButton.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.5), (int)(this.ClientSize.Height * labelY));
                        unsubscribeButton.Size = new System.Drawing.Size(100, 30);
                        unsubscribeButton.Click += UnsubscribeButton_Click;
                        this.Controls.Add(unsubscribeButton);

                        // Aggiunge una coppia di Label e pulsante alla lista
                        labelButtonPairs.Add(new Tuple<Label, Button>(label, unsubscribeButton));

                        // Incrementa la posizione Y per la prossima etichetta
                        labelY += 0.05;
                    }
                }
                else
                {
                // Mostra un messaggio se l'utente non è iscritto a nessuna tratta
                MessageBox.Show("Non sei iscritto a nessuna tratta", "Avviso");
                }
            }
            catch (Exception ex)
            {
             // Gestisce eventuali eccezioni durante la richiesta e mostra un messaggio di errore
             MessageBox.Show("Errore durante la richiesta: " + ex.Message, "Errore");
            }
        }


        // Metodo per cancellare le Label e i pulsanti esistenti
        private async void ClearLabels()
        {
            // Itera all'indietro attraverso la lista delle coppie di Label e pulsanti
            for (int i = _labelButtonPairs.Count - 1; i >= 0; i--)
            {
                // Ottiene la coppia corrente
                var pair = _labelButtonPairs[i];

                // Rimuove la Label e il pulsante dalla collezione di controlli della finestra
                this.Controls.Remove(pair.Item1);
                this.Controls.Remove(pair.Item2);

                // Rimuove la coppia dalla lista
                _labelButtonPairs.RemoveAt(i);
            }

            // Pulisce la lista di coppie di Label e pulsanti
            _labelButtonPairs.Clear();
        }

        
        // Gestisce l'evento Click del pulsante di disiscrizione
        private async void UnsubscribeButton_Click(object sender, EventArgs e)
        {
            // Ottiene il pulsante di disiscrizione e Label corrispondente
            Button unsubscribeButton = (Button)sender;
            Label correspondingLabel = (Label)unsubscribeButton.Tag;

            // Rimuove il pulsante e Label dalla collezione di controlli della finestra
            this.Controls.Remove(unsubscribeButton);
            this.Controls.Remove(correspondingLabel);

            // Estrae il topic dalla stringa rappresentante il tag
            string[] topic = this.fromTagToTopic(unsubscribeButton.Tag.ToString());

            // Crea un'istanza di RichiestaRoute per la disiscrizione dalla tratta
            RichiestaRoute Request = new RichiestaRoute(topic[0], topic[1], _emailLabel.Text);

            try
            {
                // Esegue una richiesta DELETE per disiscriversi dalla tratta
                Request.EseguireDeleteRoute(Request);

                // Mostra un messaggio di successo
                MessageBox.Show("Disiscrizione avvenuta con successo dalla tratta", "Disiscrizione Effettuata");

                // Cancella i valori delle etichette
                ClearLabels();

                // Ricarica solo le etichette
                ReloadLabels(_labelButtonPairs);
            }
            catch (Exception ex)
            {
                // Gestisce eventuali eccezioni durante la richiesta e mostra un messaggio di errore
                MessageBox.Show("Errore durante la disiscrizione: " + ex.Message, "Errore");
            }
        }


        // Metodo per convertire un tag in un array di stringhe rappresentante un topic
        private string[] fromTagToTopic(string topic)
        {
            // Divide il tag utilizzando il carattere ':'
            string[] partial = topic.Split(':');

            // Crea un StringBuilder per manipolare il risultato
            StringBuilder result = new StringBuilder(partial[1]);

            // Rimuove il primo carattere dal risultato
            result.Remove(0, 1);

            // Divide il risultato utilizzando il carattere '_'
            string[] partial2 = result.ToString().Split('_');

            return partial2;
        }
        
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // Qui mostro messaggio di logout
            MessageBox.Show("Logout effettuato con successo", "Logout");

            // Chiude la finestra corrente
            this.Close();
        }
    }
}
