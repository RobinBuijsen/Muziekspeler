namespace Muziekspeler.Model
{
    public class UserRequest
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Song RequestedSong { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public UserRequest(Guid userId, Song song)
        {
            UserId = userId;
            RequestedSong = song;
        }

        public override string ToString()
        {
            return $"[Request {RequestId}] User {UserId} requested '{RequestedSong.Title}' at {Timestamp:HH:mm:ss}";
        }
    }
}
