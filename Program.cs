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

    var info = $"{song.Artist} - {song.Title} ({song.Duration:mm\\:ss})";
    return Results.Ok(info);
});

app.Run();
