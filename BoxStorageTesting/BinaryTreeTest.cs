using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using BoxModel;
using GenericDataStructures;
using System.Diagnostics;

namespace BoxStorageTesting
{
    [TestClass]
    public class BinaryTreeTesting
    {
        static Random r = new Random();
        static Stopwatch stopwatch = new Stopwatch();
        static BinaryTree<int, int> binaryTree = new BinaryTree<int, int>();
        [TestMethod]
        public void BinaryTreeAddValid()
        {
           
            binaryTree.Add(7, 7);
            binaryTree.Add(8, 8);
            binaryTree.Add(9, 9);
            Assert.IsTrue(binaryTree.Count() == 3);
        }
        [TestMethod]
        public void EqualAddition()
        {
            binaryTree.Add(3, 5);
            binaryTree.Add(4, 4);
            binaryTree.Add(3, 6);
            Assert.IsTrue(binaryTree.Count() == 2);
        }
        [TestMethod]
        public void BinaryTreeAddPreformanceTest()
        {
            stopwatch.Start();
            for (int i = 0; i < 100; i++)
                binaryTree.Add(r.Next(1, 100), r.Next(1,100));
            Assert.IsTrue((int)stopwatch.ElapsedMilliseconds < 10);

        }
        [TestMethod]
        public void BinaryTreeRemoveValid()
        {
            
            binaryTree.Add(7, 7);
            binaryTree.Add(8, 8);
            binaryTree.Add(9, 9);
            binaryTree.Add(3, 3);
            binaryTree.Add(5, 5);
            binaryTree.Remove(3);
            Assert.IsFalse(binaryTree.Contains(3));
            Assert.IsTrue(binaryTree.Contains(5));
        }
        [TestMethod]
        public void RemoveTestInvalid()
        {
            
            binaryTree.Add(7, 7);
            binaryTree.Remove(3);
            Assert.IsTrue(binaryTree.Contains(7));
        }
        [TestMethod]
        public void FindTestValid()
        {
            
            binaryTree.Add(12, 6);
            binaryTree.Add(13, 7);
            binaryTree.Add(10, 6);
            Assert.IsTrue(binaryTree.TryFind(10, out int i));
        }
        [TestMethod]
        public void GetLargerTestValid()
        {
            binaryTree.Add(1, 3);
            binaryTree.Add(2, 3);
            Assert.AreEqual(binaryTree.GetLarger(1), 2);
        }
    }


    [TestClass]
    public class StackTestClass
    {
        static MyStack<int> stack = new MyStack<int>();
        [TestMethod]
        public void StackAddTest()
        {
            stack.Push(1);
            Assert.IsTrue(stack.Count == 1);
        }
        [TestMethod]
        public void StackPopTest()
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            int i = stack.Pop();
            Assert.IsTrue(i == 3);
        }
    }
}
