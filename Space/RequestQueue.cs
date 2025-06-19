using System.Collections.Concurrent;
using Muziekspeler.Model;

namespace Muziekspeler.Space
{
    public class RequestQueue
    {
        private readonly ConcurrentQueue<UserRequest> _queue = new();

        public void Enqueue(UserRequest request)
        {
            _queue.Enqueue(request);
            Console.WriteLine($"[Queue] Verzoek toegevoegd: {request}");
        }

        public bool TryDequeue(out UserRequest? request)
        {
            return _queue.TryDequeue(out request);
        }

        public int Count => _queue.Count;
    }
}
