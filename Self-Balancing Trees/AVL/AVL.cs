using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    //TODO: Fix the Add
    public class AVL<T> where T : IComparable<T>
    {
        internal class Node : IComparable<Node>
        {
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

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

                var children = new List<Node>();
                if (this.Left != null)
                    children.Add(this.Left);
                if (this.Right != null)
                    children.Add(this.Right);

                for (int i = 0; i < children.Count; i++)
                {
                    children[i].PrintPretty(indent, i == children.Count - 1);
                }

            }
        }

        internal Node Root = null;
        public int Count { get; private set; } = 0;

        public void Add(T value)
        {
            Root = Add(Root, value);
            Count++;
        }

        private Node Add(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            if (value.CompareTo(node.Value) < 0)
            {
                node.Left = Add(node.Left, value);
            }
            else //if (value.CompareTo(node.Value) >= 0)
            {
                node.Right = Add(node.Right, value);
            }

            node = Fixup(node);

            return node;
        }

        public bool Remove(T value)
        {
            //search for a node
            int oldCount = Count;
            Root = Remove(Root, value);
            return oldCount != Count;
        }

        private Node Remove(Node node, T value)
        {
            //Count--; when you remove the node
        }

        internal Node Find(T value)
        {
            Node curr = Root;
            while (curr != null)
            {
                if (curr.Value.CompareTo(value) == 0)
                {
                    return curr;
                }
                else if (value.CompareTo(curr.Value) < 0)
                {
                    curr = curr.Left;
                }
                else if (value.CompareTo(curr.Value) > 0)
                {
                    curr = curr.Right;
                }
            }
            return null;
        }

        public bool Contains(T value)
        {
            return Find(value) != null; //code re-use
        }

        private void UpdateHeight(Node node)
        {
            int left = (node.Left == null ? 0 : node.Left.Height);
            int right = node.Right == null ? 0 : node.Right.Height;
            node.Height = Math.Max(left, right) + 1;
        }

        private Node Fixup(Node node)
        {
            //Update Height of current node
            UpdateHeight(node);
            //If balance is greater than one, or less than -1, rotate.
            if (node.Balance > 1)
            {
                if (node.Right.Balance < 0)
                {
                    //Rotate Right on right child
                    node.Right = RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }
            else if (node.Balance < -1)
            {
                if (node.Left.Balance > 0)
                {
                    //Rotate left on left child
                    node.Left = RotateLeft(node.Left);
                }

                node = RotateRight(node);
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            var pivot = node.Left;

            node.Left = pivot.Right;
            pivot.Right = node;

            UpdateHeight(node);
            UpdateHeight(pivot);

            return pivot;
        }

        private Node RotateLeft(Node node)
        {
            var pivot = node.Right;

            node.Right = pivot.Left;
            pivot.Left = node;

            UpdateHeight(node);
            UpdateHeight(pivot);

            return pivot;
        }

        public void Print()
        {
            Root.PrintPretty("", true);
        }
    }
}
