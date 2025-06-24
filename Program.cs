using Muziekspeler.ClientSimulation;
using Muziekspeler.Services;
using Muziekspeler.Space;
using Muziekspeler.Utils;

namespace Muziekspeler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Logger.Info("🚀 Muziekspeler POC gestart...");

            // 1. Initialiseer centrale SBA-component
            var dataGrid = new DataGrid();

            // 2. Laad songs
            var songRepo = new SongRepository(dataGrid);
            songRepo.LoadTestSongs();

            // 3. Start StreamService
            var streamService = new StreamService(dataGrid);
            streamService.Start();

            // 4. Simuleer gebruikers
            var userSimulator = new UserSimulator(dataGrid, Config.UserCount);
            userSimulator.InitializeUsers();
            userSimulator.StartSimulation(Config.RequestIntervalMs, maxRequests: 50);

            // 5. Start monitoring thread (bijv. elke 5 seconden status)
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    Logger.Info($"📊 Actieve streams: {dataGrid.ActiveStreams.Count}");
                    await Task.Delay(5000); // elke 5 seconden
                }
            });


            // 6. Laat het programma draaien tot gebruiker afsluit
            Logger.Info("✅ Systeem draait. Druk op [Enter] om te stoppen...");
            Console.ReadLine();

            // 7. Stop background services netjes
            streamService.Stop();
            Logger.Info("🛑 Muziekspeler POC afgesloten.");
        }
    }
}
