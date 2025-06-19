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
                        _dataGrid.ActiveStreams[request.UserId] = request.RequestedSong;
                        _dataGrid.Log($"✅ Stream gestart voor gebruiker {request.UserId}: {request.RequestedSong}");
                    }
                    else
                    {
                        _dataGrid.Log($"⚠️ Onbekende gebruiker {request.UserId}. Verzoek genegeerd.");
                    }
                }
                else
                {
                    await Task.Delay(10); // even wachten als er niets is
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
