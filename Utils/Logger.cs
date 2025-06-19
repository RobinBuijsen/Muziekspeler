namespace Muziekspeler.Utils
{
    public static class Logger
    {
        private static readonly object _lock = new();

        public static void Info(string message)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                Console.ResetColor();
            }
        }

        public static void Warning(string message)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[⚠️ {DateTime.Now:HH:mm:ss}] {message}");
                Console.ResetColor();
            }
        }

        public static void Error(string message)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[❌ {DateTime.Now:HH:mm:ss}] {message}");
                Console.ResetColor();
            }
        }
    }
}
