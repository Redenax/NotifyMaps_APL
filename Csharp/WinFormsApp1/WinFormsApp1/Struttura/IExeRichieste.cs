namespace WinFormsApp1.Struttura;

public interface IExeRichieste
{
    Task<string> EseguireRichiestaPost(string url, string datiJson);
    Task<string> EseguireRichiestaGet(string url);
}