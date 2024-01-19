using WinFormsApp1.Struttura;

namespace WinFormsApp1;

public partial class UpdateWindow : Form
{
    private  RichiestaLogin _struttura;

    public UpdateWindow(RichiestaLogin dato)
    {
        _struttura = dato;
        InitializeComponent();
    }
}