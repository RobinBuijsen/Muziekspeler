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

// Initialisatie
var dataGrid = app.Services.GetRequiredService<DataGrid>();
var songRepo = app.Services.GetRequiredService<SongRepository>();
var streamService = app.Services.GetRequiredService<StreamService>();
var userSimulator = app.Services.GetRequiredService<UserSimulator>();

Logger.Info("🚀 Muziekspeler Web POC gestart...");

// 1. Laad songs
songRepo.LoadTestSongs();

// 2. Start stream
streamService.Play();

// 3. Simuleer gebruikers
userSimulator.InitializeUsers();
userSimulator.StartSimulation(Config.RequestIntervalMs, maxRequests: 50);

// 4. Realtime monitoring
_ = Task.Run(async () =>
{
    while (true)
    {
        Logger.Info($"📊 Actieve streams: {dataGrid.ActiveStreams.Count}");
        await Task.Delay(5000);
    }
});

// 5. Statische webinterface
app.UseDefaultFiles();
app.UseStaticFiles();

// 6. API Endpoints
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

    var info = $"{song.Artist} - {song.Title} ({song.Duration:mm\\:ss})";
    return Results.Ok(info);
});

// Lijst van alle songs
app.MapGet("/songs", (SongRepository repo) =>
{
    var songs = repo.GetAllSongs()
        .Select(song => new {
            id = song.Id,
            title = song.Title,
            artist = song.Artist,
            duration = song.Duration.ToString(@"mm\:ss")
        });

    return Results.Json(songs);
});

// Speel specifiek nummer
app.MapPost("/play/{id:guid}", (Guid id, DataGrid grid, StreamService stream) =>
{
    if (grid.SongCatalog.TryGetValue(id, out var song))
    {
        stream.PlaySpecificSong(song);
        return Results.Ok($"Speelt: {song.Title}");
    }
    return Results.NotFound("Liedje niet gevonden");
});

// 7. Start
Logger.Info("✅ Systeem draait. Druk op [Enter] om te stoppen...");
app.RunAsync();
Console.ReadLine();

streamService.Stop();
Logger.Info("🛑 Muziekspeler POC afgesloten.");
