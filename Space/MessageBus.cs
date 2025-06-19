namespace Muziekspeler.Space
{
    public class MessageBus
    {
        private readonly object _lock = new();

        public void Publish(string message)
        {
            lock (_lock)
            {
                Console.WriteLine($"[Bus] {DateTime.Now:HH:mm:ss} >> {message}");
            }
        }
    }
}
