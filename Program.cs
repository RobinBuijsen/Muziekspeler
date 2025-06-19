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
            userSimulator.StartSimulation(Config.RequestIntervalMs);

            // 5. Laat het programma draaien tot gebruiker afsluit
            Logger.Info("✅ Systeem draait. Druk op [Enter] om te stoppen...");
            Console.ReadLine();

            // 6. Stop background services netjes
            streamService.Stop();
            Logger.Info("🛑 Muziekspeler POC afgesloten.");
        }
    }
}
