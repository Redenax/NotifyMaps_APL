using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace WinFormsApp1.Struttura;

// Classe che gestisce le richieste REST utilizzando il pattern Circuit Breaker
public class RichiestaRest : IExeRichieste
{
    private readonly HttpClient httpClient;  // Client HTTP per effettuare le richieste

    // Costruttore che inizializza il cliente HTTP
    public RichiestaRest()
    {
        httpClient = new HttpClient();
    }

     // Metodo GET per eseguire una richiesta REST
    public virtual async Task<string> EseguireRichiestaGet(string url)
    {
        // Ottenere un'istanza del Circuit Breaker
        CircuitBreaker circuitBreaker = CircuitBreaker.Instance();
        try
        {
            // Verificare se il circuito è chiuso
            if (!circuitBreaker.is_open())
            {
                 // Iterare attraverso il numero di tentativi consentiti dallo stato corrente del Circuit Breaker
                for (int i = 0; i < (int)circuitBreaker.State; i++)
                {
                    try
                    {
                        // Eseguire la richiesta GET utilizzando il cliente HTTP
                        var response = await httpClient.GetAsync(url);

                        // Verificare se la richiesta ha avuto successo
                        if (response.IsSuccessStatusCode)
                        {
                            // Leggere il contenuto della risposta come stringa
                            var responseContent = await response.Content.ReadAsStringAsync();

                            // Se lo stato del Circuit Breaker è in fase di mezzo aperto, chiudere il circuito
                            if (circuitBreaker.State == CircuitBreaker.CircuitState.halfOpen)
                            {
                                circuitBreaker.close_circuit();
                            }

                            // Restituire il contenuto della risposta
                            return responseContent;
                        }
                        // Restituire il codice di errore in caso di risposta non riuscita
                        return "error code: " + response.StatusCode;
                    }
                    catch (Exception e)
                    {
                        // Gestire eventuali eccezioni durante l'esecuzione della richiesta
                        Console.WriteLine(e.Message);
                    }
                }

                // Se tutti i tentativi falliscono, aprire il circuito
                circuitBreaker.open_circuit();
                throw new Exception("Il circuito si è aperto.");
            }
            else
            {
                 // Se il circuito è aperto, generare un'eccezione con un messaggio appropriato
                throw new Exception("Il circuito è aperto, attendi riprova fra un po.");
            }
        }
        catch (Exception ex)
        {
            // Gestire eventuali eccezioni durante il controllo del Circuit Breaker o l'esecuzione della richiesta
            Console.WriteLine($"Errore Generico: {ex.Message}");
            return $"Errore Generico: {ex.Message}";
        }
    }

    // Metodo asincrono per eseguire una richiesta POST e ottenere una lista di stringhe come risposta
    public virtual async Task<List<string>> EseguireRichiestaPostList(string url, string dati)
    {
        // Creare un oggetto HttpContent con i dati da inviare
        HttpContent content = new StringContent(dati);

        CircuitBreaker circuitBreaker = CircuitBreaker.Instance();
        try
        {
            if (!circuitBreaker.is_open())
            {
                for (int i = 0; i < (int)circuitBreaker.State; i++)
                {
                    try
                    {
                        var response = await httpClient.PostAsync(url, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            if (circuitBreaker.State == CircuitBreaker.CircuitState.halfOpen)
                            {
                                circuitBreaker.close_circuit();
                            }

                            // Verificare se lo stato della risposta è Accepted
                            if (response.StatusCode == HttpStatusCode.Accepted)
                            {
                                Console.WriteLine(responseContent);

                                // Restituire una lista contenente la risposta
                                return new List<string> { responseContent };
                            }

                            Console.WriteLine(responseContent);

                            // Convertire la risposta (presumibilmente una stringa JSON) in una lista di stringhe
                            if (responseContent != "Lista vuota")
                            {
                                var risultato = JsonConvert.DeserializeObject<List<string>>(responseContent);

                                Console.WriteLine(risultato);

                                // Restituire la lista di stringhe ottenuta dalla risposta
                                return risultato;
                            }

                            responseContent = await response.Content.ReadAsStringAsync();

                            // Gestione degli errori
                            Console.WriteLine($"Errore nella richiesta: {response.StatusCode}\n{responseContent}");

                            // Restituisci una lista vuota in caso di errore
                            return new List<string>
                                { $"Errore: {response.StatusCode}\nRisposta dal server: {responseContent}" };
                        }
                    }
                    catch (Exception e)
                    {
                        // Gestire eventuali eccezioni durante l'esecuzione della richiesta
                        var risultato = JsonConvert.DeserializeObject<List<string>>(e.Message);
                        Console.WriteLine(risultato);
                    }
                }

                // Se tutti i tentativi falliscono, aprire il circuito
                circuitBreaker.open_circuit();
                throw new Exception("Il circuito si è aperto.");
            }

            // Se il circuito è ancora aperto, generare un'eccezione con un messaggio appropriato
            throw new Exception("Il circuito è aperto, attendi riprova fra un po.");
        }
        catch (Exception ex)
        {
            // Gestione delle eccezioni, ad esempio:
            Console.WriteLine($"Errore Generico: {ex.Message}");
            // Restituisci una lista vuota in caso di eccezione
            return new List<string> { $"Errore Generico: {ex.Message}" };
        }
    }

    public virtual async Task<List<string>> EseguireRichiestaGetList(string url)
    {
        CircuitBreaker circuitBreaker = CircuitBreaker.Instance();
        try
        {
            if (!circuitBreaker.is_open())
            {
                for (int i = 0; i < (int)circuitBreaker.State; i++)
                {
                    try
                    {
                        var response = await httpClient.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            if (circuitBreaker.State == CircuitBreaker.CircuitState.halfOpen)
                            {
                                circuitBreaker.close_circuit();
                            }

                            // Converti la risposta (presumibilmente una stringa JSON) in una lista di stringhe
                            var risultato = JsonConvert.DeserializeObject<List<string>>(responseContent);
                            return risultato;
                        }
                        else
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            
                            if (circuitBreaker.State == CircuitBreaker.CircuitState.halfOpen)
                            {
                                circuitBreaker.close_circuit();
                            }
                            
                            // Gestione degli errori, ad esempio:
                            Console.WriteLine($"Errore nella richiesta: {response.StatusCode}\n{responseContent}");
                            // Restituisci una lista vuota in caso di errore
                            return new List<string>
                                { $"Errore: {response.StatusCode}\nRisposta dal server: {responseContent}" };
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                // ha provato a collegari al server ma qualcosa è andato storto
                circuitBreaker.open_circuit();
                throw new Exception("Il circuito si è aperto.");
            }

            // circuito ancora aperto
            throw new Exception("Il circuito è aperto, attendi riprova fra un po.");
        }
        catch (Exception ex)
        {
            // Gestione delle eccezioni, ad esempio:
            Console.WriteLine($"Errore Generico: {ex.Message}");
            // Restituisci una lista vuota in caso di eccezione
            return new List<string> { $"Errore Generico: {ex.Message}" };
        }
    }

    public virtual async Task<string> EseguireRichiestaPost(string url, string dati)
    {
        HttpContent content = new StringContent(dati);
        CircuitBreaker circuitBreaker = CircuitBreaker.Instance();
        try
        {
            if (!circuitBreaker.is_open())
            {
                for (int i = 0; i < (int)circuitBreaker.State; i++)
                {
                    try
                    {
                        var response = await httpClient.PostAsync(url, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Risultato: {response.StatusCode}\n{responseContent}");
                            if (circuitBreaker.State == CircuitBreaker.CircuitState.halfOpen)
                            {
                                circuitBreaker.close_circuit();
                            }

                            return responseContent;
                        }

                        return "error code: " + response.StatusCode.GetTypeCode();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                circuitBreaker.open_circuit();
                throw new Exception("Il circuito si è aperto.");
            }

            throw new Exception("Il circuito è aperto, attendi riprova fra un po.");
        }
        catch (Exception ex)
        {
            // Gestione degli eccezioni, ad esempio:
            Console.WriteLine($"Errore Generico:  {ex.Message}");
            return "Errore Generico: " + ex.Message;
        }
    }

// Metodo che restituisce l'oggetto serializzato in formato JSON
    public string GetOggettoSerializzato(object oggettoDaSerializzare)
    {
        // Serializza l'oggetto in formato JSON
        string json = JsonConvert.SerializeObject(oggettoDaSerializzare);
        return json;
    }
}