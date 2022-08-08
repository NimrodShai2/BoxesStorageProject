using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructures
{
    public class MyStack<T>
    {
        private DoubleLinkedList<T> _list;
        public int Count { get { return _list.Count; } }
        public MyStack()
        {
            _list = new DoubleLinkedList<T>();
        }
        public bool IsEmpty { get { return _list.IsEmpty; } }
        public void Push(T value)
        {
            _list.AddToStart(value);

        }
        public T Pop()
        {
            if (_list.IsEmpty)
                return default(T);
            T value = _list.First;
            _list.Remove(_list.First);
            return value;
        }
    }
}

