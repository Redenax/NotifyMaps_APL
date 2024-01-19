using System.ComponentModel;

namespace WinFormsApp1
{
    partial class StartupWindow
    {
        private IContainer _components = null;
        private Button _button1;
        private Button _button2;
        private Label _label1;
        private Label _label2;
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
            
            // Ottiene le dimensioni dello schermo principale
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Imposta le proprietà della finestra
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size((int)(screenWidth * 0.5), (int)(screenHeight * 0.5));
            this.AutoSize = true;
            this.Text = "Client_MapsNotify";

            // Aggiunge un pulsante di registrazione
            this._button1 = new System.Windows.Forms.Button();
            this._button1.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.3), (int)(this.ClientSize.Height * 0.5));
            this._button1.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.1));
            this._button1.Text = "Registrazione";
            this._button1.Click += new System.EventHandler(this.button1_Click);
            this.Controls.Add(this._button1);

            // Aggiunge un pulsante di login
            this._button2 = new System.Windows.Forms.Button();
            this._button2.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.6), (int)(this.ClientSize.Height * 0.5));
            this._button2.Size = new System.Drawing.Size((int)(this.ClientSize.Width * 0.1), (int)(this.ClientSize.Height * 0.1));
            this._button2.Text = "Login";
            this._button2.Click += new System.EventHandler(this.button2_Click);
            this.Controls.Add(this._button2);

            // Aggiunge una label per il testo centrale
            this._label1 = new Label();
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.45), (int)(this.ClientSize.Height * 0.05));
            this._label1.Font = new Font("Arial", 24, FontStyle.Regular);
            this._label1.Text = "Benvenuto";
            this.Controls.Add(this._label1);

            // Aggiunge una label per il sottotesto centrale
            this._label2 = new Label();
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point((int)(this.ClientSize.Width * 0.45), (int)(this.ClientSize.Height * 0.1));
            this._label2.Font = new Font("Arial", 12, FontStyle.Regular);
            this._label2.Text = "Scegli come procedere";
            this.Controls.Add(this._label2);
            
        }

        #endregion

        
        // Gestisce il clic del pulsante di Registrazione
        private void button1_Click(object sender, EventArgs e)
        {
            // Crea una nuova istanza della finestra di registrazione
            RegisterWindow nuovaMaschera = new RegisterWindow();

            // Nasconde la finestra corrente
            this.Hide();

            // Visualizza la finestra di registrazione in modalità modale
            nuovaMaschera.ShowDialog();

            // Chiude la finestra corrente dopo la finestra di registrazione
            this.Close();
            
        }

        // Gestisce il clic del pulsante di login
        private void button2_Click(object sender, EventArgs e)
        {
            // Crea una nuova istanza della finestra di login
            LoginWindow nuovaMaschera = new LoginWindow();

            // Nasconde la finestra corrente
            this.Hide();

            // Visualizza la finestra di login in modalità modale
            nuovaMaschera.ShowDialog();

            // Chiude la finestra corrente dopo la finestra di login
            this.Close();
            
        }
    }
}
