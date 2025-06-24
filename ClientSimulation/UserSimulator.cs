using Muziekspeler.Model;
using Muziekspeler.Space;

namespace Muziekspeler.ClientSimulation
{
    public class UserSimulator
    {
        private readonly DataGrid _dataGrid;
        private readonly Random _random = new();
        private readonly List<User> _simulatedUsers = new();
        private readonly int _userCount;

        public UserSimulator(DataGrid dataGrid, int userCount = 50)
        {
            _dataGrid = dataGrid;
            _userCount = userCount;
        }

        public void InitializeUsers()
        {
            for (int i = 0; i < _userCount; i++)
            {
                var user = new User($"User{i + 1}");
                _dataGrid.ActiveUsers[user.Id] = user;
                _simulatedUsers.Add(user);
            }

            _dataGrid.Log($"👥 {_userCount} gebruikers gesimuleerd.");
        }

        public void StartSimulation(int intervalMs = 200, int maxRequests = 1000)
        {
            Task.Run(async () =>
            {
                var songList = _dataGrid.SongCatalog.Values.ToList();

                for (int i = 0; i < maxRequests; i++)
                {
                    var user = _simulatedUsers[_random.Next(_simulatedUsers.Count)];
                    var song = songList[_random.Next(songList.Count)];

                    var request = new UserRequest(user.Id, song);
                    _dataGrid.RequestQueue.Enqueue(request);
                    _dataGrid.Log($"📥 Verzoek {i + 1}/{maxRequests}: {user.Username} wil '{song.Title}' streamen");

                    await Task.Delay(intervalMs);
                }

                _dataGrid.Log($"🎯 Simulatie voltooid: {maxRequests} verzoeken verzonden.");
            });
        }
    }
}
