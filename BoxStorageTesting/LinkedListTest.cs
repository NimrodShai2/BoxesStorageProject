using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using BoxModel;
using System.Linq;
using GenericDataStructures;

namespace BoxStorageTesting
{
    [TestClass]
    public class LinkedListTest
    {
        static DoubleLinkedList<int> list = new DoubleLinkedList<int>();
        static Stopwatch stopwatch = new Stopwatch();
        [TestMethod]
        public void ListAddTest()
        {
            list.AddToStart(1);
            Assert.IsTrue(list.Contains(1));
        }
        [TestMethod]
        public void ListRemoveTest()
        {
            list.AddToStart(1);
            list.AddToStart(2);
            list.AddToStart(3);
            list.AddToStart(7);
            list.AddToStart(6);
            list.AddToStart(22);
            list.Remove(6);
            Assert.IsFalse(list.Contains(6));
            Assert.IsTrue(list.Contains(22));
        }
    }
    [TestClass]
    public class QueueTest
    {
        static MyQueue<int> queue = new MyQueue<int>();
        [TestMethod]
        public void DeQueueTest()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            int i = queue.Dequeue();
            Assert.IsTrue(i == 1);
        }
        [TestMethod]
        public void EnqueueTest()
        {
            queue.Enqueue(4);
            Assert.AreEqual(1, queue.Count);
        }
        [TestMethod]
        public void RemoveFromLineTest()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.RemoveFromLine(3);
            Assert.IsFalse(queue.Contains(3));
        }
    }
}
