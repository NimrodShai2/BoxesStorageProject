using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        #region Node
        /// <summary>
        /// A nested class for constructing a doubly linked list.
        /// </summary>
        internal class DoubleNode
        {
            public T Data { get; set; }
            private DoubleNode _next;
            private DoubleNode _prev;
            public DoubleNode(T data, DoubleNode next, DoubleNode prev)
            {
                Data = data;
                _next = next;
                _prev = prev;
            }
            public DoubleNode Next { get { return _next; } set { _next = value; } }
            public DoubleNode Prev { get { return _prev; } set { _prev = value; } }
        }
        #endregion

        DoubleNode _head;
        DoubleNode _tail;
        public DoubleLinkedList()
        {
            _head = null;
            _tail = null;
        }
        public void AddToStart(T value)
        {
            _head = new DoubleNode(value, _head, null);
            if (_head.Next != null)
                _head.Next.Prev = _head;
            if (_tail == null)
                _tail = _head;
        }
        public void AddToEnd(T value)
        {
            _tail = new DoubleNode(value, null, _tail);
            if (_tail.Prev != null)
                _tail.Prev.Next = _tail;
            if (_head == null)
                _head = _tail;
        }
        /// <summary>
        /// Removes an item from the list by traveling from both ends.
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
        {
            DoubleNode current1 = _head, current2 = _tail;
            while(current1 != null && current2 != null && !current2.Data.Equals(value) && !current1.Data.Equals(value))
            {
                current1 = current1.Next;
                current2 = current2.Prev;
            }
            if (current1.Data.Equals(value))
            {
                if (current1.Prev == null)
                {
                    _head = _head.Next;
                }
                else
                {
                    current1.Prev.Next = current1.Next;
                }
            }
            else if (current2.Data.Equals(value))
            {
                if (current2.Next == null)
                {
                    _tail = _tail.Prev;
                }
                else
                {
                    _tail.Next.Prev = current2.Prev;
                }
            }
        }
        #region Enumerator
        public IEnumerator<T> GetEnumerator()
        {
            var currrent = _head;
            while (currrent != null)
            {
                yield return currrent.Data;
                currrent = currrent.Next;
                if (currrent == null)
                    yield break;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        /// <summary>
        /// Represents the number of elements currently in the list.
        /// </summary>
        public int Count
        {
            get
            {
                int counter = 0;
                for (DoubleNode p = _head; p != null; p = p.Next)
                    counter++;
                return counter;
            }
        }
        /// <summary>
        /// First item in the list.
        /// </summary>
        public T First { get { return _head.Data; } }
        /// <summary>
        /// Last item in the list (first from end).
        /// </summary>
        public T Last { get { return _tail.Data; } }
        public bool IsEmpty { get { return _head == null; } }
        
    }
}
