using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerPattern
{
    public class ProducerConsumer<T>
    {
        private Queue<T> _queue = new Queue<T>();
        private readonly object _lock = new object();

        public void Produce(T input)
        {
            lock (_lock)
            {
                if (_queue.Count <= 3)
                {
                    _queue.Enqueue(input);
                }
                else
                {
                    Monitor.Wait(_lock);
                }

                Monitor.Pulse(_lock);
            }
        }

        public void Consume()
        {
            lock (_lock)
            {
                while (!_queue.Any())
                {
                    Monitor.Wait(_lock);

                }
                _queue.Dequeue();
                Monitor.Pulse(_lock);
            }
        }
    }
}
