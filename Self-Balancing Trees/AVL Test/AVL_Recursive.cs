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

        internal void CheckNode(Node)

        internal void CheckTree(AVL<int> tree, int size)
        {

        }

        [Fact]
        public void Add()
        {
            int size = 100;
            int[] array = Helper.UniqueRandomization(size, rand);

            //every node should have a balance between -1 & 1
            //the count of the tree should be correct

        }
    }
}
