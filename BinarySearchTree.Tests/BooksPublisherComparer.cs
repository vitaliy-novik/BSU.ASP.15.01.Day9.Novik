using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookList;

namespace BinarySearchTree.Tests
{
    class BooksPublisherComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y) => x.Publisher.CompareTo(y.Publisher);
    }
}
