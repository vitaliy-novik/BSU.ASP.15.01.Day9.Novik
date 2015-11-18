using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySearchTree;
using BinarySearchTree.Tests;
using Matrix;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10000];
            int c = -1;
            Random r = new Random();
            while (++c < 10000)
                arr[c] = r.Next();
            BSTree<int> tree = new BSTree<int>(arr);
            TimeSpan ts = new TimeSpan();
            
            long time = ts.Ticks;
            foreach (var item in tree.InOrder())
            {
                --c;
            }
            time = ts.Ticks - time;
            Console.WriteLine(time);
            Console.Read();
        }
    }
}
