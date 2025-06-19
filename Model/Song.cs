namespace Muziekspeler.Model
{
    public class Song
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }

        public Song(string title, string artist, TimeSpan duration)
        {
            Title = title;
            Artist = artist;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"{Artist} - {Title} ({Duration:mm\\:ss})";
        }
    }
}
