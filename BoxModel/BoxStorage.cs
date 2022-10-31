using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GenericDataStructures;
using System.Collections;

namespace BoxModel
{
    public class BoxStorage : IEnumerable<Box>
    {
        #region Boxes Binary Search Tree
        /// <summary>
        /// An inner class that represents the specific binary tree for the boxes storage
        /// </summary>
        class BoxesBST
        {
            private BinaryTree<double, BinaryTree<double, Box>> _main = new BinaryTree<double, BinaryTree<double, Box>>();//The main tree that hold trees as values

            public void Add(Box b, out bool exists)//Can check if a box with the same size already exists
            {
                BinaryTree<double, Box> inner;
                bool found = _main.TryFind(b.Width, out inner);
                if (found)
                {
                    if (inner.TryFind(b.Height, out Box temp))
                    {
                        exists = true;
                        temp.NumOfCopies++;
                    }
                    else
                    {
                        exists = false;
                        inner.Add(b.Height, b);
                        b.NumOfCopies++;
                    }
                }
                else
                {
                    exists = false;
                    inner = new BinaryTree<double, Box>();
                    inner.Add(b.Height, b);
                    _main.Add(b.Width, inner);
                    b.NumOfCopies++;
                }
                

            }
            /// <summary>
            /// Gets a box by width & height. Works in O(log n).
            /// </summary>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns>The box matching the parmeters, or null if not found.</returns>
            public Box GetBox(double width, double height)
            {
                BinaryTree<double, Box> inner;
                Box b;
                bool found = _main.TryFind(width, out inner);
                if (!found) { return null; }
                bool found2 = inner.TryFind(height, out b);
                if (!found2) { return null; }
                if (b.NumOfCopies < 1)
                {
                    _instance.Remove(b);
                    return null;
                }
                return b;
            }
            public void Remove(Box b)
            {
                BinaryTree<double, Box> inner;
                bool found = _main.TryFind(b.Width, out inner);
                if (!found) { return; }
                bool found2 = inner.TryFind(b.Height, out b);
                if (!found2) { return; }
                inner.Remove(b.Height);
            }
            /// <summary>
            /// Checks if a box is already contained in the tree. Works in O(log n).
            /// </summary>
            /// <param name="b"></param>
            /// <returns></returns>
            public bool Contains(Box b)
            {
                BinaryTree<double, Box> inner;
                bool found = _main.TryFind(b.Width, out inner);
                if (!found) { return false; }
                bool found2 = inner.TryFind(b.Height, out Box temp);
                return found2;
            }
            public Box GetLarger(Box b)
            {
                if (b == null)
                    return null;
                BinaryTree<double,Box> inner;
                bool found = _main.TryFind(b.Width, out inner);
                if (!found) { return null; }
                double newHeight = inner.GetLarger(b.Height);
                if (newHeight > b.Height)
                    return GetBox(b.Width, newHeight);
                double newWidth = _main.GetLarger(b.Width);
                return GetBox(newWidth, b.Height);
            }
            public double GetLargerHeight(double width, double height)
            {
                BinaryTree<double, Box> inner;
                bool found = _main.TryFind(width, out inner);
                if (!found)
                    return -1;
                return inner.GetLarger(height);
            }
            public double GetLargerWidth(double width)
            {
                return _main.GetLarger(width);
            }
        }
        #endregion


        #region Fields
		/// <summary>
        /// The double binary tree that holds all the boxes.
        /// </summary>
        private BoxesBST _storage;
        /// <summary>
        /// Queue of expiration dates for the boxes.
        /// </summary>
        private MyQueue<Box> _dates;
        private static BoxStorage _instance;//Singleton
        /// <summary>
        /// The instance of the singleton class.
        /// </summary>
        public static BoxStorage Instance//Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BoxStorage();
                    _instance.InitiateData();
                }
                return _instance;
            }
        } 
        #endregion



        private BoxStorage()
        {
            _storage = new BoxesBST();
            _dates = new MyQueue<Box>();
        }
        public void Add(Box b)
        {
            bool exists;
            _storage.Add(b, out exists);
            if (!exists)//Enqueues the date for the new class.
            {
                b.UpdateTimeBuyed();
                _dates.Enqueue(b);
            }
        }
        public void Remove(Box b)
        {
            _storage.Remove(b);
            _dates.RemoveFromLine(b);
        }
        /// <summary>
        /// Add a box a number of times.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="times"></param>
        public void AddTimes(Box b, int times)
        {
            if (b != null && b.NumOfCopies > 0)
            {
                b.NumOfCopies += times;
            }
            else
                for (int i = 0; i < times; i++)
                {
                    Add(b);
                }
        }
        public void UpdateDates()
        {
            while (!_dates.IsEmpty && _dates.FirstInLine.LastTimeBuyed.AddSeconds(Constants.NumOfDaysUntilExpired) < DateTime.Now)//If the time now is past the first boxes' expiration date.
            {
                _storage.Remove(_dates.FirstInLine);
                _dates.Dequeue();
            }
        }
        public Box GetLarger(Box b)
        {
            return _storage.GetLarger(b);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>The closest larger box by height.</returns>
        public double GetLargerHeight(double width, double height)
        {
            return _storage.GetLargerHeight(width, height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <returns>The closest larger box by width.</returns>
        public double GetLargerWidth(double width)
        {
            return _storage.GetLargerWidth(width);
        }
        public bool Contains(Box b)
        {
            return _storage.Contains(b);
        }
        /// <summary>
        /// Sends the item to the end of the dates line.
        /// </summary>
        /// <param name="item"></param>
        public void SendToEndOfLine(Box item)
        {
            _dates.SendToEndOfLine(item);
        }
        public int Count { get { return _dates.Count; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>The box answering to the given parameters, or null if not found.</returns>
        public Box GetBox(double width, double height)
        {
            return _storage.GetBox(width, height);
        }
        /// <summary>
        /// Adds 100 random sized boxes from 1 to 25.
        /// </summary>
        private void InitiateData()
        {
            Box b;
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                b = new Box(r.Next(1,25), r.Next(1,25));
                _instance.AddTimes(b, 10);
            }
        }

        #region Enumerators
        public IEnumerator<Box> GetEnumerator()
        {
            return _dates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dates.GetEnumerator();
        } 
        #endregion
    }
}
