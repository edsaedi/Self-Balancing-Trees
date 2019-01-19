using System;
using System.Linq;
using Self_Balancing_Trees;
using Xunit;

namespace AVL_Test
{
    public class UnitTest1
    {
        Random rand = new Random();

        public int[] Randomize(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(0, size + 1);
            }
            return array;
        }

        public int[] UniqueRandomization(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                int temp = rand.Next(0, size * 2);
                while (array.Contains(temp))
                {
                    temp = rand.Next(0, size * 2);
                }
                array[i] = temp;
            }
            return array;
        }

        public AVLTree<int> CrateTree(int size)
        {
            return CreateTree(Randomize(size));
        }

        public AVLTree<int> CreateTree(int[] size)
        {
            AVLTree<int> tree = new AVLTree<int>();
            for (int i = 0; i < size.Length; i++)
            {
                tree.Add(size[i]);
            }
            return tree;
        }

        internal void CheckNode(AVLNode<int> node)
        {
            if (node.Left != null)
            {
                Assert.True(node.Left.Value < node.Value);
                CheckNode(node.Left);
            }
            if (node.Right != null)
            {
                Assert.True(node.Right.Value > node.Value);
                CheckNode(node.Right);
            }
        }

        internal void CheckTree(AVLTree<int> tree, int size)
        {
            CheckNode(tree.root);
            Assert.True(tree.Count == size);
        }

        [Fact]
        public void Find()
        {
            int size = 100;
            for (int i = 0; i < size; i++)
            {
                int[] array = UniqueRandomization(size);
                var tree = CreateTree(array);

                for (int j = 0; j < array.Length; j++)
                {
                    Assert.True(array[j] == tree.Find(array[j]).Value);
                }
            }
        }
    }
}