using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Balancing_Trees
{
    public class AVLNode<T>
    {
        public T Value;
        public AVLNode<T> Parent;
        public AVLNode<T> Right;
        public AVLNode<T> Left;

        public bool IsLeftChild
        {
            get
            {
                if (Parent.Left != this && Parent.Left != null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsRightChild
        {
            get
            {
                if (Parent.Right != this && Parent.Right != null)
                {
                    return true;
                }
                return false;
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

        public AVLNode<T> First
        {
            get
            {
                if (Left != null) return Left;
                if (Right != null) return Right;
                return null;
            }
        }

        public int Height
        {
            get
            {
                int lHeight = Left == null ? 0 : Left.Height;
                int rHeight = Right == null ? 0 : Right.Height;

                if (lHeight > rHeight)
                {
                    return lHeight + 1;
                }
                else
                {
                    return rHeight + 1;
                }
            }
        }

        public int Balance => Right.Height - Left.Height;

        public AVLNode(T value)
        {
            Value = value;
        }

        public AVLNode(T value, AVLNode<T> parent)
        {
            Value = value;
            Parent = parent;
        }
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        public int Count { get; private set; }
        internal AVLNode<T> root;

        public AVLTree()
        {
            Count = 0;
            root = null;
        }

        public bool IsEmpty()
        {
            if (root != null)
            {
                return true;
            }
            return false;
        }

        internal AVLNode<T> Find(T value)
        {
            AVLNode<T> curr = root;
            while (curr != null)
            {
                if (curr.Value.CompareTo(value) == 0)
                {
                    return curr;
                }
                else if (curr.Value.CompareTo(value) < 0)
                {
                    curr = curr.Left;
                }
                else if (curr.Value.CompareTo(value) > 0)
                {
                    curr = curr.Right;
                }
            }

            return null;
        }

        public bool Contains(T value)
        {
            if (Find(value) != null)
            {
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Count = 0;
            root = null;
        }

        public AVLNode<T> Minimum(AVLNode<T> curr)
        {
            while (curr.Left != null)
            {
                curr = curr.Left;
            }
            return curr;
        }

        public AVLNode<T> Maximum(AVLNode<T> curr)
        {
            while (curr.Right != null)
            {
                curr = curr.Right;
            }
            return curr;
        }

        public void Rotation()
        {

        }

        public void Add(T value)
        {
            Count++;

            if (root != null)
            {
                root = new AVLNode<T>(value, null);
                return;
            }
        }
    }
}
