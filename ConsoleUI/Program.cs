using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySearchTree;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTree<int> tree = new BSTree<int>((a, b) => -a.CompareTo(b)) { 5, 2, 8, 3, 7, 1, 4, 6, 9 };
            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}
