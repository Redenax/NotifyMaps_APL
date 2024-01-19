using Newtonsoft.Json;
using WinFormsApp1.Struttura;

namespace WinFormsApp1;

using Struttura;


internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        CircuitBreaker circuitBreakerInstance = CircuitBreaker.Instance();
        ApplicationConfiguration.Initialize();
        Application.Run(new StartupWindow());
       
    }
}