using System;
using System.Linq;
using AVL_With_Parents;
using Xunit;

namespace AVL_Test
{
    public class AVL_Parents
    {
        Random rand = new Random(42);

        public AVLTree<int> CreateTree(int size)
        {
            return CreateTree(Helper.Randomize(size, rand));
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
            int[] array = Helper.UniqueRandomization(size, rand);
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
            CheckTree(CreateTree(Helper.UniqueRandomization(size, rand)), size);
        }

        [Fact]
        public void Remove()
        {
            int size = 100;
            //Create Tree
            int[] array = Helper.UniqueRandomization(size, rand);
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