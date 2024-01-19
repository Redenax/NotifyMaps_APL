using Newtonsoft.Json;

namespace WinFormsApp1.Struttura;

public class RichiestaLogin : RichiestaRest
{
    // Attributi privati

    // Costruttore
    public RichiestaLogin(string email, string password)
    {
        Email = email;
        Password = password;
    }

    // Proprietà (getter e setter) per l'attributo 'email'
    [JsonProperty("Email")]
    public string Email { get; set; }

    // Proprietà (getter e setter) per l'attributo 'password'
    [JsonProperty("Password")]
    public string Password { get; set; }

    // Metodo ToString per rappresentare l'oggetto come stringa
    public override string ToString()
    {
        return $"Email: {Email}, Password: {Password}";
    }
    
    public override async Task<string> EseguireRichiestaPost(string url, string dati)
    {
        // Chiamare il metodo della classe padre utilizzando 'base'
        var result = await base.EseguireRichiestaPost(url, dati);

        return result;
    }
    public  async Task<string> EseguireLogin(RichiestaLogin Request)
    {

        string serializzato=Request.GetOggettoSerializzato(Request);
        var url = "http://127.0.0.1:25536/api/v1/authentication";
        // Chiamare il metodo della classe padre utilizzando 'base'
        var result = await base.EseguireRichiestaPost(url, serializzato);


        return result;
    }
    
}