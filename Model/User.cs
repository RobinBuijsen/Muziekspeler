namespace Muziekspeler.Model
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }

        public User(string username)
        {
            Username = username;
        }

        public override string ToString()
        {
            return $"User {Username} (ID: {Id})";
        }
    }
}
