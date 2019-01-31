using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    internal class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
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
    }
}
