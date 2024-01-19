using WinFormsApp1.Struttura;

namespace WinFormsApp1;

public partial class UiManagerWindow : Form
{
    private RichiestaLogin _struttura;
   public UiManagerWindow(RichiestaLogin dato)
    {
        this._struttura = dato;
        InitializeComponent();
    }
}