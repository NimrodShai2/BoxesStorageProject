using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    public class MyQueue<T> : IEnumerable<T>
    {
        private DoubleLinkedList<T> _list;
        /// <summary>
        /// Number of items in the queue.
        /// </summary>
        public int Count { get { return _list.Count; } }
        public bool IsEmpty { get { return _list.IsEmpty; } }
        public MyQueue()
        {
            _list = new DoubleLinkedList<T>();
        }
        /// <summary>
        /// Add an item to the queue.
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(T value)
        {
            _list.AddToEnd(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The first item in the queue, or default value if the queue is empty.</returns>
        public T Dequeue()
        {
            if (IsEmpty)
                return default(T);
            T value = _list.First;
            _list.Remove(_list.First);
            return value;
        }
        /// <summary>
        /// The first item in the queue.
        /// </summary>
        public T FirstInLine { get { return _list.First; } }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
