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
                LoadPlaylist();
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
