using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BookList;

namespace BinarySearchTree.Tests
{
    [TestFixture]
    public class BSTreeTests
    {
        
        [Test]
        public void InOrderIntDefaultComparerTest()
        {
            BSTree<int> tree = new BSTree<int>(
                new int[]{ 5, 2, 8, 1, 3, 4, 7, 9, 6 });

            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            CollectionAssert.AreEqual(expected, tree.InOrder());
        }

        [Test]
        public void PreOrderIntDefaultComparerTest()
        {
            BSTree<int> tree = new BSTree<int>(
                new int[] { 5, 2, 8, 1, 3, 4, 7, 9, 6 });

            int[] expected = { 5, 2, 1, 3, 4, 8, 7, 6, 9 };

            CollectionAssert.AreEqual(expected, tree.PreOrder());
        }

        [Test]
        public void PostOrderIntDefaultComparerTest()
        {
            BSTree<int> tree = new BSTree<int>(
                new int[] { 5, 2, 8, 1, 3, 4, 7, 9, 6 });

            int[] expected = { 1, 4, 3, 2, 6, 7, 9, 8, 5 };

            CollectionAssert.AreEqual(expected, tree.PostOrder());
        }

        [Test]
        public void InOrderIntNumberOfDigitsComparerTest()
        {
            BSTree<int> tree = new BSTree<int>(
                new int[] { 55555, 666666, 333, 333, 22, 1, 4444}, 
                new IntNumberOfDigitsComparer());

            int[] expected = { 1, 22, 333, 4444, 55555, 666666};

            CollectionAssert.AreEqual(expected, tree.InOrder());
        }

        [Test]
        public void InOrderStringDefaultComparerTest()
        {
            BSTree<string> tree = new BSTree<string>(
                new string[] { "g", "d", "s", "a", "p", "P", "z" });

            string[] expected = { "a", "d", "g", "p", "P", "s", "z" };

            CollectionAssert.AreEqual(expected, tree.InOrder());
        }

        [Test]
        public void InOrderStrinLengthComparerTest()
        {
            BSTree<string> tree = new BSTree<string>(
                new string[] { "ggg", "dd", "sssss", "a", "pppp", "zzzzzz" }, 
                new StringLengthComparer());

            string[] expected = { "a", "dd", "ggg", "pppp", "sssss", "zzzzzz" };

            CollectionAssert.AreEqual(expected, tree.InOrder());
        }

        static Book book1 = new Book()
        {
            Author = "Jeffrey Richter",
            Title = "CLR via C#",
            Publisher = "Microsoft Press"
        };
        static Book book2 = new Book()
        {
            Author = "Joseph Albahary, Ben Albahary",
            Title = "C# 5.0 in a Nutshell",
            Publisher = "O'Reilly"
        };        
        static Book book3 = new Book()
        {
            Author = "Dino Esposito",
            Title = "Programming Microsoft ASP.NET 4",
            Publisher = "Microsoft Press"
        };        

        Book[] books = { book1, book2, book3 };
        
        [Test]
        public void InOrderBooksDefaultComparerTest()
        {
            BSTree<Book> tree = new BSTree<Book>(books);

            Book[] expected = { book2, book1, book3 };

            CollectionAssert.AreEqual(expected, tree.InOrder());
        }

        [Test]
        public void InOrderBooksPublisherComparerTest()
        {
            BSTree<Book> tree = new BSTree<Book>(books, new BooksPublisherComparer());

            Book[] expected = { book1, book2 };

            CollectionAssert.AreEqual(expected, tree.InOrder());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InOrderPoint2DDefaultComparerTest()
        {            
            BSTree<Point2D> tree = new BSTree<Point2D>(
                new Point2D[] { new Point2D(1, 2), new Point2D(3, 4) });            
        }

    }
}
