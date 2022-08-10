using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxModel;
using System.Windows.Forms;
using GenericDataStructures;

namespace BoxesView
{
    internal class Program
    {
        static Timer timer = new Timer();
        static StoreManager manager = new StoreManager();
        static void Main(string[] args)
        {
            timer.Enabled = true;
            timer.Start();
            timer.Interval = (int)new TimeSpan(1, 0, 0, 0).TotalMilliseconds;
            timer.Tick += UdateDates;
            string choice;
            while (true)
            {
                Console.WriteLine("Welcome! What would you like to do today?");
                PresentOptions();
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddBox();
                        continue;
                    case "2":
                        Order();
                        continue;
                    case "3":
                        GetInfo();
                        continue;
                    case "4":
                        ShowAllBoxes();
                        continue;
                    default:
                        break;
                }
                Console.WriteLine("Goodbye!");
                break;
            }
        }

        private static void UdateDates(object sender, EventArgs e)
        {
            manager.UpdateDates();
        }

        private static void ShowAllBoxes()
        {
            Console.Clear();
            Console.WriteLine(manager.ShowAllBoxes());
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        private static void GetInfo()
        {
            double width, height;
            while (true)
            {
                Console.WriteLine("Enter the box's width, and then it's height.");
                try
                {
                    width = double.Parse(Console.ReadLine());
                    height = double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                if (width > 0 && height > 0)
                {
                    Console.WriteLine(manager.GetBoxInfo(width, height));
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Boxes cannot have a value of 0 or minus. Try again.");
                    continue;
                }
            }
        }

        private static void AddBox()
        {
            double width, height;
            string choice;
            while (true)
            {
                Console.WriteLine("Please enter the box's width, and then its height.");
                try
                {
                    width = double.Parse(Console.ReadLine());
                    height = double.Parse(Console.ReadLine());
                    
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                if (width <= 0 || height <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Boxes cannot have a value of 0 or minus. Try again.");
                    continue;
                }
                Console.WriteLine("Press 1 to delete an item, any other button to add.");
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    manager.DeleteBox(width, height);
                    MessageBox.Show("Box was deleted!");
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Enter the amount of copies you would like to add:");
                    int amount;
                    bool legal = int.TryParse(Console.ReadLine(), out amount);
                    if (legal && amount > 0)
                    {
                        manager.AddBox(width, height, amount, out bool valid);
                        if (!valid)
                        {
                            Console.WriteLine("Max number of copies for each box can only be 40. Please add a valid entry.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Illegal input sent you back to the start.");
                        continue;
                    }
                    MessageBox.Show("Box has been added!");
                    Console.Clear();
                    break;
                }
            }
        }

        private static void PresentOptions()
        {
            Console.WriteLine("Manage the storage - Press 1");
            Console.WriteLine("Make an order - Press 2");
            Console.WriteLine("Get am item's info - Press 3");
            Console.WriteLine("Show all boxes - Press 4");
            Console.WriteLine("Close the system - Any other key");
        }
        private static void Order()
        {
            double width, height;
            int amount;
            string choice;
            DoubleLinkedList<Box> res;
            bool completed;
            while (true)
            {
                Console.WriteLine("Please enter the present's width, its height, and then the amount required");
                try
                {
                    width = double.Parse(Console.ReadLine());
                    height = double.Parse(Console.ReadLine());
                    amount = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number");
                    continue;
                }
                if (height <= 0 || width <= 0 || amount <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Boxes cannot have a value of 0 or minus. Neither can amount of boxes. Try again.");
                    continue;
                }
                res = manager.TakeOrder(width, height, amount, out completed);
                if (completed)
                {
                    Console.WriteLine("Your order was completed:");
                }
                else
                {
                    Console.WriteLine("Your order couldn't be completed. Here is the most we can offer.");
                }
                Console.WriteLine(manager.DisplayOrder(res));
                if (res.IsEmpty)
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                Console.WriteLine("Press 0 to make the order, any other key to cancel it.");
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    manager.MakeOrder(res);
                    MessageBox.Show("Your order was made!");
                    Console.Clear();
                    break;
                }
                else
                {
                    manager.CancelOrder(res);
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
