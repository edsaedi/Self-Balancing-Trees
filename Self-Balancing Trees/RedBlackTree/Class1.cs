using System;

namespace RedBlackTree
{
    public class RedBlackTree<T>
    {
        internal class Node
        {
            internal T Value { get; set; }
            internal Node Right { get; set; }
            internal Node Left { get; set; }

            internal Node(T value)
            {
                Value = value;
            }
        }

        internal Node Root { get; private set; } = null;
        public int Count { get; private set; } = 0;

        public RedBlackTree()
        {

        }

        public void Add(T value)
        {
            Add(Root, value);
            Count++;
        }

        internal Node Add(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            return null;
        }
    }
}
