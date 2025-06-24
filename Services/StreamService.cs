using Muziekspeler.Model;
using Muziekspeler.Space;

namespace Muziekspeler.Services
{
    public class StreamService
    {
        private readonly DataGrid _dataGrid;
        private readonly List<Song> _playlist = new();
        private int _currentIndex = 0;

        private bool isRunning = false;
        private bool isPaused = false;

        public bool IsRunning => isRunning;
        public bool IsPaused => isPaused;
        public Song? CurrentSong => isRunning && !isPaused && _playlist.Count > 0
            ? _playlist[_currentIndex]
            : null;

        public StreamService(DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public void LoadPlaylist()
        {
            _playlist.Clear();
            _playlist.AddRange(_dataGrid.SongCatalog.Values);

            _currentIndex = 0;
        }

        public void Play()
        {
            if (_playlist.Count == 0) LoadPlaylist(); 

            isRunning = true;
            isPaused = false;

            Console.WriteLine($"▶️ Playing: {_playlist[_currentIndex]}");
        }

        public void Pause()
        {
            if (!isRunning) return;
            isPaused = true;
            Console.WriteLine("⏸️ Paused");
        }

        public void Next()
        {
            if (_playlist.Count == 0)
            {
<<<<<<< HEAD
                LoadPlaylist();
=======
                if (_dataGrid.RequestQueue.TryDequeue(out var request))
                {
                    if (_dataGrid.ActiveUsers.ContainsKey(request.UserId))
                    {
                        // Check of gebruiker al een actieve stream heeft
                        if (_dataGrid.ActiveStreams.TryGetValue(request.UserId, out var previousSong))
                        {
                            _dataGrid.Log($"🔁 {request.UserId} was al aan het streamen: '{previousSong.Title}'. Stream wordt vervangen door: '{request.RequestedSong.Title}'");
                        }

                        // Start nieuwe stream
                        _dataGrid.ActiveStreams[request.UserId] = request.RequestedSong;
                        _dataGrid.Log($"✅ Stream gestart: {request.UserId} → {request.RequestedSong.Title}");

                        // Simuleer het afspelen van de song
                        _ = Task.Run(async () =>
                        {
                            await Task.Delay(request.RequestedSong.Duration);

                            // Check of de stream nog steeds actief is voor dit nummer
                            if (_dataGrid.ActiveStreams.TryGetValue(request.UserId, out var currentSong)
                                && currentSong.Id == request.RequestedSong.Id)
                            {
                                _dataGrid.ActiveStreams.TryRemove(request.UserId, out _);
                                _dataGrid.Log($"🛑 Stream beëindigd: {request.UserId} → {request.RequestedSong.Title}");
                            }
                        });
                    }
                    else
                    {
                        _dataGrid.Warn($"⚠️ Onbekende gebruiker {request.UserId}. Verzoek genegeerd.");
                    }
                }
                else
                {
                    await Task.Delay(10);
                }
>>>>>>> 3ee45d4461cc904e6b1ec2cf31ed3ba6d89fcdaa
            }

            _currentIndex = (_currentIndex + 1) % _playlist.Count;
            isPaused = false;
            Console.WriteLine($"⏭️ Next: {_playlist[_currentIndex]}");
        }

        public void Stop()
        {
            isRunning = false;
            isPaused = false;
            _currentIndex = 0;
            Console.WriteLine("🛑 Stopped");
        }
    }
}
