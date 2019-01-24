using System;
using System.Linq;
using Self_Balancing_Trees;
using Xunit;

namespace AVL_Test
{
    public class UnitTest1
    {
        Random rand = new Random(42);

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

        public AVLTree<int> CreateTree(int size)
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
            int[] array = UniqueRandomization(size);
            var tree = CreateTree(array);

            for (int j = 0; j < array.Length; j++)
            {
                Assert.True(array[j] == tree.Find(array[j]).Value);
            }
        }

        [Fact]
        public void Add()
        {
            int size = 100;
            CheckTree(CreateTree(UniqueRandomization(size)), size);
        }

        [Fact]
        public void Remove()
        {
            int size = 100;
            //Create Tree
            int[] array = UniqueRandomization(size);
            var tree = CreateTree(array);
            //Find Index to Remove
            int index = rand.Next(0, size + 1);
            //Remove from tree
            tree.Remove(array[index]);
            //Check Tree
            Assert.False(tree.Contains(array[index]));
            CheckTree(tree, size - 1);
        }
    }
}