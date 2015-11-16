using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree.Tests
{
    class IntNumberOfDigitsComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            int xCount = CountDigits(x), 
                yCount = CountDigits(y);
            return x.CompareTo(y);
        }

        private int CountDigits(int a)
        {
            int aCount = 0;
            while (a != 0)
            {
                a /= 10;
                ++aCount;
            }
            return aCount;
        }
    }
}
