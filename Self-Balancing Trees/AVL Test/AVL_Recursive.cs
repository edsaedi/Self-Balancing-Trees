using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AVL;

namespace AVL_Test
{
    public class AVL_Recursive
    {
        Random rand = new Random(26);

        public AVL<int> CreateTree(int size)
        {
            return CreateTree(Helper.UniqueRandomization(size, rand));
        }

        public AVL<int> CreateTree(int[] size)
        {
            AVL<int> tree = new AVL<int>();
            for (int i = 0; i < size.Length; i++)
            {
                tree.Add(size[i]);
            }
            return tree;
        }

        internal void CheckNode(AVL<int>.Node node)
        {
            Assert.True(node.Balance <= 1 && node.Balance >= -1);
            if (node.Left != null)
            {
                Assert.True(node.Left.CompareTo(node) < 0);
                CheckNode(node.Left);
            }
            if (node.Right != null)
            {
                Assert.True(node.Right.CompareTo(node) >= 0);
                CheckNode(node.Right);
            }
        }

        internal void CheckTree(AVL<int> tree, int size)
        {
            CheckNode(tree.Root);
            Assert.True(size == tree.Count);
        }

        [Fact]
        public void Add()
        {
            int size = 100;
            CheckTree(CreateTree(Helper.UniqueRandomization(size, rand)), size);
        }
    }
}
