using Muziekspeler.ClientSimulation;
using Muziekspeler.Services;
using Muziekspeler.Space;
using Muziekspeler.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DataGrid>();
builder.Services.AddSingleton<SongRepository>();
builder.Services.AddSingleton<StreamService>();
builder.Services.AddSingleton<UserSimulator>();

var app = builder.Build();

// Initialisatie vóór het starten van de webserver
var dataGrid = app.Services.GetRequiredService<DataGrid>();
var songRepo = app.Services.GetRequiredService<SongRepository>();
var streamService = app.Services.GetRequiredService<StreamService>();
var userSimulator = app.Services.GetRequiredService<UserSimulator>();

Logger.Info("🚀 Muziekspeler Web POC gestart...");

// Laad songs
songRepo.LoadTestSongs();

// Start services
streamService.Play();
userSimulator.InitializeUsers();
userSimulator.StartSimulation(Config.RequestIntervalMs);

// Serve statische bestanden (HTML, JS)
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/play", (StreamService stream) =>
{
    stream.Play();
    return Results.Ok("Gestart");
});

app.MapPost("/pause", (StreamService stream) =>
{
    stream.Pause();
    return Results.Ok("Gepauzeerd");
});

app.MapPost("/next", (StreamService stream) =>
{
    stream.Next();
    return Results.Ok("Volgende");
});

app.MapGet("/nowplaying", (StreamService stream) =>
{
    var song = stream.CurrentSong;
    if (song == null)
        return Results.Ok("Er speelt niets");

<<<<<<< HEAD
    var info = $"{song.Artist} - {song.Title} ({song.Duration:mm\\:ss})";
    return Results.Ok(info);
});

app.Run();
=======
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
>>>>>>> 3ee45d4461cc904e6b1ec2cf31ed3ba6d89fcdaa
