using Newtonsoft.Json;

namespace WinFormsApp1.Struttura;

public class RichiestaRegistrazione : RichiestaLogin
{
    // Attributi privati


    public RichiestaRegistrazione(string email, string password, string nome, string cognome) : base(email, password)
    {
        Nome = nome;
        Cognome = cognome;
    }

    public RichiestaRegistrazione(string strutturaEmail, string strutturaPassword) : base(strutturaEmail, strutturaPassword)
    {
    }

    // Proprietà (getter e setter) per l'attributo 'nome'
    [JsonProperty("Nome")]
    public string? Nome { get; set; }

    // Proprietà (getter e setter) per l'attributo 'cognome'
    [JsonProperty("Cognome")]
    public string? Cognome { get; set; }

    public  async Task<string> EseguireRegisterPost()
    {
        var url = "http://127.0.0.1:25536/api/v1/register";
        string serializzato = this.GetOggettoSerializzato(this);
        
        // Chiamare il metodo della classe padre utilizzando 'base'
        var result = await base.EseguireRichiestaPost(url, serializzato);
        

        return result;
    }
    
    public  async Task<string> EseguireUpdatePost(RichiestaRegistrazione oldValue)
    {
        var url = "http://127.0.0.1:25536/api/v1/updatedata";
        
        List<Object> objectList = new List<Object>();
        
        objectList.Add(oldValue);
        objectList.Add(this);

        string valoriSerializzati = JsonConvert.SerializeObject(objectList);
        
        // Chiamare il metodo della classe padre utilizzando 'base'
        var result = await base.EseguireRichiestaPost(url, valoriSerializzati);
        

        return result;
    }
    
    public  async Task<string> EseguireDeletePost()
    {
        var url = "http://127.0.0.1:25536/api/v1/deleteuser";
        string serializzato = this.GetOggettoSerializzato(this);
        
        // Chiamare il metodo della classe padre utilizzando 'base'
        var result = await base.EseguireRichiestaPost(url, serializzato);
        

        return result;
    }
    
    // Sovrascrivi il metodo ToString() per aggiungere informazioni specifiche di RichiestaRegistrazione
    public override string ToString()
    {
        return $"{base.ToString()}, Nome: {Nome}, Cognome: {Cognome}";
    }
}