﻿using System;
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

            public Node First
            {
                get
                {
                    Node first = null;
                    if (Left != null)
                    {
                        first = Left;
                    }
                    else if (Right != null)
                    {
                        first = Right;
                    }
                    return first;
                }
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
            node.Right.IsBlack = !node.Right.IsBlack;
            node.Left.IsBlack = !node.Left.IsBlack;
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
                RotateRight(node);
            }

            return node;
        }

        public bool Remove(T value)
        {
            int initialCount = Count;
            if (Root != null)
            {
                Root = Remove(Root, value);
                if (Root != null)
                {
                    Root.IsBlack = true;
                }
            }

            return initialCount == Count - 1 ? true : false;
        }

        internal Node Remove(Node node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                Remove(node.Left, value);
            }
            else if (value.CompareTo(node.Value) > 0)
            {
                Remove(node.Right, value);
            }
            else
            {
                Count--;
                if (node.ChildCount == 0)
                {
                    return null;
                }

                else if (node.ChildCount == 1)
                {
                    return node.First;
                }
                else
                {
                    Node canidate = Maximum(node);
                    node.Value = canidate.Value;
                    node.Left = Remove(node.Left, canidate.Value);
                }
            }
            return node;

        }

        internal Node RotateLeft(Node node)
        {
            //Node rotation
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            //Color rotation
            temp.IsBlack = node.IsBlack;
            node.IsBlack = false;
            return node;
        }

        internal Node RotateRight(Node node)
        {
            //Node rotation
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            //Color rotation
            temp.IsBlack = node.IsBlack;
            node.IsBlack = false;
            return node;
        }

        internal Node Maximum(Node node)
        {
            var temp = node.Right;
            while (temp.Left != null)
            {
                temp = temp.Left;
            }
            return null;
        }

        internal Node MoveRedRight(Node node)
        {
            FlipColor(node);

            if (IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColor(node);
            }

            return node;
        }

        internal Node MoveRedLeft(Node node)
        {
            FlipColor(node);

            if (IsRed(node.Right))
            {
                if (IsRed(node.Left))
                {
                    node.Right = RotateRight(node.Right);
                    node = RotateLeft(node);
                }
            }

            return node;
        }

        public void Print()
        {
            Root.PrintPretty("", true);
        }
    }
}