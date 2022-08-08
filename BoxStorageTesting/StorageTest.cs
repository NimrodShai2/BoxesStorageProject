using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using GenericDataStructures;
using BoxModel;
using System.Diagnostics;

namespace BoxStorageTesting
{
    [TestClass]
    public class StorageTest
    {
        static BoxStorage storage = BoxStorage.Instance;

        [TestMethod]
        public void BoxEqualTest()
        {
            Box b1 = new Box(30, 20);
            Box b2 = new Box(30, 20);
            Assert.AreEqual(b1, b2);
        }
        [TestMethod]
        public void BoxRemoveMethod()
        {
            Box b1 = new Box(23, 25);
            storage.Remove(b1);
            Assert.IsFalse(storage.Contains(b1));
        }
        [TestMethod]
        public void BoxAddTimesMethod()
        {
            Box b1 = new Box(25, 26);
            storage.AddTimes(b1, 15);
            Assert.IsTrue(b1.NumOfCopies == 15);
        }
    }
}
