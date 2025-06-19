using System.Collections.Concurrent;
using Muziekspeler.Model;
using Muziekspeler.Utils;

namespace Muziekspeler.Space
{
    public class DataGrid
    {
        // Actieve users
        public ConcurrentDictionary<Guid, User> ActiveUsers { get; } = new();

        // Alle beschikbare songs
        public ConcurrentDictionary<Guid, Song> SongCatalog { get; } = new();

        // Binnenkomende verzoeken
        public ConcurrentQueue<UserRequest> RequestQueue { get; } = new();

        // Actieve streams
        public ConcurrentDictionary<Guid, Song> ActiveStreams { get; } = new();

        // Optioneel: MessageBus
        public MessageBus MessageBus { get; } = new();

        public void Log(string message)
        {
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            Logger.Warning(message);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }
    }
}
