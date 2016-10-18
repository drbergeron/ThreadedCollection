using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Tcollection
{
    public class ThreadedCollection<T> : IEnumerable
        where T : IThreadedCollectionObject 
    {
        private List<T> _data;
        private Stopwatch _watch;
        private List<Task> _tasks;

        public bool LogDuration { get; set; } = false;
        public TimeSpan Duration { get; private set; }

        public int count { get { return _data.Count; } }

        public ThreadedCollection()
        {
            _tasks = new List<Task>();
            _data = new List<T>();
            _watch = new Stopwatch();
        }

        public void Process()
        {
            if (LogDuration) _watch.Start();
            /*foreach (var x in _data)
            {
                new Task.Run(()=> { x.Process(); });
            }
            Task.WaitAll();*/

            var t = Task.WhenAll(_data.Select(x => x.Process()).ToArray());

            if (LogDuration)
            {
                _watch.Stop();
                Duration = _watch.Elapsed;
            }
        }

        public void ProcessNoThreading()
        {
            if (LogDuration) _watch.Start();
            foreach (var x in _data)
            {
                x.Process().Wait(); 
            }
            if (LogDuration)
            {
                _watch.Stop();
                Duration = _watch.Elapsed;
            }
        }

        public void Add(T objIn)
        {
            _data.Add(objIn);
        }

        public void Clear()
        {
            _data = new List<T>();
        }

        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

    }
}
