using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree.Tests
{
    class StringLengthComparer : IComparer<string>
    {
        public int Compare(string x, string y) => x.Length.CompareTo(y.Length);        
    }
}
