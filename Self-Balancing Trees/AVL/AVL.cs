using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class AVL<T> where T : IComparable<T>
    {
        Node<T> Root = null;
        public int Count { get; private set; } = 0;

        public void Add(T value)
        {
            Root = Add(Root, value);
            Count++;
        }

        private Node<T> Add(Node<T> node, T value)
        {
            if (node == null)
            {
                return new Node<T>(value);
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

        private void UpdateHeight(Node<T> node)
        {
            int left = (node.Left == null ? 0 : node.Left.Height);
            int right = node.Right == null ? 0 : node.Right.Height;
            node.Height = Math.Max(left, right) + 1;
        }

        private Node<T> Fixup(Node<T> node)
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

        private Node<T> RotateRight(Node<T> node)
        {
            var pivot = node.Left;
            var child = pivot.Right;

            pivot.Right = node;
            node.Left = child;

            return pivot;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var pivot = node.Right;
            var child = pivot.Left;

            pivot.Left = node;
            node.Right = child;

            return pivot;
        }
    }
}
