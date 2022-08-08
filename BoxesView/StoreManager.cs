using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDataStructures;
using BoxModel;

namespace BoxesView
{
    /// <summary>
    /// A class to connect the UI to the model.
    /// </summary>
    internal class StoreManager
    {
        /// <summary>
        /// Represents the storage.
        /// </summary>
        private readonly BoxStorage store;
        public StoreManager()
        {
            store = BoxStorage.Instance;
        }
        public void AddBox(double width, double height, int amount, out bool valid)
        {
            Box b = store.GetBox(width, height);
            if (b == null)
            {
                b = new Box(width, height);
            }
            else if (amount + b.NumOfCopies >= Constants.MaxNumOfCopies)
            {
                valid = false;
                return;
            }
            valid = true;
            store.AddTimes(b, amount);
        }
        /// <summary>
        /// Takes an order from the customer, but does not execute it.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="amount"></param>
        /// <param name="completed"></param>
        /// <returns>A list that is the best offer we can make to the customer.</returns>
        public DoubleLinkedList<Box> TakeOrder(double width, double height, int amount, out bool completed)
        {
            double tempHeight = height, tempWidth = width;
            var res = new DoubleLinkedList<Box>();
            while (tempWidth < width + Constants.NumOfSizesAllowedToSearch && amount > 0)//Lets the storage go up 5 width units untill it determines that the order is not complete.
            {
                while (tempHeight < height + Constants.NumOfSizesAllowedToSearch && amount > 0)//Lets the storage go up 5 height units untill it determines that the order cannot be completed.
                {
                    Box b = store.GetBox(width, tempHeight);
                    if (b == null)
                    {
                        tempHeight++;
                        continue;
                    }
                    for (int i = 0; i < b.NumOfCopies; i++)
                    {
                        if (amount == 0)
                            break;
                        amount--;
                        b.NumToGive++;
                    }
                    res.AddToStart(b);
                    if (amount > 0)
                        tempHeight++;
                }
                if (amount == 0)
                    break;
                else
                {
                    tempWidth++;
                }
            }
            _ = amount == 0 ? completed = true : completed = false;
            return res;
        }
        /// <summary>
        /// Executes an order.
        /// </summary>
        /// <param name="order"></param>
        public void MakeOrder(DoubleLinkedList<Box> order)
        {
            foreach (Box b in order)
            {
                b.UpdateStock();
                b.UpdateTimeBuyed();
            }
        }
        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <param name="order"></param>
        public void CancelOrder(DoubleLinkedList<Box> order)
        {
            foreach(Box b in order)
            {
                b.NumToGive = 0;
            }
        }
        public string DisplayOrder(DoubleLinkedList<Box> order)
        {
            if (order == null || order.IsEmpty)
            {
                return "Sorry! We cant offer you any boxes of your request!";
            } 
            StringBuilder sb = new StringBuilder();
            foreach (Box b in order)
            {
                sb.AppendLine(b.ToString());
            }
            return sb.ToString();
        }
        public string GetBoxInfo(double width, double height)
        {
            Box b = store.GetBox(width, height);
            if (b == null)
                return "Sorry! There is no longer such box in our store.";
            return b.GetInfo();

        }
        public void UpdateDates()
        {
            store.UpdateDates();
        }
        /// <summary>
        /// Deletes a box with the matching parameters from the DB.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DeleteBox(double width, double height)
        {
            Box box = store.GetBox(width, height);
            if (box == null)
                return;
            store.Remove(box);
        }
        public string ShowAllBoxes()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Box b in store)
            {
                sb.AppendLine(b.GetInfo());
            }
            return sb.ToString();
        }
    }
}
