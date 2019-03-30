using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RedBlackTree
{
    public class RedBlackTree<T> where T : IComparable<T>
    {
        internal class Node : IComparable<Node>
        {
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
            public Node First
            {
                get
                {
                    if (Left != null)
                    {
                        return Left;
                    }
                    if (Right != null)
                    {
                        return Right;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public int ChildCount
            {
                get
                {
                    int count = 0;
                    if (Left != null)
                    {
                        count++;
                    }
                    if (Right != null)
                    {
                        count++;
                    }
                    return count;
                }
            }

            public int Balance
            {
                get
                {
                    int right = Right == null ? 0 : Right.Height;
                    int left = Left == null ? 0 : Left.Height;
                    return right - left;
                }
            }

            public Node(T value)
            {
                Value = value;
                Height = 1;
            }

            public int CompareTo(Node other)
            {
                return Value.CompareTo(other.Value);
            }

            public void PrintPretty(string indent, bool last)
            {

                Debug.Write(indent);
                if (last)
                {
                    Debug.Write("└─");
                    indent += "  ";
                }
                else
                {
                    Debug.Write("├─");
                    indent += "| ";
                }
                Debug.WriteLine(Value);

                List<Node> children = new List<Node>();
                if (Left != null)
                {
                    children.Add(Left);
                }

                if (Right != null)
                {
                    children.Add(Right);
                }

                for (int i = 0; i < children.Count; i++)
                {
                    children[i].PrintPretty(indent, i == children.Count - 1);
                }

            }
        }

        internal Node Root { get; private set; } = null;
        public int Count { get; private set; } = 0;

        //public RedBlackTree()
        //{

        //}

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

            if (value.CompareTo(node.Value) < 0)
            {
                Add(node.Left, value);
            }
            else
            {
                Add(node.Right, value);
            }

            return node;
        }
    }
}