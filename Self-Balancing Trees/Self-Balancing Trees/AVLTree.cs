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
        public int Height;

        public int Balance
        {
            get
            {
                int right = Right == null ? 0 : Right.Height;
                int left = Left == null ? 0 : Left.Height;
                return right - left;
            }
        }

        public bool IsLeftChild
        {
            get
            {
                if (Parent != null && Parent.Left != this && Parent.Left != null)
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
                if (Parent != null && Parent.Right != this && Parent.Right != null)
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

        public AVLNode(T value)
        {
            Value = value;
            Height = 1;
        }

        public AVLNode(T value, AVLNode<T> parent)
        {
            Value = value;
            Parent = parent;
            Height = 1;
        }
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        public int Count { get; private set; }
        public AVLNode<T> root;

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

        public AVLNode<T> Find(T value)
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

        public void RotateLeft(AVLNode<T> node)
        {
            var parent = node.Parent;
            var pivot = node.Right;
            var child = pivot.Left;

            if (node.IsLeftChild)
            {
                parent.Left = pivot;
            }
            else if (node.IsRightChild)
            {
                parent.Right = pivot;
            }

            pivot.Left = node;
            node.Parent = pivot;

            node.Right = child;
            child.Parent = node;
        }

        public void RotateRight(AVLNode<T> node)
        {
            var parent = node.Parent;
            var pivot = node.Left;
            var child = pivot.Right;

            //Fix Parent
            if (node.IsLeftChild)
            {
                parent.Left = pivot;
            }
            else if (node.IsRightChild)
            {
                parent.Right = pivot;
            }

            //Rotate
            pivot.Right = node;
            node.Parent = pivot;

            //Pass Child
            node.Left = child;
            child.Parent = node;
        }

        public void Add(T value)
        {
            Count++;
            if (root == null)
            {
                root = new AVLNode<T>(value);
                return;
            }
            var curr = root;
            while (curr != null)
            {
                if (value.CompareTo(curr.Value) < 0)
                {
                    if (curr.Left == null)
                    {
                        //add the new child
                        curr.Left = new AVLNode<T>(value, curr);
                        break;
                    }

                    curr = curr.Left;
                }
                else
                {
                    if (curr.Right == null)
                    {
                        curr.Right = new AVLNode<T>(value, curr);
                        break;
                    }

                    curr = curr.Right;
                }
            }


            Fixup(curr);
        }

        public bool Remove(T value)
        {
            var toRemove = Find(value);
            if (toRemove == null)
            {
                return false;
            }

            Remove(toRemove);
            Count--;
            return true;
        }

        private void Remove(AVLNode<T> del)
        {
            if (del.ChildCount == 0)
            {
                if (del.IsLeftChild)
                {
                    del.Parent.Left = null;
                }
                else if (del.IsRightChild)
                {
                    del.Parent.Right = null;
                }
                else
                {
                    root = null;
                }

                Fixup(del.Parent);
            }
            else if (del.ChildCount == 1)
            {
                if (del.IsLeftChild)
                {
                    del.Parent.Left = del.First;
                    del.First.Parent = del.Parent;
                }
                else if (del.IsRightChild)
                {
                    del.Parent.Right = del.First;
                    del.First.Parent = del.Parent;
                }
                else
                {
                    root = del.First;
                    root.Parent = null;
                }

                Fixup(del.Parent);
            }
            else if (del.ChildCount == 2)
            {
                AVLNode<T> curr = Maximum(del.Left);
                del.Value = curr.Value;
                Remove(curr);
            }
        }

        private void Fixup(AVLNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            //Update Height
            int left = node.Left == null ? 0 : node.Left.Height;
            int right = node.Right == null ? 0 : node.Right.Height;
            node.Height = Math.Max(left, right) + 1;

            if (node.Balance > 1) //right heavy
            {
                if (node.Right.Balance < 0) //weight on right
                {
                    RotateRight(node.Right);
                }

                RotateLeft(node);
            }
            else if (node.Balance < -1) //left heavy
            {
                if (node.Left.Balance > 0) //weight on left
                {
                    RotateLeft(node.Left);
                }

                RotateRight(node);
            }

            Fixup(node.Parent);
        }
    }
}
