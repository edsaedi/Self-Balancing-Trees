using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    class AVL<T> where T : IComparable<T>
    {
        //Root
        Node<T> Root;
        //Count = 0

        //Add(T value) <- calls the recursive one
        //InternalAdd(Node node, T Value) <- recursive

        /*
            EscapeCase: if the given node is null

            PreRecursion: go down the tree

            RecursiveCall: InternalAdd

            

        */

        public void Add(T value)
        {
            Add(Root, value);
        }

        private void Add(Node<T> node, T value)
        {
            //code here will run as we go DOWN the tree

            if (Root == null)
            {
                Root = new Node<T>(value);
                return;
            }

            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node<T>(value);
                }
                else
                {
                    Add(node.Left, value);
                }
            }
            else //if (value.CompareTo(node.Value) >= 0)
            {
                if (node.Right == null)
                {
                    node.Right = new Node<T>(value);
                }
                else
                {
                    Add(node.Right, value);
                }
            }

            Fixup(node);

            //code here will run as we go UP the tree
            //Balance the current node
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
                    RotateRight(node.Right);
                }

                RotateLeft(node);
            }
            else if (node.Balance < -1)
            {
                if (node.Left.Balance > 0)
                {
                    //Rotate left on left child
                    RotateLeft(node.Left);
                }

                RotateRight(node);
            }
            return null;
        }

        private Node<T> RotateRight(Node<T> node)
        {
            return null;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            return null;
        }
    }
}
