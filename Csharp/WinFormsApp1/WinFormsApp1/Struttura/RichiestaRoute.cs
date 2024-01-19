using Newtonsoft.Json;

namespace WinFormsApp1.Struttura;

// Questa classe rappresenta una richiesta di informazioni sulla route, estendendo una classe base per le richieste REST.
public class RichiestaRoute : RichiestaRest
{
    // Il costruttore inizializza l'oggetto con i valori forniti per il punto di partenza, la destinazione e l'email.
    public RichiestaRoute(string partenza, string destinazione,string email)
    {
        Partenza = partenza;
        Destinazione = destinazione;
        Email = email;
    } 

    // Annotazione di proprietà JSON per la serializzazione/deserializzazione.
    [JsonProperty("Partenza")]
    public string Partenza { get; set; }
    
    [JsonProperty("Destinazione")]
    public string Destinazione { get; set; }
    [JsonProperty("Email")]
    public string Email { get; set; }

    // Override del metodo ToString per fornire una rappresentazione formattata dell'oggetto.
    public override string ToString()
    {
        return $"Partenza: {Partenza}, Destinazione: {Destinazione},Email: {Email} ";
    }

    // Override del metodo per eseguire una richiesta POST asincrona.
    public override async Task<string> EseguireRichiestaPost(string url, string dati)
    {

        var result = await base.EseguireRichiestaPost(url, dati);

        return result;
    }

    // Metodo per eseguire una richiesta GET sulla route e ottenere una lista di stringhe come risultato.
    public  async Task<List<string>> EseguireGetRoute()
    {

        var url = "http://127.0.0.1:25536/api/v1/getroute"; 
        var requestSerialized=this.GetOggettoSerializzato(this);
        var result = await base.EseguireRichiestaPostList(url, requestSerialized);

        return result;
    }

    // Metodo per eseguire una richiesta DELETE sulla route e ottenere una stringa come risultato.
    public  async Task<string> EseguireDeleteRoute(RichiestaRoute Request)
    {

        string requestSerialized = Request.GetOggettoSerializzato(Request);
        var url = "http://127.0.0.1:25536/api/v1/deletesRoute";
        var result = await Request.EseguireRichiestaPost(url, requestSerialized);
        
        return result;
    }

    // Metodo per eseguire una richiesta di inserimento sulla route e ottenere una stringa come risultato.
    public  async Task<string> EseguireInsertRoute(RichiestaRoute Request)
    {
        
        string serializzato = Request.GetOggettoSerializzato(Request);
        var url = "http://127.0.0.1:25536/api/v1/registerRoute";
        var result = await Request.EseguireRichiestaPost(url, serializzato);
        
        return result;
    }
}