namespace WinFormsApp1.Struttura;

using System;

 // Classe che implementa il pattern Circuit Breaker per la gestione delle chiamate a servizi
public class CircuitBreaker
{
    private readonly int _resetTimeout; // Timeout per il reset del circuito
    public CircuitState State { get; set; } = CircuitState.Closed; // Stato corrente del circuito
    private DateTime? _lastFailureTime = null; // Timestamp dell'ultima fallita richiesta
    private static CircuitBreaker _instance; // Istanza unica del CircuitBreaker

    // Metodo per ottenere l'istanza unica del CircuitBreaker utilizzando il pattern Singleton
    public static CircuitBreaker Instance()
    {
        // Se l'istanza non è stata creata, creala
        if (_instance == null)
        {
            _instance = new CircuitBreaker();
        }

        // Restituisci l'istanza unica
        return _instance;
    }

     // Costruttore che imposta il timeout di reset del circuito
    public CircuitBreaker()
    {
        _resetTimeout = 30; // Valore di default per il timeout
    }

    // Metodo per aprire il circuito in caso di fallimento delle chiamate
    public void open_circuit()
    {
        State = CircuitState.Open;
        _lastFailureTime = DateTime.Now; // Registra il timestamp dell'ultima fallita richiesta
    }

    // Metodo per chiudere il circuito quando la situazione migliora
    public void close_circuit()
    {
        State = CircuitState.Closed;
    }

    // Metodo per mettere il circuito nello stato semi-aperto per testare la ripresa
    public void half_open()
    {
        State = CircuitState.halfOpen;
    }

    // Metodo per verificare se il circuito è aperto
    public bool is_open()
    {
        if (State == CircuitState.Open)
        {
            var currentTime = DateTime.Now;

            // Se è passato il tempo di reset, passa allo stato semi-aperto
            if (currentTime.Subtract(_lastFailureTime ?? DateTime.MinValue).Seconds > _resetTimeout)
            {
                half_open();
                return false;
            }

            return true;
        }

        return false;
    }

     // Enumerazione che rappresenta gli stati possibili del CircuitBreaker
    public enum CircuitState
    {
        Closed = 3, // Chiuso (operativo)
        Open = 0,   // Aperto (in stato di errore)
		halfOpen = 1 // Semi-Aperto (per testare la ripresa)
    }
}
