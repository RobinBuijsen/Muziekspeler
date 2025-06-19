using Muziekspeler.Model;
using Muziekspeler.Space;

namespace Muziekspeler.Services
{
    public class SongRepository
    {
        private readonly DataGrid _dataGrid;

        public SongRepository(DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public void LoadTestSongs()
        {
            var songs = new List<Song>
            {
                new Song("Space Odyssey", "Zeno", TimeSpan.FromMinutes(3.5)),
                new Song("Cloud Surfing", "Aurora Sky", TimeSpan.FromMinutes(4)),
                new Song("Infinite Bass", "DJ Depth", TimeSpan.FromMinutes(2.8)),
                new Song("Solar Drift", "Nebulae", TimeSpan.FromMinutes(3.2)),
                new Song("Binary Love", "Code Pulse", TimeSpan.FromMinutes(3.75))
            };

            foreach (var song in songs)
            {
                _dataGrid.SongCatalog[song.Id] = song;
                _dataGrid.Log($"🎵 Song geladen: {song}");
            }
        }
    }
}
