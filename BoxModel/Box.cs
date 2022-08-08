using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxModel
{
    public static class Constants
    {
        public static readonly int MaxNumOfCopies = 40;
        public static readonly int NumOfDaysUntilExpired = 14;
        public static readonly int NumOfSizesAllowedToSearch = 5;
    }
    public class Box
    {
        #region Fields
        /// <summary>
        /// The last time a box was modified.
        /// </summary>
        public DateTime LastTimeBuyed { get; set; }
        private readonly double _width;
        private readonly double _height;
        private int _numOfCopies;
        #endregion

        #region Ctors
        public Box(double width, double height)
        {
            _width = width;
            _height = height;
            _numOfCopies = 0;
            LastTimeBuyed = DateTime.Now;
            NumToGive = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Number of copies available.
        /// </summary>
        public int NumOfCopies
        {
            get { return _numOfCopies; }
            set
            {
                if (_numOfCopies >= Constants.MaxNumOfCopies)
                    return;
                else
                    _numOfCopies = value;
            }
        }
        /// <summary>
        /// Number of copies that are currently part of an order.
        /// </summary>
        public int NumToGive { get; set; }
        public double Width { get { return _width; } }
        public double Height { get { return _height; } } 
        #endregion
        public override string ToString()
        {
            return $"Width: {_width}, Height: {_height}, Amount: {NumToGive}";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Box))
                return false;
            Box box = (Box)obj;
            return box.Width == Width && box.Height == Height;
        }
        public override int GetHashCode()
        {
            return -1456364324 + _width.GetHashCode() * _height.GetHashCode();
        }
        /// <summary>
        /// Updates the expiration date for the box.
        /// </summary>
        public void UpdateTimeBuyed()
        {
            LastTimeBuyed = DateTime.Now;
        }
        /// <summary>
        /// Updates the number of copies available and sets the NumToGive to 0.
        /// </summary>
        public void UpdateStock()
        {
            NumOfCopies -= NumToGive;
            NumToGive = 0;
        }
        public string GetInfo()
        {
            return $"Width: {_width}, Height: {_height}\nAmount Available: {NumOfCopies}\nLast Time Modifed: {LastTimeBuyed:d}\nExpiration Date:{LastTimeBuyed.AddDays(14):d}\n";
        }
    }
}
