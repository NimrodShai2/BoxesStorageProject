using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxModel
{
    public static class Constants
    {
        static readonly Configuration configuration = new Configuration();
        public static readonly int MaxNumOfCopies = configuration.Data.MaxNumOfBoxes;
        public static readonly int NumOfDaysUntilExpired = configuration.Data.NumofDaysUntilExpired;
        public static readonly double PrecentageAllowedToAdvanced = configuration.Data.PrecentageAllowedToSearch;
        public static readonly int LargestWidth = configuration.Data.LargestWidth;
        public static readonly int LargestHeight = configuration.Data.LargestHeight;
        public static readonly int AlmostNoBoxes = configuration.Data.AlmostNoBoxes;
    }
    public class Box
    {
        #region Statics
        static readonly string AboutToEndMessage = $"Number of copies for this box is below {Constants.AlmostNoBoxes}!\n"; 
        #endregion

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
        public double Width => _width;
        public double Height => _height;
        public bool AboutToEnd => NumOfCopies <= Constants.AlmostNoBoxes;
        #endregion

        #region Methods
        public override string ToString()
        {
            string s =$"Width: {_width}, Height: {_height}, Amount: {NumToGive}\n";
            return AboutToEnd ? s + AboutToEndMessage : s;
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
        public void UpdateTimeBuyed() => LastTimeBuyed = DateTime.Now;
       
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
            string s = $"Width: {_width}, Height: {_height}\nAmount Available: {NumOfCopies}\nLast Time Modifed: {LastTimeBuyed:d}\nExpiration Date:{LastTimeBuyed.AddDays(14):d}\n";
            return AboutToEnd ? s + AboutToEndMessage : s;
        } 
        #endregion
    }
}
