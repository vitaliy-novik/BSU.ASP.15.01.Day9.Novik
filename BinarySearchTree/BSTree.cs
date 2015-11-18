using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public sealed class BSTree<T> : IEnumerable<T>
    {
        private class BSTNode
        {
            public T Value { get; set; }
            public BSTNode Left { get; set; }
            public BSTNode Right { get; set; }

            public BSTNode(T value) : this(value, null, null) { }

            public BSTNode(T value, BSTNode left, BSTNode right)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }

        private BSTNode root;
        private Comparison<T> comparer;

        public int Count { get; private set; } = 0;

        #region Constructors

        public BSTree()
        {
            ComparerInit();
        }

        public BSTree(IComparer<T> comparer)
        {
            if (comparer == null)
                ComparerInit();
            else
                this.comparer = (t1, t2) => comparer.Compare(t1, t2);
        }

        public BSTree(Comparison<T> comparer)
        {
            if (comparer == null)
                ComparerInit();
            else
                this.comparer = comparer;
        }

        public BSTree(IEnumerable<T> items)
        {
            ComparerInit();
            ItemsInit(items);
        }

        public BSTree(IEnumerable<T> items, Comparison<T> comparer) : this(comparer)
        {
            ItemsInit(items);
        }

        public BSTree(IEnumerable<T> items, IComparer<T> comparer) : this(comparer)
        {
            ItemsInit(items);
        }

        #endregion

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException($"{nameof(item)}");
            BSTNode current = root, parent = null;
            int comparison;
            while (current != null)
            {
                comparison = comparer(item, current.Value);
                if (comparison == 0)
                    return;
                parent = current;
                if (comparison > 0)
                    current = current.Right;
                else
                    current = current.Left;                                    
            }
            if (parent == null)
                root = new BSTNode(item);
            else
            {
                comparison = comparer(item, parent.Value);
                if (comparison > 0)
                    parent.Right = new BSTNode(item);
                else
                    parent.Left = new BSTNode(item);
            }
            ++Count;
        }

        public bool Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException($"{nameof(item)}");
            if (root == null)
                return false;  
            BSTNode current = root, parent = null;
            int comparison = comparer(item, current.Value);
            while (comparison != 0)
            {
                if (comparison < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
                else
                    comparison = comparer(item, current.Value);
            }
            if (current.Right == null)
            {
                if (parent == null)
                    root = current.Left;
                else
                {
                    comparison = comparer(parent.Value, current.Value);
                    if (comparison > 0)
                        parent.Left = current.Left;
                    else if (comparison < 0)
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                    root = current.Right;
                else
                {
                    comparison = comparer(parent.Value, current.Value);
                    if (comparison > 0)
                        parent.Left = current.Right;
                    else if (comparison < 0)
                        parent.Right = current.Right;
                }
            }
            else
            {
                BSTNode leftMin = current.Right.Left, leftMinParent = current.Right;
                while (leftMin.Left != null)
                {
                    leftMinParent = leftMin;
                    leftMin = leftMin.Left;
                }

                leftMinParent.Left = leftMin.Right;

                leftMin.Left = current.Left;
                leftMin.Right = current.Right;

                if (parent == null)
                    root = leftMin;
                else
                {
                    comparison = comparer(parent.Value, current.Value);
                    if (comparison > 0)
                        parent.Left = leftMin;
                    else if (comparison < 0)
                        parent.Right = leftMin;
                }
            }
            --Count;
            return true;
        }        

        public bool Contains(T item)
        {
            if (item == null)
                throw new ArgumentNullException($"{nameof(item)}");
            BSTNode current = root;
            int comparison;
            while (current != null)
            {
                comparison = comparer(item, current.Value);
                if (comparison == 0)
                    return true;
                if (comparison > 0)
                    current = current.Right;
                else
                    current = current.Left;
            }
            return false;
        }

        #region Traversal

        public IEnumerable<T> PreOrder()
        {
            Stack<BSTNode> stack = new Stack<BSTNode>();
            BSTNode current;
            stack.Push(root);
            while (stack.Count != 0)
            {
                current = stack.Pop();
                if (current != null)
                {
                    stack.Push(current.Right);
                    stack.Push(current.Left);
                    yield return current.Value;
                }
            }
        }

        public IEnumerable<T> InOrder()
        {
            foreach(T value in InOrder(root))
                yield return value;
        }

        private IEnumerable<T> InOrder(BSTNode node)
        {
            if (node == null)
                yield break;
            foreach (T value in InOrder(node.Left))
                yield return value;
            yield return node.Value;
            foreach (T value in InOrder(node.Right))
                yield return value;
        }

        public IEnumerable<T> PostOrder()
        {
            Stack<BSTNode> stack = new Stack<BSTNode>();
            BSTNode current = root, parent = null;
            while (true)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                if (stack.Count == 0)
                    break;
                current = stack.Peek();
                if (current.Right != null && current.Right != parent)
                    current = current.Right;
                else
                {
                    yield return current.Value;
                    parent = current;
                    current = null;
                    stack.Pop();
                }
            }
        }

        #endregion

        public IEnumerator<T> GetEnumerator() => InOrder().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region private methods

        private void ComparerInit()
        {
            if (!typeof(T).GetInterfaces().Contains(typeof(IComparable<T>)))
                throw new ArgumentException($"{typeof(T)} is not Comparable");            
            comparer = (t1, t2) => (t1 as IComparable<T>).CompareTo(t2);
        }

        private void ItemsInit(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException($"{nameof(items)}");
            foreach (var item in items)
            {
                Add(item);
            }
        }

        #endregion
        
    }
}
