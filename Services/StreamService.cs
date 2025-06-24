using Muziekspeler.Model;
using Muziekspeler.Space;

namespace Muziekspeler.Services
{
    public class StreamService
    {
        private readonly DataGrid _dataGrid;
        private volatile bool _isRunning = true;

        public StreamService(DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public void Start()
        {
            Task.Run(ProcessRequests);
        }

        private async Task ProcessRequests()
        {
            while (_isRunning)
            {
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
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
