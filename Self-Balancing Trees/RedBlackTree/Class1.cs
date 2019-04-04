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
            public bool IsBlack { get; set; }

            public Node(T value)
            {
                Value = value;
                Left = null;
                Right = null;
                IsBlack = false;
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

        public RedBlackTree()
        {
            Count = 0;
            Root = null;
        }

        internal bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }

            return !node.IsBlack;
        }

        private void FlipColor(Node node)
        {
            node.IsBlack = !node.IsBlack;
            node.Left.IsBlack = !node.Left.IsBlack;
            node.Right.IsBlack = !node.Right.IsBlack;
        }

        public void Clear()
        {
            Count = 0;
            Root = null;
        }

        public void Add(T value)
        {
            Add(Root, value);
            Root.IsBlack = true;
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
            else if (value.CompareTo(node.Value) > 0)
            {
                Add(node.Right, value);
            }
            else
            {
                throw new ArgumentException("An entry with the same value exists!");
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColor(node);
            }

            if (IsRed(node.Right))
            {
                RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                //RotateRight(node);
            }

            return node;
        }

        internal Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            return node;
        }

        public void Print()
        {
            Root.PrintPretty("", true);
        }
    }
}